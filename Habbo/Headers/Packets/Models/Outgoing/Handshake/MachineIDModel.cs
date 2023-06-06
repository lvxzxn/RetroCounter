namespace RetroCounter.Habbo.Headers.Packets.Models.Outgoing.Handshake
{
    public class MachineIDModel
    {
        public ushort Header { get; set; }
        public string? WINRelease { get; set; }
    }
}
