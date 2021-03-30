using System;
using System.Diagnostics;
using RestSharp;

namespace nWebgame.PlatformTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            int num = 10000;

            for (var x = 0; x < num; x++)
            {
                var name = "131" + rand.Next(1000, 9999) + rand.Next(1000, 9999);

                login(name, name + "10086");
            }

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds / num);
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
