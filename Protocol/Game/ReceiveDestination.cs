using System.Globalization;
using System.Text;

namespace dkab.Protocol.Game
{
    class ReceiveDestination : InPacket
    {
        public string Name { get; private set; }
        public float X { get; private set; }
        public float Y {get; private set;}
        public override bool TryParse(byte[] packet)
        {
            string[] disPacket = Encoding.ASCII.GetString(packet).Split();

            if(!(disPacket.Length == 4 && disPacket[0].Equals("pos")))
            {
                return false;
            }

            Name = disPacket[1];
            X = float.Parse(disPacket[2], CultureInfo.InvariantCulture);
            Y = float.Parse(disPacket[3], CultureInfo.InvariantCulture);

            return true;
        }

        public override string ToString()
        {
            return string.Format("pos {0} {1} {2}", Name, X.ToString("0.00", CultureInfo.InvariantCulture), Y.ToString("0.00", CultureInfo.InvariantCulture));
        }
    }
}
