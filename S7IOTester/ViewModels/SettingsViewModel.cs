using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
using S7IOTester.Models;

namespace S7IOTester.ViewModels
{
    class SettingsViewModel : ObservableObject
    {
        public ObservableRangeCollection<string> CPUFamily { get;}


        public SettingsViewModel()
        {

            CPUFamily = new ObservableRangeCollection<string>();
            //Initialize CPU Families
            CPUFamily.Add("S7-300");
            CPUFamily.Add("S7-400");
            CPUFamily.Add("S7-1200");
            CPUFamily.Add("S7-1500");

            SaveSettings = new Command(Save);


            //LoadSettings();

        }

        public Command SaveSettings { get; }


        #region Methods
        void LoadSettings()
        {
            /*
            IPAddress = Preferences.Get("IP", string.Empty);
            CPUType = Preferences.Get("CPUFamily", string.Empty);*/

        }
             
        void Save()
        {
            /*if (IPAddress != "") Preferences.Set("IP",IPAddress);
            Preferences.Set("CPUFamily", CPUType);*/

            DatabaseHandler _db = new DatabaseHandler();
            var cpu = new CPUs { Family = CPUType, IP = IPAddress, Name = CPUName };
            try
            {
                _db.InsertCPU(cpu);
                Application.Current.MainPage.DisplayAlert("Info", "CPU succesfully saved", "Ok");
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Info", e.Message, "Ok");
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

        //CPUFamily selected
        string _CPUName = "";
        public string CPUName
        {
            get => _CPUName;
            set => SetProperty(ref _CPUName, value);
        }

        //Device IP Address
        bool _SaveEnable = false;
        public bool SaveEnable
        {
            get => _SaveEnable;
            set => SetProperty(ref _SaveEnable, value);
        }

        #endregion

    }
}
