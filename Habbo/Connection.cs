using System.Text;
using System.Net;

using Sulakore.Protocol;
using Sulakore.Habbo;
using Sulakore.Communication;

using RetroCounter.Models;
using RetroCounter.Habbo.Headers.Packets;

namespace RetroCounter.Habbo
{
    public static class Connection
    {
        public static HNode? _client;
        public static Random _random = new();

        public static Outgoing? Outgoing;
        public static Incoming? Incoming;

        public static Dictionary<HRoomEntry, HotelModel> Rooms = new();
        public static HotelModel? Hotel;

        private async static void SendToServer(ushort header, params object[] packet)
            => await _client.SendAsync(HMessage.Construct(header, packet));

        public async static Task Connect( string sso, HotelModel hotel ) 
        {
            Hotel = hotel;

            _client = new HNode();

            if (Hotel.UseProxy)
                _client.SOCKS5EndPoint = new IPEndPoint(IPAddress.Parse(Hotel.Proxy.Ip), Hotel.Proxy.Port);
            
            await _client.ConnectAsync(IPAddress.Parse(hotel.Host), hotel.Port);

            
            SendToServer(Outgoing.Production.Header, Outgoing.Production.Release, "FLASH", 1, 0);
            SendToServer(Outgoing.ClientVariables.Header, 401, Outgoing.ClientVariables.Text1, Outgoing.ClientVariables.Text2);
            SendToServer(Outgoing.MachineID.Header, MD5(new Random().Next(10000).ToString()), MD5(new Random().Next(10000).ToString()), Outgoing.MachineID.WINRelease);
            SendToServer(Outgoing.SSOTicket.Header, sso, _random.Next(Outgoing.SSOTicket.MaxInt));

            HandlePacket(await _client.ReceivePacketAsync());
        }

        private static void OpenNavigator() 
           => SendToServer(Outgoing.OpenNavigator.Header, "hotel_view", "");


        private async static void HandlePacket(HMessage packet)
        {

            try
            {
                if (packet is null) return;

                if (packet.Header == Incoming.AuthenticationOK.Header)
                    SendToServer(Outgoing.RequestUserData.Header);

                else if (packet.Header == Incoming.UserObject.Header)
                {
                    packet.ReadInteger();
                    string username = packet.ReadString();
                    SendToServer(Outgoing.UserObject.Header, username);
                    OpenNavigator();
                }

                else if (packet.Header == Incoming.Ping.Header)
                    SendToServer(Outgoing.Pong.Header);

                else if (packet.Header == Incoming.NavigatorSearchResults.Header)
                {
                    foreach (var result in HSearchResult.Parse(packet))
                    {
                        foreach (var room in result.Rooms.OrderBy(c => c.UserCount))
                        {
                            if (!Rooms.ContainsKey(room) && room.UserCount >= 1)
                            {
                                Rooms.Add(room, Hotel);
                                Hotel.TotalRoomsCount += 1;
                            }

                        }
                    }

                    foreach (var room in Rooms.OrderBy(t => t.Key.UserCount)) 
                    {
                        if (room.Value == Hotel)
                            Hotel.TotalUsersCount += room.Key.UserCount;
                    }

                    Hotel.TotalUsersFake = int.Parse(Hotel.UsersCountCMS) - Hotel.TotalUsersCount;

                    _client.Disconnect();

                }

                HandlePacket(await _client.ReceivePacketAsync());
            }

            catch 
            {
                _client.Disconnect();
            }

        }

        private static string MD5(this string s)
        {
            using var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }
    }
}
