using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using S7IOTester.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace S7IOTester.ViewModels
{
    class IOsWithTemplateViewModel : ObservableObject
    {

        S7PLC plc;

        public ObservableRangeCollection<string> CPUNames { get; set; }
        public Command Connect { get; }
        public Command Update { get; }

        List<CPUs> CPUList = new List<CPUs>() { };

        public IOsWithTemplateViewModel()
        {
            CPUNames = new ObservableRangeCollection<string>();
            Connect = new Command(CPUConnect);
            Update = new Command(OnAppearing);
            LoadSettings();
            LoadCPUs();

        }



        #region Methods

        public void OnAppearing()
        {
            LoadCPUs();
            LoadSettings();
        }

        void LoadCPUs()
        {
            DatabaseHandler _db = new DatabaseHandler();
            CPUList = _db.SelectCPUs();

            CPUNames.Clear();
            foreach (var cpu in CPUList)
            {
                CPUNames.Add(cpu.Name);
            }
        }

        void LoadSettings()
        {
            CPUs SelectedCPU = CPUList.Find(x => x.Name == SelectedCPUName);
            if (SelectedCPU != null)
            {
                IPAddress = SelectedCPU.IP;
                CPUType = SelectedCPU.Family;

                if (IPAddress != "" && CPUType != "")
                {
                    CanConnect = true;
                    plc = new S7PLC(CPUType, IPAddress);
                }
            }
        }

        void CPUConnect()
        {
            short retval = plc.ConnDisconn();

            if (retval == 1)
            {
                Status = "Connected";
                ConnectButton = "Disconnect";
                UpdateIO();
            }
            else if (retval == 0)
            {
                Status = "Disconnected";
                ConnectButton = "Connect";
            }

            else
            {
                Status = "Error";
                ConnectButton = "Connect";
            }
        }

        bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        async Task UpdateIO()
        {
            while (Status == "Connected")
            {
                await Task.Delay(500);
                //TODO Read variables list
            }
        }

        #endregion


        #region Properties

        //CPU selected
        string _SelectedCPUName = "";
        public string SelectedCPUName
        {
            get => _SelectedCPUName;
            set
            {
                if (value == _SelectedCPUName)
                    return;
                _SelectedCPUName = value;
                OnPropertyChanged();
                LoadSettings();
            }
        }

        //Device IP Address
        string _IPAdress = "";
        public string IPAddress
        {
            get => _IPAdress;
            set => SetProperty(ref _IPAdress, value);
        }

        //CPUFamily selected
        string _CPUType = "";
        public string CPUType
        {
            get => _CPUType;
            set => SetProperty(ref _CPUType, value);
        }

        //Byte address
        string _ByteAddress = "";
        public string ByteAddress
        {
            get => _ByteAddress;
            set => SetProperty(ref _ByteAddress, value);
        }

        //Connection Status
        string _Status = "Disconnected";
        public string Status
        {
            get => _Status;
            set => SetProperty(ref _Status, value);
        }

        //Connection button
        string _ConnectButton = "Connect";
        public string ConnectButton
        {
            get => _ConnectButton;
            set => SetProperty(ref _ConnectButton, value);
        }

        //Connection availability
        bool _CanConnect = false;
        public bool CanConnect
        {
            get => _CanConnect;
            set => SetProperty(ref _CanConnect, value);
        }

        #endregion
    }
}
