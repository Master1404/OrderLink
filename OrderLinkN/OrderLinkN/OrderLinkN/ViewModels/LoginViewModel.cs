
using OrderLinkN.Models;
using OrderLinkN.Repository;
using OrderLinkN.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OrderLinkN.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private IUserRepository _userRepository;
        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        public LoginViewModel(INavigationService navigationService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;
            SignInCommand = new Command(SignIn);
            SignUpCommand = new Command(SignUp);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        private async void SignIn()
        {
            var users = await _userRepository.GetAllAsync<User>();
            var user = users.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
                var navigationParams = new NavigationParameters
            {
                { "userId", user.Id }
            };
                await _navigationService.NavigateAsync(nameof(MainPage), navigationParams);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User not registered", "OK");
            }
        }

        private async void SignUp()
        {
            //await _navigationService.NavigateAsync(nameof(MainPage));
            await _navigationService.NavigateAsync(nameof(RegistrationPage));
            //await _navigationService.NavigateAsync(new Uri(nameof(RegistrationPage), UriKind.Relative));
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
         
        }
    }
}
