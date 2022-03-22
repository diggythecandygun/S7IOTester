using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7IOTester.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace S7IOTester.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemplateDefPage : ContentPage
    {
        TemplateDefViewModel _viewModel;
        public TemplateDefPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TemplateDefViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}