using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring
{
    public class EqPing
    {
        public string ip;
        public string id;
        public bool status;
    }
    internal class Monitoring
    {
        MySQL mySQL = new MySQL();
        public Monitoring()
        {
        }
        static public bool Ping(string ip)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(ip);
            return reply.Status == IPStatus.Success ? true : false;
        }
        public void StartMonitoring()
        {
            while (true)
            {
                Console.WriteLine("Monitoring : " + DateTime.Now);
                DataTable dataTable = mySQL.GetDataTableSQL("SELECT id, ip, status FROM monitoring_equipment WHERE monitoring = True");

                List<EqPing> list = new List<EqPing>();
                foreach (DataRow dr in dataTable.Rows)
                    list.Add(new EqPing { ip = dr[1].ToString(), id = dr[0].ToString(), status = (bool)dr[2] });
                ParallelOptions options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = list.Count
                };
                Parallel.ForEach(list.ToArray(), options, address =>
                {
                    bool status = false;
                    for (int i = 0; i < 6; i++)
                        if (Ping(address.ip))
                        {
                            status = true;
                            break;
                        }
                    if (status != address.status)
                    {
                        if (status)
                            mySQL.SendSQL("UPDATE monitoring_equipment SET status = " + status + " WHERE id = " + address.id);
                        else
                            mySQL.SendSQL("UPDATE monitoring_equipment SET status = " + status + ", time_off = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE id = " + address.id);
                    }
                });
                Console.WriteLine("Sleep");
                Thread.Sleep(2 * 60 * 1000);
            }
        }
    }
}