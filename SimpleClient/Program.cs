using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AAS.Common;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace SimpleClient
{
    class Program
    {
        private static HubConnection _connection;

        static async Task Main(string[] args)
        {
            await InitConnection();
            await Menu();


            Console.WriteLine("---");
            Console.ReadLine();
        }

        private static async Task InitConnection()
        {
            _connection = new HubConnectionBuilder().WithUrl("http://localhost:5000/aap").Build();

            _connection.On<Asset>("NewAsset", OnNewAsset);
            _connection.On<string>(ApplicationMethod.Update, OnNewUpdate);
            _connection.On<DateTime>("Connected", OnConnected);

            Console.WriteLine("Connecting to server ...");
             await _connection.StartAsync();
        }

        public static async Task Menu()
        {
            while (true)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("1 - Send POST");
                Console.WriteLine("2 - Get State");
                Console.WriteLine("3 - Get Active Device");
                Console.WriteLine("4 - Disconnect");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        var result = await Post(new Asset
                        {
                            Name = "jerry",
                            Irai = "420420"
                        });

                        Console.WriteLine(result);
                        break;

                    case "2":
                        var state = _connection.State;
                        Console.WriteLine(state);
                        break;

                    case "3":
                        var timer = Stopwatch.StartNew();
                        Console.WriteLine($"Active client: {await Get("/api/Server")}");
                        Console.WriteLine($"Time Taken: {timer.ElapsedMilliseconds} ms");
                        break;
                    case "4":
                        await _connection.StopAsync();
                        break;

                    default:
                        Console.WriteLine("I dont understand.");
                        break;
                }
            }
        }

        private static void OnConnected(DateTime obj)
        {
            Console.WriteLine($"Connected to server. Server time: {obj}");
        }

        private static void OnNewUpdate(string obj)
        {
            Console.WriteLine(obj);
        }

        private static void OnNewAsset(Asset obj)
        {
            Console.WriteLine(obj.Irai);
            Console.WriteLine(obj.Name);
        }

        private static void OnShowMessage(string obj)
        {
            Console.Write("Server say: ");
            Console.WriteLine(obj);
        }

        private static async Task<string> Post(object obj)
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // method address would be like api/callUber:SomePort for example
            var result = await client.PostAsync("api/key", content);
            return await result.Content.ReadAsStringAsync();
        }

        private static async Task<string> Get(string uri)
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            // method address would be like api/callUber:SomePort for example
            var result = await client.GetAsync(uri);
            return await result.Content.ReadAsStringAsync();
        }
    }
}
