// See https://aka.ms/new-console-template for more information
try
{
    new Monitoring.Monitoring().StartMonitoring();
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
}
Console.ReadLine();