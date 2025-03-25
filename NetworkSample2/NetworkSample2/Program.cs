using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{   
    class Program
    {
        static Socket listenSocket;

        static List<Socket> clientSockets = new List<Socket>();
        //static List<Thread> threadManager = new List<Thread>();

        static object _lock = new object();

        static void AcceptThread()
        {
            
            TcpClient tcp = new TcpClient();
            StreamWriter wr = new StreamWriter(tcp.GetStream());
            wr.Write("dlrpanjwl");

            while (true)
            {
                Socket clientSocket = listenSocket.Accept();
                lock (_lock)
                {
                    clientSockets.Add(clientSocket);
                }
                Console.WriteLine($"Connect client : {clientSocket.RemoteEndPoint}");

                Thread workThread = new Thread(new ParameterizedThreadStart(WorkThread));
                workThread.IsBackground = true;
                workThread.Start(clientSocket);
                //threadManager.Add(workThread);
            }            
        }

        static void WorkThread(Object clientObjectSocket)
        {            
            Socket clientSocket = clientObjectSocket as Socket;
            clientSocket.AcceptAsync();

            while (true)
            {
                try
                {
                    byte[] headerBuffer = new byte[2];
                    int RecvLength = clientSocket.Receive(headerBuffer, 2, SocketFlags.None);
                    if (RecvLength > 0)
                    {
                        short packetlength = BitConverter.ToInt16(headerBuffer, 0);
                        packetlength = IPAddress.NetworkToHostOrder(packetlength);


                        byte[] dataBuffer = new byte[4096];
                        RecvLength = clientSocket.Receive(dataBuffer, packetlength, SocketFlags.None);
                        string JsonString = Encoding.UTF8.GetString(dataBuffer);
                        Console.WriteLine(JsonString);

                        JObject clientData = JObject.Parse(JsonString);

                        string message = "{ \"message\" : \"" + clientData.Value<String>("messega") + "\"}";
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                        headerBuffer = BitConverter.GetBytes(length);

                        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);
                        lock (_lock)
                        {
                            foreach (Socket sendSocket in clientSockets)
                            {
                                int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                            }
                        }
                    }
                    else //0보다 작다면
                    {

                        string message = "{\"message\" : \" Disconnect : " + clientSocket.RemoteEndPoint + "\"}";
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                        headerBuffer = BitConverter.GetBytes(length);

                        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                        clientSocket.Close();
                        lock (_lock)
                        {
                            clientSockets.Remove(clientSocket);

                            foreach (Socket sendSocket in clientSockets)
                            {
                                int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                            }
                        }
                        return; //쓰레드 종료
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine($"Error : {e.Message} {clientSocket.RemoteEndPoint}");

                    string message = "{\"message\" : \" Disconnect : " + clientSocket.RemoteEndPoint + "\"}";
                    byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                    ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                    byte[] headerBuffer = new byte[2];

                    headerBuffer = BitConverter.GetBytes(length);

                    byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                    Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                    Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                    clientSocket.Close();
                    lock (_lock)
                    {
                        clientSockets.Remove(clientSocket);

                        foreach (Socket sendSocket in clientSockets)
                        {
                            int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                        }
                    }
                }
            }            
        }

        static void Main(string[] args)
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //통로 만들고

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000); //기기랑 연결

            listenSocket.Bind(listenEndPoint); //대기

            listenSocket.Listen(10);

            Thread acceptThread = new Thread(new ThreadStart(AcceptThread));

            acceptThread.IsBackground = true;
            acceptThread.Start();
            acceptThread.Join();

            //acceptThread.Resume();

            listenSocket.Close();
        }
    }
}