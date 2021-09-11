using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using BeetleX.Http.Clients;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace nWebgame.PlatformTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Thread> threads = new List<Thread>();

            // 10w 次创建
            int 线程数量 = 15;
            int 新增数量 = 10000;

            HashSet<string> allNames = new HashSet<string>(线程数量 * 新增数量 + 10);

            var rand = new Random();
            int addNum = 0;

            while (allNames.Count < 线程数量 * 新增数量)
            {
                var name = "13" + rand.Next(1, 9) + rand.Next(1000, 9999) + rand.Next(1000, 9999);
                addNum++;
                if (!allNames.Contains(name))
                    allNames.Add(name);
            }

            var ar = allNames.ToArray();

            Stopwatch allTime = Stopwatch.StartNew();

            for (var i = 0; i < 线程数量; i++)
            {
                var newNames =  ar.Skip(i * 新增数量).Take(新增数量).ToArray();
                Thread thread = new Thread(o =>
                {
                    Runtest(新增数量, newNames);
                });
                thread.Start();
                threads.Add(thread);
            }

            Thread.Sleep(10000);
            for(var i =0;i < threads.Count; i++)
            {
                threads[i].Join();
            }

            allTime.Stop();

            Console.WriteLine("allTime: {0} min qps:{1}", 
                allTime.Elapsed.TotalMinutes, 
                (int)((线程数量 * 新增数量) / allTime.Elapsed.TotalSeconds));
        }

        private static int firstThreadId = 0;

        private static int randIndex = new Random().Next(1000);


        static void Runtest(int num, string[] names)
        {
            Random rand = new Random(randIndex += 10086);

            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();

            Stopwatch watch2 = Stopwatch.StartNew();
            watch2.Start();

            if (firstThreadId == 0)
            {
                firstThreadId = Thread.CurrentThread.ManagedThreadId;
            }

            Console.WriteLine("begin [threadId: {0} ]", Thread.CurrentThread.ManagedThreadId);
            int success = 0;
            for (var x = 1; x <= num; x++)
            {
                string name;
                if (names != null && x - 1 < names.Length)
                {
                    name = names[x - 1];
                }
                else
                {
                    name = "13" + rand.Next(1, 9) + rand.Next(1000, 9999) + rand.Next(1000, 9999);
                }

                if (login2(name, name + "10086"))
                    success++;

                if (x % 1000 == 0 && firstThreadId == Thread.CurrentThread.ManagedThreadId)
                {
                    Console.WriteLine("[threadId: {3} ] num:{2} {0:F2} ms success:{1}", watch2.ElapsedMilliseconds / (double)1000, success, num, Thread.CurrentThread.ManagedThreadId);
                    watch2.Restart();
                    System.GC.Collect();
                }
            }

            watch.Stop();
            Console.WriteLine("{0:F2} ms success:{1}", watch.ElapsedMilliseconds / (double)num, success);
        }

        static bool login(string userName, string password)
        {
            var url = "http://localhost:5000/api/Account/CreateAccount";

            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);
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
            else
            {
                Console.Write(ret.StatusCode);
            }
            return false;
        }

        static bool login2(string userName, string password)
        {
            HttpJsonClient client = new HttpJsonClient("http://localhost:5000");
            
            client.SetBody(new
            {
                Name = userName,
                Password = password
            });

            var result = client.Post("/api/Account/CreateAccount").Result;
            JToken rdata = result.GetResult<JToken>();
            if(rdata["code"].ToString() == "0")
            {
                return true;
            }
            //else
            //{
            //    Console.Write(rdata["errorMessage"]);
            //}

            return false;
        }
    }
}
