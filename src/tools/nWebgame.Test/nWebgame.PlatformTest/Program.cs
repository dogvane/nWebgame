using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using RestSharp;

namespace nWebgame.PlatformTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();

            for (var i = 0; i < 1; i++)
            {
                Thread thread = new Thread(o =>
                {
                    Runtest(2000);
                });
                thread.Start();
                threads.Add(thread);
            }

            Thread.Sleep(10000);
            for(var i =0;i < threads.Count; i++)
            {
                threads[i].Join();
            }
        }

        static void Runtest(int num)
        {
            Random rand = new Random();
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();

            for (var x = 0; x < num; x++)
            {
                var name = "131" + rand.Next(1000, 9999) + rand.Next(1000, 9999);

                login(name, name + "10086");
            }

            watch.Stop();
            Console.WriteLine("{0:F2} ms", watch.ElapsedMilliseconds / (double)num);
        }

        static bool login(string userName, string password)
        {
            var url = "http://localhost:5000/api/Account/CreateAccount";

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);
            // request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                Name = userName,
                Password = password
            });

            var ret = client.Execute(request);
            if (ret.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Console.WriteLine(ret.Content);
                return true;
            }
            return false;
        }
    }
}
