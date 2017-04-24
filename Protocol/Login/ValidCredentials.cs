using System.Text;

namespace dkab.Protocol.Login
{
    class ValidCredentials : InPacket
    {
        public uint ClientId
        {
            get;
            internal set;
        }
        public override bool TryParse(byte[] packet)
        {
            string packetStr = Encoding.ASCII.GetString(packet);
            if (packetStr.IndexOf("OK") != 0)
                return false;

            ClientId = uint.Parse(packetStr.Substring(3, packetStr.Length - 4));

            return true;
        }
    }
}
