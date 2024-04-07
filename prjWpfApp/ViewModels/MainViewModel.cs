using GalaSoft.MvvmLight.Command;
using prjWpfApp.Models;
using prjWpfApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prjWpfApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // 發票API
        private readonly InvoiceApi _invoiceApi = new InvoiceApi();
        public event PropertyChangedEventHandler PropertyChanged;

        // 發票資料集合
        private ObservableCollection<InvoiceDTO> _invoices = new ObservableCollection<InvoiceDTO>();
        public ObservableCollection<InvoiceDTO> Invoices
        {
            get => _invoices;
            set => SetProperty(ref _invoices, value); // 使用SetProperty方法在賦值時觸發PropertyChanged事件
        }

        // 目前選擇的城市
        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set => SetProperty(ref _selectedCity, value); // 使用SetProperty方法在賦值時觸發PropertyChanged事件
        }

        // 可選擇的城市列表
        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities)); // 直接觸發PropertyChanged事件
            }
        }

        // 設定要求的資料筆數，最多不超過100筆
        private int _limit = 10;
        public int Limit
        {
            get => _limit;
            set => SetProperty(ref _limit, Math.Min(value, 100)); // 使用SetProperty方法在賦值時觸發PropertyChanged事件並限制最大值
        }

        // 命令，用於觸發資料獲取
        public ICommand FetchDataCommand { get; }

        public MainViewModel()
        {
            FetchDataCommand = new RelayCommand(async () => await FetchData());
            Cities = new ObservableCollection<string>()
        {
            "基隆市", "臺北市", "新北市", "桃園市", "新竹市",
            "新竹縣", "苗栗縣", "臺中市", "彰化縣", "南投縣",
            "雲林縣", "嘉義市", "嘉義縣", "臺南市", "高雄市",
            "屏東縣", "宜蘭縣", "花蓮縣", "臺東縣", "澎湖縣",
            "金門縣", "連江縣"
        };
            SelectedCity = Cities.FirstOrDefault(); // 預設選擇第一個城市為選擇的城市
        }

        // 獲取資料的異步方法
        private async Task FetchData()
        {
            if (!string.IsNullOrWhiteSpace(SelectedCity) && Limit > 0)
            {
                var invoiceData = await _invoiceApi.GetInvoiceDataAsync(SelectedCity, Limit);
                Invoices.Clear(); // 先清空現有資料
                foreach (var item in invoiceData)
                {
                    Invoices.Add(item); // 添加新獲取的資料
                }
            }
        }

        // 觸發PropertyChanged事件的輔助方法
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 設定屬性值並在變化時觸發PropertyChanged事件的輔助方法
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

}
