using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuoteServer
{
    internal class Program
    {
        static Dictionary<string, List<string>> quotesByCategory = new Dictionary<string, List<string>>
        {
            { "business", new List<string> {
                "Success is not just about the destination, but the journey we undertake to get there.", 
                "Play by the rules, but be ferocious.",
                "Business opportunities are like buses, there’s always another one coming" }
            },

            { "inspiration", new List<string> { "When you have a dream, you've got to grab it and never let go", 
                "Nothing is impossible. The word itself says 'I'm possible!",
                "Just don't give up trying to do what you really want to do. Where there is love and inspiration, I don't think you can go wrong" }
            },
            { "stress", new List<string> { "Stress should be a powerful driving force, not an obstacle",
                "Calmness is the cradle of power", 
                "The greatest weapon against stress is our ability to choose one thought over another" }
            }
        };

        static Dictionary<string, DateTime> connectedClients = new Dictionary<string, DateTime>();

        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
               
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 8888;

                
                server = new TcpListener(ipAddress, port);

             
                server.Start();

                Console.WriteLine("Quote Generator Server started.");

                while (true)
                {
                    Console.WriteLine("Waiting for a client connection...");

                 
                    TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("Client connected: " + client.Client.RemoteEndPoint);
                    LogConnection(client.Client.RemoteEndPoint.ToString());
                    connectedClients.Add(client.Client.RemoteEndPoint.ToString(), DateTime.Now);
                    HandleClientRequest(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("Server stopped. Press any key to exit.");
            Console.ReadKey();
        }

        static void HandleClientRequest(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                   
                    string clientRequest = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    Console.WriteLine("Client request: " + clientRequest);

                    if (clientRequest.StartsWith("GET_QUOTE"))
                    {
                       
                        string[] requestParts = clientRequest.Split(':');
                        if (requestParts.Length == 2)
                        {
                            string category = requestParts[1].Trim();

                          
                            if (quotesByCategory.ContainsKey(category))
                            {
                                string randomQuote = GetRandomQuote(category);
                                byte[] quoteBytes = Encoding.ASCII.GetBytes(randomQuote);
                                stream.Write(quoteBytes, 0, quoteBytes.Length);
                            }
                            else
                            {
                                string errorMessage = "Invalid category.";
                                byte[] errorBytes = Encoding.ASCII.GetBytes(errorMessage);
                                stream.Write(errorBytes, 0, errorBytes.Length);
                            }
                        }
                        else
                        {
                            string errorMessage = "Invalid request format.";
                            byte[] errorBytes = Encoding.ASCII.GetBytes(errorMessage);
                            stream.Write(errorBytes, 0, errorBytes.Length);
                        }
                    }
                    else if (clientRequest.Trim().Equals("DISCONNECT"))
                    {
                        Console.WriteLine("Client disconnected: " + client.Client.RemoteEndPoint);
                        LogDisconnection(client.Client.RemoteEndPoint.ToString());
                        connectedClients.Remove(client.Client.RemoteEndPoint.ToString());
                        client.Close();
                        break;
                    }
                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static string GetRandomQuote(string category)
        {
            Random random = new Random();
            int index = random.Next(quotesByCategory[category].Count);
            return quotesByCategory[category][index];
        }

        static void LogConnection(string clientEndPoint)
        {
            Console.WriteLine("Logging connection: " + clientEndPoint);
            Console.WriteLine("Connected: " + DateTime.Now);
        }

        static void LogDisconnection(string clientEndPoint)
        {
            Console.WriteLine("Logging disconnection: " + clientEndPoint);
            Console.WriteLine("Disconnected: " + DateTime.Now);
        }
    }
}
