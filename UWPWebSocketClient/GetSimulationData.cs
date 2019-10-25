using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using Newtonsoft.Json;

namespace GetSimulation
{
    public class GetSimulationData
    {
        ClientWebSocket ws = new ClientWebSocket();
        public GetSimulationData()
        {
        }
        public GetSimulationData(string url)
        {
            //ws.Options.Credentials = new NetworkCredential("Jason", "password", "/add");
           
            ws.ConnectAsync(new Uri(url), new CancellationToken()).Wait();
        }
        public Dictionary<String, Object> SendRequest()
        {
            byte[] bytes = new byte[1024];
            ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("request")), WebSocketMessageType.Text, true, new CancellationToken()).Wait();
            ws.ReceiveAsync(bytes, new CancellationToken()).Wait();
            return parse(bytes);
        }
        public void SendPause()
        {
            ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("pause")), WebSocketMessageType.Text, true, new CancellationToken()).Wait();
        }
        public void SendStop()
        {
            ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("stop")), WebSocketMessageType.Text, true, new CancellationToken()).Wait();
        }
        public void SendRunData(object data)
        {
            ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data))), WebSocketMessageType.Text, true, new CancellationToken()).Wait();
        }
        public bool State()
        {
            if (ws.State.ToString() == "Closed" || ws.State.ToString() == "None" )
            {
                return false;
            }else
            {
                return true;
            }
        }
        public static Dictionary<String, Object> parse(byte[] json)
        {
            string jsonStr = Encoding.UTF8.GetString(json);
            return JsonConvert.DeserializeObject<Dictionary<String, Object>>(jsonStr);
        }
    }
    
}
