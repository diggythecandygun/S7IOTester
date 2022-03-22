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
    class TemplateDefViewModel : ObservableObject
    {

        public ObservableRangeCollection<string> VarTypes { get; }
        public ObservableRangeCollection<string> DataTypes { get; }
        public ObservableRangeCollection<string> CPUNames { get; set; }
        List<CPUs> CPUList = new List<CPUs>() { };

        public Command addTemplate { get; }

        public TemplateDefViewModel()
        {
            VarTypes = new ObservableRangeCollection<string>();
            DataTypes = new ObservableRangeCollection<string>();
            CPUNames = new ObservableRangeCollection<string>();

            //Initialize CPU Families
            VarTypes.Add("DB");
            VarTypes.Add("Merker");
            VarTypes.Add("I");
            VarTypes.Add("Q");

            //Initialize DataTypes
            DataTypes.Add("BOOL");
            DataTypes.Add("INT");
            DataTypes.Add("REAL");

            addTemplate = new Command(AddTemplate);

        }

        #region Methods

        public void OnAppearing()
        {
            LoadCPUs();
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

        void AddTemplate()
        {

        }

        #endregion

        #region Properties

        //Selected var type
        string _SelectedVarType = "";
        public string SelectedVarType
        {
            get => _SelectedVarType;
            set => SetProperty(ref _SelectedVarType, value);
        }

        //Selected var type
        string _SelectedDataType = "";
        public string SelectedDataType
        {
            get => _SelectedDataType;
            set => SetProperty(ref _SelectedDataType, value);
        }

        //CPU selected
        string _SelectedCPUName = "";
        public string SelectedCPUName
        {
            get => _SelectedCPUName;
            set => SetProperty(ref _SelectedCPUName, value);
        }




        #endregion


    }
}
