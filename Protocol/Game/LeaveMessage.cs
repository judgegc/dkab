using System.Text;

namespace dkab.Protocol.Game
{
    class LeaveMessage : InPacket
    {
        public string Name { get; private set; }

        public override bool TryParse(byte[] packet)
        {
            string[] disPacket = Encoding.ASCII.GetString(packet).Split();

            if (!(disPacket.Length == 2 && disPacket[0].Equals("delpos")))
            {
                return false;
            }

            Name = disPacket[1];

            return true;
        }

        public override string ToString()
        {
            return string.Format("delpos {0}", Name);
        }
    }
}
