using System.Text;

namespace dkab.Protocol.Game
{
    class ReceivePing : InPacket
    {
        public override bool TryParse(byte[] packet)
        {
            return Encoding.ASCII.GetString(packet).Equals("ping");
        }
    }
}
