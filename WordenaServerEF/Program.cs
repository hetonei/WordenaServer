using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WordenaData;
using WordenaServerEF.CommandProcessing;

namespace WordenaServerEF
{

    internal class Program
    {
        private const int Port = 11000;

        private static void Main()
        {
            Console.WriteLine("Запуск сервера...");
            string Host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(Host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, Port);

            Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Синхронизация с БД...");
            TableManager.DbSynchronize();
            Console.WriteLine("Синхронизация выполнена!");
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(100000);
                Console.WriteLine("Сервер запущен по адресу {0}:{1}!", IPAddress.Any, Port);
                while (true)
                {
                    Socket handler = sListener.Accept();
                    string data = null;
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    Console.Write("ПОЛУЧЕНО: " + data + "\n\n");

                    Separator.Separation(data);
                    CommandDictionary cd = new CommandDictionary(new Commands());
                    string reply = cd.RunCommand(Separator.Command, Separator.Data.ToString());

                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);
                    Console.Write("ОТПРАВЛЕНО: " + reply + "\n\n");

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}