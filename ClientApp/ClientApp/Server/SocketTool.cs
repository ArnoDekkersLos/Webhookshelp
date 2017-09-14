using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Server
{
    class SocketTool
    {
        private string socketServerHostname = "localhost";
        private string socketServerIp = "127.0.01";
        private int socketServerPort = 888;
        private TcpClient client;

        public SocketTool()
        {
            client = new TcpClient(socketServerIp, socketServerPort);
            SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
            try
            {
                sslStream.AuthenticateAsClient(socketServerHostname);
            }
            catch (Exception e)
            {
                client.Dispose();
                Console.WriteLine(e.ToString());
                client.Close();
            }
        }

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            //return false;
            //Force ssl certyficates as correct
            return false;
        }
    }
}
