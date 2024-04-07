using prjWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using prjWpfApp.Services;
using prjWpfApp.ViewModels;
using System.ComponentModel;
namespace prjWpfApp.Views
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var descriptor = e.PropertyDescriptor as PropertyDescriptor;

            if (descriptor != null)
            {
                var displayNameAttribute = descriptor.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayNameAttribute != null && !string.IsNullOrEmpty(displayNameAttribute.DisplayName))
                {
                    e.Column.Header = displayNameAttribute.DisplayName;
                }
            }
        }
    }

}
