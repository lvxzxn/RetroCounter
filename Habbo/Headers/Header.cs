using RetroCounter.Habbo.Headers.Packets;

namespace RetroCounter.Habbo.Headers
{
    public enum Hotel 
    {
        Habblet = 0,
        Habblive = 1,
        Iron = 2,
        Lella = 3
    }

    public class Header
    {

        static void WriteHeaders(Hotel hotel)
        {

            if (hotel == Hotel.Habblet)
            {
                Outgoing outgoing = new Outgoing();
                Incoming incoming = new Incoming();

                outgoing.Production.Header = 4000;
                outgoing.Production.Release = "PRODUCTION-202101051337-07542168";

                outgoing.ClientVariables.Header = 1053;
                outgoing.ClientVariables.Text1 = "https://images.habblet.city/library/";
                outgoing.ClientVariables.Text2 = "https://images.habblet.city/library/gamedata/habblet_vars.txt?v4?v567";

                outgoing.MachineID.Header = 2490;
                outgoing.MachineID.WINRelease = "WIN/32,0,0,363";

                outgoing.SSOTicket.Header = 2419;
                outgoing.SSOTicket.MaxInt = 10000;

                outgoing.RequestUserData.Header = 357;
                outgoing.Pong.Header = 295;
                outgoing.UserObject.Header = 3878;

                outgoing.OpenNavigator.Header = 249;
                incoming.NavigatorSearchResults.Header = 2690;

                incoming.AuthenticationOK.Header = 2491;
                incoming.UserObject.Header = 2725;
                incoming.Ping.Header = 10;

                Connection.Outgoing = outgoing;
                Connection.Incoming = incoming;

            }

            else if (hotel == Hotel.Lella) 
            {
                Outgoing outgoing = new Outgoing();
                Incoming incoming = new Incoming();

                outgoing.Production.Header = 4000;
                outgoing.Production.Release = "PRODUCTION-201611291003-338511768";

                outgoing.ClientVariables.Header = 1053;
                outgoing.ClientVariables.Text1 = "/swf/gordon/prod/";
                outgoing.ClientVariables.Text2 = "/swf/gamedata/habbo_vars.txt?v79";

                incoming.NavigatorSearchResults.Header = 2690;
                outgoing.OpenNavigator.Header = 249;

                outgoing.MachineID.Header = 2490;
                outgoing.MachineID.WINRelease = "WIN/21,0,0,213";

                outgoing.SSOTicket.Header = 2419;
                outgoing.SSOTicket.MaxInt = 10000;

                incoming.UserObject.Header = 2725;
                outgoing.UserObject.Header = 3878;

                incoming.AuthenticationOK.Header = 2491;
                outgoing.RequestUserData.Header = 357;

                incoming.Ping.Header = 3928;
                outgoing.Pong.Header = 2596;

                Connection.Outgoing = outgoing;
                Connection.Incoming = incoming;

            }

        }

        public static void Get(Hotel hotel) 
           => WriteHeaders(hotel);
    }
}
