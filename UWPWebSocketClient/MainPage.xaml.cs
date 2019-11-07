using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace UWPWebSocketClient
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //private string _Status;
        public string Status { get; set; }
        public string Peptide { get; set; }
        public string TubeNum { get; set; }
        public string Time { get; set; }
        public string PumpA { get; set; }
        public string PumpB { get; set; }
        public string PumpC { get; set; }
        public string PumpD { get; set; }
        public string PumpAml { get; set; }
        public string PumpBml { get; set; }
        public string PumpCml { get; set; }
        public string PumpDml { get; set; }
        public string Waste { get; set; }
        public string Holding { get; set; }
        public string Pressure { get; set; }
        public string PressureA { get; set; }
        public string PressureB { get; set; }
        public string PressureC { get; set; }
        public string PressureD { get; set; }
        public string FlowDestination { get; set; }

        public string AU { get; set; }
        public string WaveLength { get; set; }
        ClientWebSocket ws = new ClientWebSocket();
        public MainPage()
        {
            this.InitializeComponent();
        }
        public bool State()
        {
            if (ws.State.ToString() == "Closed" || ws.State.ToString() == "None" || ws.State.ToString() == "Aborted")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private async void ReceiveData(ClientWebSocket websocket)
        {
            while (true && State())
            {
                byte[] bytes = new byte[20000];
                WebSocketReceiveResult result = await ws.ReceiveAsync(bytes, new CancellationToken());
                parse(bytes);
            }
        }
        public void parse(byte[] json)
        {
            string jsonStr = Encoding.UTF8.GetString(json);
            Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
            if (data != null)
            {
                Status = data["status"].ToString();
                Peptide = data["peptide"].ToString();
                TubeNum = data["tubeNum"].ToString();
                Time = Math.Round(Convert.ToDouble(data["time"]), 2).ToString();
                PumpA = Math.Round(Convert.ToDouble(data["pumpA"]), 2).ToString();
                PumpB = Math.Round(Convert.ToDouble(data["pumpB"]), 2).ToString();
                PumpC = Math.Round(Convert.ToDouble(data["pumpC"]), 2).ToString();
                PumpD = Math.Round(Convert.ToDouble(data["pumpD"]), 2).ToString();
                PumpAml = Math.Round(Convert.ToDouble(data["pumpAml"]), 2).ToString();
                PumpBml = Math.Round(Convert.ToDouble(data["pumpBml"]), 2).ToString();
                PumpCml = Math.Round(Convert.ToDouble(data["pumpCml"]), 2).ToString();
                PumpDml = Math.Round(Convert.ToDouble(data["pumpDml"]), 2).ToString();
                Waste = Math.Round(Convert.ToDouble(data["waste"]), 2).ToString();
                Holding = Math.Round(Convert.ToDouble(data["holding"]), 2).ToString();
                Pressure = Math.Round(Convert.ToDouble(data["pressure"]), 2).ToString();
                PressureA = Math.Round(Convert.ToDouble(data["pressureA"]), 2).ToString();
                PressureB = Math.Round(Convert.ToDouble(data["pressureB"]), 2).ToString();
                PressureC = Math.Round(Convert.ToDouble(data["pressureC"]), 2).ToString();
                PressureD = Math.Round(Convert.ToDouble(data["pressureD"]), 2).ToString();
                FlowDestination = Convert.ToInt32(data["flowDestination"]).ToString();
                AU = Math.Round(Convert.ToDouble(data["au"]), 2).ToString();
                WaveLength = Math.Round(Convert.ToDouble(data["wavelength"]), 2).ToString();
                _ = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    this.Bindings.Update();
                });
            }
        }

        private void SendPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!State()) ws = new ClientWebSocket(); ws.ConnectAsync(new Uri("ws://127.0.0.1:4649/add"), new CancellationToken()).Wait();
                ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("pause")), WebSocketMessageType.Text, true, new CancellationToken()).Wait();
            }
            catch
            {
                Debug.WriteLine("Disconnected");
            }
        }

        private void SendStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!State()) ws = new ClientWebSocket(); ws.ConnectAsync(new Uri("ws://127.0.0.1:4649/add"), new CancellationToken()).Wait();
                ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("stop")), WebSocketMessageType.Text, true, new CancellationToken()).Wait();
            }
            catch
            {
                Debug.WriteLine("Disconnected");
            }

        }

        private  void SendData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!State()) ws = new ClientWebSocket(); ws.ConnectAsync(new Uri("ws://127.0.0.1:4649/add"), new CancellationToken()).Wait(2000);
                var purification = new List<Dictionary<string, object>>();
                purification.Add(new Dictionary<string, object> { { "TimeStart", 0 }, { "TimeEnd", 1 }, { "PumpAStart", 40 }, { "PumpAEnd", 60 }, { "PumpBStart", 10 }, { "PumpBEnd", 10 }, { "PumpCStart", 50 }, { "PumpCEnd", 30 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 25 }, { "FlowDestination", 1 } });
                purification.Add(new Dictionary<string, object> { { "TimeStart", 1 }, { "TimeEnd", 2 }, { "PumpAStart", 70 }, { "PumpAEnd", 60 }, { "PumpBStart", 20 }, { "PumpBEnd", 20 }, { "PumpCStart", 10 }, { "PumpCEnd", 20 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 25 }, { "FlowDestination", 2 } });
                purification.Add(new Dictionary<string, object> { { "TimeStart", 2 }, { "TimeEnd", 3 }, { "PumpAStart", 70 }, { "PumpAEnd", 30 }, { "PumpBStart", 20 }, { "PumpBEnd", 50 }, { "PumpCStart", 10 }, { "PumpCEnd", 20 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 400 }, { "FlowDestination", 3 } });
                purification.Add(new Dictionary<string, object> { { "TimeStart", 3 }, { "TimeEnd", 4 }, { "PumpAStart", 30 }, { "PumpAEnd", 0 }, { "PumpBStart", 50 }, { "PumpBEnd", 70 }, { "PumpCStart", 20 }, { "PumpCEnd", 30 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 30 }, { "FlowDestination", 2 } });
                var washcycle = new List<Dictionary<string, object>>();
                washcycle.Add(new Dictionary<string, object> { { "TimeStart", 0 }, { "TimeEnd", 1 }, { "PumpAStart", 40 }, { "PumpAEnd", 60 }, { "PumpBStart", 10 }, { "PumpBEnd", 10 }, { "PumpCStart", 50 }, { "PumpCEnd", 30 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 25 }, { "FlowDestination", 1 } });
                washcycle.Add(new Dictionary<string, object> { { "TimeStart", 1 }, { "TimeEnd", 2 }, { "PumpAStart", 70 }, { "PumpAEnd", 60 }, { "PumpBStart", 20 }, { "PumpBEnd", 20 }, { "PumpCStart", 10 }, { "PumpCEnd", 20 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 25 }, { "FlowDestination", 2 } });
                washcycle.Add(new Dictionary<string, object> { { "TimeStart", 2 }, { "TimeEnd", 3 }, { "PumpAStart", 70 }, { "PumpAEnd", 30 }, { "PumpBStart", 20 }, { "PumpBEnd", 50 }, { "PumpCStart", 10 }, { "PumpCEnd", 20 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 400 }, { "FlowDestination", 3 } });
                washcycle.Add(new Dictionary<string, object> { { "TimeStart", 3 }, { "TimeEnd", 4 }, { "PumpAStart", 30 }, { "PumpAEnd", 0 }, { "PumpBStart", 50 }, { "PumpBEnd", 70 }, { "PumpCStart", 20 }, { "PumpCEnd", 30 }, { "PumpDStart", 0 }, { "PumpDEnd", 0 }, { "FlowRate", 30 }, { "FlowDestination", 2 } });
                var root = new
                {
                    Status = 0,
                    Peptide = 1,
                    Tubes = 36,
                    TubeNum = -1,
                    TubeML = 50,
                    Purification = purification,
                    WashCycle = washcycle
                };
                ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(root))), WebSocketMessageType.Text, true, new CancellationToken()).Wait();
                ReceiveData(ws);
            }
            catch(InvalidOperationException o)
            {
                Debug.WriteLine(o);
            }
            catch
            {

            }
        }
    }
}
