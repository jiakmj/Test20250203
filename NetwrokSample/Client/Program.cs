using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {        
        static void Main(string[] args)
        {
            Random random = new Random();
            String[] oper = {"+", "-", "*", "/"};

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //클라이언트가 소켓 뚫음

            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.22"), 4000);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 4000); //Loopback 나자신 사용
            serverSocket.Connect(serverEndPoint); //접속하면 자동으로 bind도 됨

            byte[] buffer; 
            
            String message = $"{random.Next(1, 99)}{oper[random.Next(0, 4)]}{random.Next(1, 99)}";
            buffer = Encoding.UTF8.GetBytes(message);
            int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None); //Send한다고 바로 가는게 아니라 OS가 모아놨다가 보냄

            //블록킹
            byte[] buffer2 = new byte[1024];            
            int RecbLemgth = serverSocket.Receive(buffer2); //받을 때도 OS가 버퍼에 가지고 있다가 받음

            Console.WriteLine(Encoding.UTF8.GetString(buffer2));

            serverSocket.Close();
        }
    }
}
