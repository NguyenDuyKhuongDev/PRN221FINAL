using FinalPRN221.Constants.LogConst;
using Serilog;
using System.Net;

namespace FinalPRN221.Extensions
{
    public static class UserLogExtension
    {
        public static async Task CreateLogUser(ApplicationUser user, int ActionID, string Action)
        {
            var hostEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());
            var ipAddress = hostEntry.AddressList
                .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                .ToString();

            var msg = $"User_id: {user.Id}_ip:{ipAddress}_Action: {Action}";

            Log.ForContext(LogPropertiesConst.ActionID, ActionID)
                      .ForContext(LogPropertiesConst.UserID, user.Id)
                      .ForContext(LogPropertiesConst.IpAddress, ipAddress)
                      .Information(msg);
        }
    }
}