using FinalPRN221.Constants.LogConst;
using Serilog;
using System.Net;

namespace FinalPRN221.Extensions
{
    public static class UserLogExtension
    {
        public static async Task CreateLogUser(ApplicationUser user)
        {
            var hostEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());
            var ipAddress = hostEntry.AddressList
                .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?
                .ToString();
            string msg = $"User have id :{user.Id}- ip:{ipAddress} With Action{LogActionConst.User_Login} ";

            Log.ForContext(LogPropertiesConst.ActionID, LogActionConst.User_Login)
                      .ForContext(LogPropertiesConst.UserID, user.Id)
                      .ForContext(LogPropertiesConst.IpAddress, ipAddress)
                      .Information(msg);
        }
    }
}