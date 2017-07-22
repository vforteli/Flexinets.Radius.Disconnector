using Flexinets.Radius.Disconnector.com.telekomaustria.m2msimplify;
using log4net;
using System;

namespace Flexinets.Radius.Disconnector
{
    public class RadiusDisconnectorV2
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(RadiusDisconnectorV2));
        private readonly String _apiUsername;
        private readonly String _apiPassword;
        private readonly String _apiUrl;


        public RadiusDisconnectorV2(String apiUsername, String apiPassword, String apiUrl)
        {
            _apiUsername = apiUsername;
            _apiPassword = apiPassword;
            _apiUrl = apiUrl;
        }


        /// <summary>
        /// Disconnect a user by msisdn
        /// </summary>
        /// <param name="msisdn"></param>
        /// <returns></returns>
        public (Boolean success, String resultCode, String errorDescription) DisconnectUserByMsisdn(String msisdn)
        {
            try
            {
                _log.Info($"Disconnecting msisdn {msisdn}");
                var client = new TAGExternalAPIImplService()
                {
                    Url = _apiUrl
                };
                var response = client.disconnectSessions(_apiUsername, _apiPassword, simIdentifier.MSISDN, new[] { msisdn });
                return (response.resultCode == "OK", response.resultCode, response.errorDescription);
            }
            catch (Exception ex)
            {
                _log.Fatal($"Something went haywire disconnecting {msisdn}. Start panicking!", ex);
                return (false, "fail", ex.Message);
            }
        }
    }
}