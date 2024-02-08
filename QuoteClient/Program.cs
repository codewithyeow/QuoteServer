using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuoteClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
             
                string ipAddress = "127.0.0.1";
                int port = 8888;

                TcpClient client = new TcpClient(ipAddress, port);
                Console.WriteLine("Connected to server.");

 
                NetworkStream stream = client.GetStream();


                while (true)
                {

                    Console.WriteLine("Available Categories: business, inspiration, stress");
                    Console.WriteLine("Enter the category or 'DISCONNECT' to disconnect:");

                    string category = Console.ReadLine();

                    if (category.Trim().Equals("DISCONNECT"))
                    {
                  
                        byte[] disconnectBytes = Encoding.ASCII.GetBytes(category);
                        stream.Write(disconnectBytes, 0, disconnectBytes.Length);

                       
                        client.Close();
                        break;
                    }

                  
                    string request = "GET_QUOTE: " + category;
                    byte[] requestBytes = Encoding.ASCII.GetBytes(request);
                    stream.Write(requestBytes, 0, requestBytes.Length);

                  
                    byte[] responseBytes = new byte[1024];
                    int bytesRead = stream.Read(responseBytes, 0, responseBytes.Length);
                    string response = Encoding.ASCII.GetString(responseBytes, 0, bytesRead);
                    Console.WriteLine("Received response: " + response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
