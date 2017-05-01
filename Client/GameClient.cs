using dkab.Protocol.Game;
using dkab.Protocol;
using dkab.Settings;
using System;
using System.Net.Sockets;
using dkab.Helpers;
using System.IO;

namespace dkab.Client
{
    /// <summary>Interacted with game server.</summary>
    class GameClient
    {     
        

        public EventPipe Events
        {
            get
            {
                return events;
            }
        }
        public bool IsConnected
        {
            get
            {
                return socket?.Connected ?? false;
            }
        }           
        public uint ClientId { get; private set; }

        private TcpClient socket;
        private string username;
        private EventPipe events;     
        private bool needDisconnect = false;

        public GameClient()
        {
            events = new EventPipe();
        }
        public bool Login(string username, string passwo rd)
        {
            this.username = username;
            GameSession s = new GameSession();
            ClientId = s.Login(username, password);

            if(ClientId > 0)
            {
                EnterGame();
                return true;
            }
          
            return false;
        }
        private void EnterGame()
        {
            try
            {
                socket = new TcpClient();
                socket.Connect(ServerSettings.Items.GameServer);
                socket.GetStream().ReadTimeout = 5000;


                PacketParser parser = new PacketParser();
                while (!needDisconnect)
                {
                    try
                    {
                        byte[] payload = ReadPacket();

                        InPacket p = parser.Parse(payload);

                        //skip yourself pos packet
                        if (p is ReceiveDestination)
                        {
                            ReceiveDestination unit = p as ReceiveDestination;

                            if (!unit.Name.Equals(username, StringComparison.OrdinalIgnoreCase))
                            {
                                events.Push(p);
                            }

                        }
                        else if(p is UpdateMonster)
                        {
                            events.Push(p);
                        }
                        else if(p is UpdateHerb)
                        {
                            events.Push(p);
                        }
                        else if (p is LeaveMessage)
                        {
                            events.Push(p);
                        }
                        else if (p is ReceivePing)
                        {
                            Ping();
                        }
                    }
                    catch (IOException)//throwed, when after 5 seconds has no data
                    {
                        Ping();
                    }                 
                }

                socket.Close();
                needDisconnect = false;
            }
            catch(Exception ex)
            {
                Logger.StackTrace(ex);
            }
        }
        public void Disconnect()
        {
            needDisconnect = true;
        }
        private void Ping()
        {
            new Ping(socket.GetStream()).Send();
        }

        private byte[] ReadPacket()
        {
            NetworkStreamOperations stream = new NetworkStreamOperations(socket.GetStream());

            byte[] lenPacket =  stream.Read(8);

            long payloadLen = BitConverter.ToInt64(lenPacket, 0);


            byte[] payload = stream.Read((int)payloadLen);

            return payload;
        }

        public void MoveTo(float x, float y)
        {
            new SetDestination(socket.GetStream(), ClientId, x, y).Send();
        }

        
    }
}
