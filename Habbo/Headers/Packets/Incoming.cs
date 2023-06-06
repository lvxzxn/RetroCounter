using RetroCounter.Habbo.Headers.Packets.Models.Incoming;
using RetroCounter.Habbo.Headers.Packets.Models.Latency;

namespace RetroCounter.Habbo.Headers.Packets
{
    public  class Incoming
    {
        public PingMessageModel? Ping { get; set; }
        public AuthenticationOKModel? AuthenticationOK { get; set; }
        public UserObjectInModel? UserObject { get; set; }

        public NavigatorSearchResultsModel? NavigatorSearchResults { get; set; }

        public Incoming() 
        {
            Ping = new PingMessageModel();
            AuthenticationOK = new AuthenticationOKModel();
            UserObject = new UserObjectInModel();
            NavigatorSearchResults = new NavigatorSearchResultsModel();
        }
    }
}