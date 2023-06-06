namespace RetroCounter.Habbo.Headers.Packets.Models.Outgoing.Handshake
{
    public class ClientVariablesModel
    {
        public ushort Header { get; set; }
        public string? Text1 { get; set; }
        public string? Text2 { get; set; }
    }
}
