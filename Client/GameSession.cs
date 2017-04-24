using dkab.Protocol.Login;
using dkab.Settings;
using System;
using System.Net.Sockets;

namespace dkab.Client
{
    public class GameSession
    {
        TcpClient socket;
        private const uint MAX_PACKET_SIZE = 1024;

        public uint Login(string username, string password)
        {
            socket = new TcpClient();
            socket.Connect(ServerSettings.Items.LoginServer);

            new HelloMessage(socket.GetStream()).Send();

            byte[] helloPacket = ReadPacket();

            if (!new ReplyHelloMessage().TryParse(helloPacket))
            {
                if(new InvalidVersion().TryParse(helloPacket))
                {
                    throw new Exception("Ошибка! Версии клиента и сервера различаются.");
                }
                else
                    throw new Exception("Ошибка! Что-то пошло не так");
            }

            new LoginMessage(socket.GetStream(), username, password).Send();

            byte[] loginReply = ReadPacket();
            ValidCredentials cred = new ValidCredentials();
            if (cred.TryParse(loginReply))
            {
                return cred.ClientId;
            }
            else if (new RegAccount().TryParse(loginReply))//reg new user
            {
                Login(username, password);//relogin
            }
            else if (new InvalidCredentials().TryParse(loginReply))
            {
                return 0;
            }
            return 0;
        }

        private byte[] ReadPacket()
        {
            byte[] packet = new byte[MAX_PACKET_SIZE];

            int readLen = socket.GetStream().Read(packet, 0, packet.Length);

            byte[] payload = new byte[readLen];
            Array.Copy(packet, payload, payload.Length);

            return payload;
        }
    }
}
