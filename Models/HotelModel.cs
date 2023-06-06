using RetroCounter.Models.Proxy;

namespace RetroCounter.Models
{
    public class HotelModel
    {
        public string? Name { get; set; }
        public string? Host { get; set; }
        public string? UsersCountCMS { get; set; }

        public bool Encryption { get; set; }
        public bool Stuff { get; set; }
        public bool UseProxy { get; set; } = false;

        public ProxyModel? Proxy { get; set; }

        public int TotalRoomsCount { get; set; }
        public int TotalUsersCount { get; set; }
        public int TotalUsersFake { get; set; }
        public int Port { get; set; }
    }
}
