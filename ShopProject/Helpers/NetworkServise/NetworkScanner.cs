using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise
{
    public static class NetworkScanner
    {
        /// <summary>
        /// Base IpRouter = 192.168.0.
        /// </summary>
        internal static string IpRouter { get; set; } = "192.168.0.";
        /// <summary>
        /// Base minIPAddress = 1
        /// </summary>
        internal static int MinIPAddress { get; set; } = 1;
        /// <summary>
        /// Base minIPAddress = 255
        /// </summary>
        internal static int MaxIPAddress { get; set; } = 255;
        /// <summary>
        /// Base Port = 5000
        /// </summary>
        internal static int Port { get; set; } = 5000;
        /// <summary>
        /// Bae ApiEndpoint = /Api/Settings/Ping
        /// </summary>
        private static string _apiEndpoint = "/api/Settings/Ping";
        /// <summary>
        /// URL Database
        /// </summary>
        internal static string Url { get; set; } = string.Empty;
        /// <summary>
        /// List IP addresses
        /// </summary>
        private static List<string> _ipAddress = new List<string>();

        private static TaskCompletionSource<bool> _taskCompletionSource;
        private static int _pendingPings;

        public static async Task<string> SearchDataBaseURLAsync()
        {
            SearchAllLocalAddress();
            List<Task<string>> tasks = new List<Task<string>>();

            for (int i = 0; i < _ipAddress.Count; i++)
            {

                int tasknumber = i;

                var ip = _ipAddress[i];
                tasks.Add(Task.Run(async () =>
                {

                    if (IsPortOpen(ip, Port))
                    {
                        Url = $"http://{ip}:{Port}{_apiEndpoint}";
                        if (await IsApiReachable(Url))
                        {
                            return $"http://{ip}:{Port}";
                        }
                    }
                    throw new InvalidOperationException($"Завдання task{i} завершилося з помилкою.");
                }));
            }

            try
            {
                var complateTask = await Task.WhenAny(tasks.ToArray());
                return await complateTask;
            }
            catch (Exception ex)
            {
                throw new AggregateException("Усі завдання завершилися з помилкою.", tasks.Select(t => t.Exception).Where(e => e != null));
            }
        }

        private static bool IsPortOpen(string host, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(host, port);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static async Task<bool> IsApiReachable(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void SearchAllLocalAddress()
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();

            Ping p;
            _pendingPings = MaxIPAddress - MinIPAddress;

            for (int i = MinIPAddress; i < MaxIPAddress; i++)
            {
                string ip = IpRouter + i.ToString();
                p = new Ping();
                p.PingCompleted += new PingCompletedEventHandler(p_PingCompleted);
                p.SendAsync(ip, 100, ip);
            }
            _taskCompletionSource.Task.Wait();

        }

        private static void p_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            PingReply reply;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                reply = e.Reply;
                _ipAddress.Add(reply.Address.ToString());
            }
            else if (e.Reply == null)
            {
                reply = e.Reply;
                _ipAddress.Add((reply.Address.ToString()));
            }
            //reply = e.Reply;
            //DisplayReply(reply);

            if (Interlocked.Decrement(ref _pendingPings) == 0)
            {
                _taskCompletionSource.TrySetResult(true);
            }
        }
        private static void DisplayReply(PingReply reply)
        {
            if (reply == null)
                return;

            Console.WriteLine("ping status: {0}", reply.Status);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
            }
        }
    }
}
