using RetroCounter.Habbo.Headers.Packets.Models.Outgoing.Handshake;
using RetroCounter.Habbo.Headers.Packets.Models.Latency;
using RetroCounter.Habbo.Headers.Packets.Models.Outgoing;

namespace RetroCounter.Habbo.Headers.Packets
{
    public class Outgoing
    {
        public ClientVariablesModel? ClientVariables { get; set; }
        public MachineIDModel? MachineID { get; set; }
        public ProductionModel? Production { get; set; }
        public RequestUserDataModel? RequestUserData { get; set; }
        public SSOTicketModel? SSOTicket { get; set; }
        public UserObjectModel? UserObject { get; set; }
        public PongMessageModel? Pong { get; set; }
        public OpenNavigatorModel? OpenNavigator { get; set; }

        public Outgoing() 
        {
            ClientVariables = new ClientVariablesModel();
            MachineID = new MachineIDModel();
            Production = new ProductionModel();
            RequestUserData = new RequestUserDataModel();
            UserObject = new UserObjectModel();
            SSOTicket = new SSOTicketModel();
            Pong = new PongMessageModel();
            OpenNavigator = new OpenNavigatorModel();
        }

    }
}