using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GetSimulation;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Windows.UI.Core;

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
        public string AU { get; set; }
        public string WaveLength { get; set; }


        GetSimulationData get = new GetSimulationData();
        static Timer RunTimer;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SendData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!get.State()) get = new GetSimulationData("ws://192.168.1.107:4649/add");
                var root = new
                {
                    Status = 0,
                    Peptide = 1,
                    Tubes = 36,
                    TubeNum = 1,
                    TubeML = 50,
                    Purification = new object[]
                       {
                         new
                        {
                            TimeStart = 0,
                            TimeEnd = 1,
                            PumpAStart = 20,
                            PumpAEnd = 40,
                            PumpBStart = 80,
                            PumpBEnd = 60,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 25,
                            FlowDestination = 1,
                        },
                        new
                        {
                            TimeStart = 1,
                            TimeEnd = 2,
                            PumpAStart = 40,
                            PumpAEnd = 45,
                            PumpBStart = 60,
                            PumpBEnd = 55,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 25,
                            FlowDestination = 2,
                        },
                        new
                        {
                            TimeStart = 2,
                            TimeEnd = 3,
                            PumpAStart = 45,
                            PumpAEnd = 55,
                            PumpBStart = 55,
                            PumpBEnd = 45,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 400,
                            FlowDestination = 3,
                        },
                        new
                        {
                            TimeStart = 3,
                            TimeEnd = 4,
                            PumpAStart = 55,
                            PumpAEnd = 60,
                            PumpBStart = 45,
                            PumpBEnd = 40,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 30,
                            FlowDestination = 2,
                        },
                        new
                        {
                            TimeStart = 4,
                            TimeEnd = 5,
                            PumpAStart = 60,
                            PumpAEnd = 55,
                            PumpBStart = 40,
                            PumpBEnd = 45,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 25,
                            FlowDestination = 1,
                        }
                       },
                    WashCycle = new object[]
                       {
                        new
                        {
                            TimeStart = 0,
                            TimeEnd = 5,
                            PumpAStart = 0,
                            PumpAEnd = 40,
                            PumpBStart = 0,
                            PumpBEnd = 60,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 25,
                            FlowDestination = 1,
                        },
                        new
                        {
                            TimeStart = 5,
                            TimeEnd = 10,
                            PumpAStart = 40,
                            PumpAEnd = 45,
                            PumpBStart = 60,
                            PumpBEnd = 55,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 25,
                            FlowDestination = 2,
                        },
                        new
                        {
                            TimeStart = 10,
                            TimeEnd = 75,
                            PumpAStart = 45,
                            PumpAEnd = 55,
                            PumpBStart = 55,
                            PumpBEnd = 45,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 30,
                            FlowDestination = 3,
                        },
                        new
                        {
                            TimeStart = 75,
                            TimeEnd = 80,
                            PumpAStart = 55,
                            PumpAEnd = 60,
                            PumpBStart = 45,
                            PumpBEnd = 40,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 30,
                            FlowDestination = 2,
                        },
                        new
                        {
                            TimeStart = 80,
                            TimeEnd = 85,
                            PumpAStart = 60,
                            PumpAEnd = 55,
                            PumpBStart = 40,
                            PumpBEnd = 45,
                            PumpCStart = 0,
                            PumpCEnd = 0,
                            PumpDStart = 0,
                            PumpDEnd = 0,
                            FlowRate = 25,
                            FlowDestination = 1,
                        }
                       }
                };
                get.SendRunData(root);
                RunTimer = new Timer(_RunTimer, null, 100, 1000);
            }
            catch
            {
                Debug.WriteLine("Disconnected");
            }
        }
        private void _RunTimer (Object o)
        {
            Dictionary<String, Object> data = get.SendRequest();
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
            AU = Math.Round(Convert.ToDouble(data["au"]), 2).ToString();
            WaveLength = Math.Round(Convert.ToDouble(data["wavelength"]), 2).ToString();
            _ = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                this.Bindings.Update();
            });

        }

        private void SendPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!get.State()) get = new GetSimulationData("ws://192.168.1.107:4649/add");
                get.SendPause();
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
                if (!get.State()) get = new GetSimulationData("ws://192.168.1.107:4649/add");
                get.SendStop();
            }
            catch
            {
                Debug.WriteLine("Disconnected");
            }
        }
    }
}
