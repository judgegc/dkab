using System;
using System.Net.Sockets;

namespace dkab.Protocol
{
    class OutGamePacket : OutPacket
    {
        public OutGamePacket(NetworkStream s) : base(s)
        {
        }

        public override void Send()
        {
            //send packet length
            byte[] packetSize = BitConverter.GetBytes((UInt64)data.Length);
            stream.Write(packetSize, 0, packetSize.Length);

            base.Send();
        }
    }
}
