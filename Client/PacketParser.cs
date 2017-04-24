using dkab.Helpers;
using dkab.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace dkab.Client
{
    class PacketParser
    {
        private List<Type> packetTypes;

        public PacketParser()
        {
            packetTypes = TypeHelper.FindDerivedTypes<InPacket>();
        }
        public InPacket Parse(byte[] raw)
        {
            foreach(var packetType in packetTypes)
            {
                InPacket p = (InPacket)Activator.CreateInstance(packetType);
                if (p.TryParse(raw))
                {
                    return p;
                }
            }
            string packetStr = Encoding.ASCII.GetString(raw);
            throw new Exception(string.Format("Unknown packet type: {0}", packetStr));
        }
    }
}
