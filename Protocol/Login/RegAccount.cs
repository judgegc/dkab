using System.Text;

namespace dkab.Protocol.Login
{
    class RegAccount : InPacket
    {
        public override bool TryParse(byte[] packet)
        {
            return Encoding.ASCII.GetString(packet).Equals("REG\n");
        }
    }
}
