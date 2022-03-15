using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using S7IOTester.Models;
using System.Threading.Tasks;

namespace S7IOTester.ViewModels
{
    class IOsViewModel : ObservableObject
    {
        S7PLC plc;

        public IOsViewModel()
        {

            Connect = new Command(CPUConnect);
            Update = new Command(LoadSettings);
            LoadSettings();

        }

        public void OnAppearing()
        {
            LoadSettings();
        }

        public Command Connect { get; }
        public Command Update { get; }

        #region Methods

        public void LoadSettings()
        {
            IPAddress = Preferences.Get("IP", string.Empty);
            CPUType = Preferences.Get("CPUFamily", string.Empty);
            if (IPAddress != "" && CPUType != "")
            {
                CanConnect = true;
                plc = new S7PLC(CPUType, IPAddress);
            }
            else CanConnect = false;
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
                X0 = false;
                X1 = false;
                X2 = false;
                X3 = false;
                X4 = false;
                X5 = false;
                X6 = false;
                X7 = false;
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
                byte IOByte = plc.ReadByte(ByteAddress)[0];

                if (IsBitSet(IOByte, 0)) X0 = true;
                else X0 = false;

                if (IsBitSet(IOByte, 1)) X1 = true;
                else X1 = false;

                if (IsBitSet(IOByte, 2)) X2 = true;
                else X2 = false;

                if (IsBitSet(IOByte, 3)) X3 = true;
                else X3 = false;

                if (IsBitSet(IOByte, 4)) X4 = true;
                else X4 = false;

                if (IsBitSet(IOByte, 5)) X5 = true;
                else X5 = false;

                if (IsBitSet(IOByte, 6)) X6 = true;
                else X6 = false;

                if (IsBitSet(IOByte, 7)) X7 = true;
                else X7 = false;

            }
        }

        #endregion


        #region Properties

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

        //X0 Status
        bool _X0 = false;
        public bool X0
        {
            get => _X0;
            set => SetProperty(ref _X0, value);
        }

        //X0 Status
        bool _X1 = false;
        public bool X1
        {
            get => _X1;
            set => SetProperty(ref _X1, value);
        }

        //X2 Status
        bool _X2 = false;
        public bool X2
        {
            get => _X2;
            set => SetProperty(ref _X2, value);
        }

        //X3 Status
        bool _X3 = false;
        public bool X3
        {
            get => _X3;
            set => SetProperty(ref _X3, value);
        }

        //X4 Status
        bool _X4 = false;
        public bool X4
        {
            get => _X4;
            set => SetProperty(ref _X4, value);
        }

        //X5 Status
        bool _X5 = false;
        public bool X5
        {
            get => _X5;
            set => SetProperty(ref _X5, value);
        }

        //X6 Status
        bool _X6 = false;
        public bool X6
        {
            get => _X6;
            set => SetProperty(ref _X6, value);
        }

        //X7 Status
        bool _X7 = false;
        public bool X7
        {
            get => _X7;
            set => SetProperty(ref _X7, value);
        }

        #endregion
    }
}
