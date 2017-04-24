namespace dkab.Protocol
{
    public abstract class InPacket
    {
        public abstract bool TryParse(byte[] packet);
    }
}
