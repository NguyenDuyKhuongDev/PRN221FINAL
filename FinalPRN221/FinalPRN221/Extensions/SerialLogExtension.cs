using NuGet.Packaging.Signing;
using Serilog.Core;
using Serilog.Events;
using System.Net;

namespace EmpManager.Logs
{
    public class SerialLogExtension : ILogEventEnricher
    {
        public string UserID { get; set; }
        public string? ipAddress { get; set; }
        public int ActionID { get; set; }
        public int LogLevelID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Message { get; set; }

        public void CustomLogEnricher(string UserID, int ActionID, int LogLevelID)
        {
            this.UserID = UserID;
            //lay ipv4
            ipAddress = Dns.GetHostEntry(Dns.GetHostName())
                              .AddressList
                              .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                              .ToString();
            this.ActionID = ActionID;
            this.LogLevelID = LogLevelID;
            TimeStamp = DateTime.Now;
            Message = $"User with UserID: {UserID}" +
                $"ActionID: {ActionID}" +
                $"LogLevelID: {LogLevelID} " +
                $"at:{TimeStamp}" +
                $" throught IPV4 ipAddress:  {ipAddress}";
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserID", UserID));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("IPAddress", ipAddress));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ActionID", ActionID));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("LogLevelID", LogLevelID));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TimeStamp", TimeStamp));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Message", TimeStamp));
        }
    }

    public class UserLog
    {
        public string UserID { get; set; }
        public string? ipAddress { get; set; }
        public int ActionID { get; set; }
        public int LogLevelID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Message { get; set; }

        public UserLog(string UserID, int ActionID, int LogLevelID)
        {
            this.UserID = UserID;
            //lay ipv4
            ipAddress = Dns.GetHostEntry(Dns.GetHostName())
                              .AddressList
                              .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                              .ToString();
            this.ActionID = ActionID;
            this.LogLevelID = LogLevelID;
            TimeStamp = DateTime.Now;
            Message = $"User with UserID: {UserID}" +
                $"ActionID: {ActionID}" +
                $"LogLevelID: {LogLevelID} " +
                $"at:{TimeStamp}" +
                $" throught IPV4 ipAddress:  {ipAddress}";
        }
    }
}