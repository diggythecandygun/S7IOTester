using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using S7IOTester.Models;

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
