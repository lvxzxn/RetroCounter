using RetroCounter.Habbo;
using RetroCounter.Habbo.Headers;
using RetroCounter.Login;
using RetroCounter.Models;

List<HotelModel> _hotels = new();
UserModel user = new();

user.UserName = "retrostatsbylxz";
user.Password = "asd123123";

AddHotel();
Counter();
Initialize();

void Initialize()
{
    Console.ForegroundColor = ConsoleColor.Green;

    WriteLine();
    WriteLine();

    WriteLine("[RetroCounter]");
    WriteLine("RetroCounter by: Luiz1n", true);

    Console.ReadLine();
}

void AddHotel() 
{
    _hotels.Add(new HotelModel
    {
        Name = "Habblet",
        Host = "51.222.244.100",
        Port = 30000,
        Encryption = false,
        TotalRoomsCount = 0,
        Stuff = false,
    });

    _hotels.Add(new HotelModel
    {
        Name = "Habblive",
        Host = "",
        Port = 30000,
        Encryption = true,
        TotalRoomsCount = 0,
        Stuff = false,
    });

    _hotels.Add(new HotelModel
    {
        Name = "Iron",
        Host = "",
        Port = 30000,
        Encryption = false,
        TotalRoomsCount = 0,
        Stuff = false,
    });

    _hotels.Add(new HotelModel
    {
        Name = "Lella",
        Host = "15.235.26.128",
        Port = 30069,
        Encryption = false,
        TotalRoomsCount = 0,
        Stuff = false,
    });
}

async void Counter() 
{
    foreach (var hotel in _hotels)
    {
        if (hotel.Host != "")
        {
            string sso = "";

            if (hotel.Name == "Habblet")
            {
                sso = await Habblet.Login(hotel, user);
                Header.Get(Hotel.Habblet);
            }

            else if (hotel.Name == "Lella")
            {
                sso = await Lella.Login(hotel, user);
                Header.Get(Hotel.Lella);
            }

            await Connection.Connect(sso, hotel);
        }
    }

}

void WriteLine(string Text = "", bool ln = false) => Console.WriteLine("{0," + ((Console.WindowWidth / 2) + Text.Length / 2) + "}", ln == true ? Text + Environment.NewLine : Text);