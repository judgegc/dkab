using System;
using System.Net.Sockets;

namespace dkab.Protocol
{
    class OutPacket
    {
        protected Byte[] data;
        protected NetworkStream stream;

        public OutPacket(NetworkStream s)
        {
            stream = s;
        }
        public virtual void Send()
        {
            if (data != null)
            {
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
