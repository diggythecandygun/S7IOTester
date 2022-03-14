using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Xamarin.Essentials;

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


            LoadSettings();

        }

        public Command SaveSettings { get; }


        #region Methods
        void LoadSettings()
        {
            IPAddress = Preferences.Get("IP", string.Empty);
            CPUType = Preferences.Get("CPUFamily", string.Empty);
        }
             
        void Save()
        {
            if (IPAddress != "") Preferences.Set("IP",IPAddress);
            Preferences.Set("CPUFamily", CPUType);
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

        #endregion

    }
}
