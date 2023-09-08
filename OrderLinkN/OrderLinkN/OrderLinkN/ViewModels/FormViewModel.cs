using OrderLinkN.Models;
using OrderLinkN.Repository;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrderLinkN.ViewModels
{
    public class FormViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private IUserRepository _userRepository;
        public ICommand SubmitCommand { get; set; }
        private int _userId;
        public FormViewModel(INavigationService navigationService, IUserRepository userRepository)
        {
            _navigationService = navigationService;
            _userRepository = userRepository;

            SubmitCommand = new Command(Submit);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _country;
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        private string _areaCode;
        public string AreaCode
        {
            get => _areaCode;
            set => SetProperty(ref _areaCode, value);
        }

        private string _gstNumber;
        public string GSTNumber
        {
            get => _gstNumber;
            set => SetProperty(ref _gstNumber, value);
        }

        private async void Submit(object obj)
        {

            int userId = _userId;
            await LoadUserData(userId);

            
          
            var navigationParams = new NavigationParameters
            {
                  { "userId", _userId }
            };

            await _navigationService.NavigateAsync(nameof(MainPage), navigationParams);

        }
      
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            // Получение данных пользователя из параметров навигации
            if (parameters.TryGetValue("userId", out int userId))
            {
                _userId = userId;
                LoadUserData(userId);
            }
        }

        private async Task LoadUserData(int userId)
        {
            var user = await _userRepository.GetSingleByIdAsync<User>(userId);
            if (user != null)
            {
                // Загрузка данных пользователя в поля
                Address = user.Address;
                State = user.State;
                Country = user.Country;
                AreaCode = user.AreaCode;
                GSTNumber = user.GSTNumber;
            }
        }
    }
}
