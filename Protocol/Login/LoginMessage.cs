using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace dkab.Protocol.Login
{
    class LoginMessage : OutPacket
    {
        public LoginMessage(NetworkStream s, string username, string password) : base(s)
        {
            SHA512 hash = SHA512.Create();
            data = Encoding.ASCII.GetBytes(
                string.Format(
                    "login {0} {1}\r\n",
                    username,
                    BitConverter.ToString(hash.ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", "").ToLower())
                );
        }
    }
}
