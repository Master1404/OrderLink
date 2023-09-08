using OrderLinkN.Models;
using OrderLinkN.Repository;
using OrderLinkN.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrderLinkN.ViewModels
{
    public class MainPageViewModl: BaseViewModel
    {
        private IUserRepository _userRepository;
        private readonly INavigationService _navigationService;
        public ICommand LogOutCommand { get; set; }

        public MainPageViewModl(INavigationService navigationService, IUserRepository userRepository)
        {
            _navigationService = navigationService;
            _userRepository = userRepository;
            LogOutCommand = new Command(LogOut);
        }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private async void LogOut(object obj)
        {
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            // Получение данных пользователя из параметров навигации
            if (parameters.TryGetValue("userId", out int userId))
            {
               
                LoadUserData(userId);
            }
        }

        private async void LoadUserData(int userId)
        {
            var user = await _userRepository.GetSingleByIdAsync<User>(userId);
            if (user != null)
            {
                FullName = user.FullName;
            }
        }
    }
}
