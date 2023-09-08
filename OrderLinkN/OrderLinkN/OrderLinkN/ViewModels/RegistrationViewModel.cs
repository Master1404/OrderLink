using OrderLinkN.Models;
using OrderLinkN.Repository;
using OrderLinkN.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OrderLinkN.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {

        private readonly INavigationService _navigationService;
        private IUserRepository _userRepository;
        public ICommand SignInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public RegistrationViewModel(INavigationService navigationService, IUserRepository userRepository)
        {
            _navigationService = navigationService;
             _userRepository = userRepository;
             SignInCommand = new Command(SignIn);
             SignUpCommand = new Command(SignUp);
        }
     
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

       
        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);

        }

        //public bool ArePasswordsMatching => Password == ConfirmPassword;

        private async void SignUp(object obj)
        {
            if (AreFieldsValid()) // Проверка валидности полей
            {
                // Создать объект пользователя на основе введенных данных
                var newUser = new User
                {
                    FullName = FullName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    CompanyName = CompanyName,
                    Password = Password
                };

                // Сохранить нового пользователя в базе данных
                await _userRepository.AddAsync(newUser);


                int newUserId = newUser.Id;

                // Создать параметр навигации с передачей идентификатора пользователя
                var navigationParams = new NavigationParameters
                {
                     { "userId", newUserId }
                };
                await _navigationService.NavigateAsync(nameof(FormPage), navigationParams);
            }
            else
            {
                // Вывести сообщение о неверных данных
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid data. Please check the fields.", "OK");
            }
        }

        private bool AreFieldsValid()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(CompanyName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            if (Password != ConfirmPassword)
            {
                return false;
            }

            return true;
        }

      /*  private bool IsValidEmail(string email)
        {
            // Пример регулярного выражения для проверки email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Пример регулярного выражения для проверки номера телефона (только цифры)
            string pattern = @"^\d+$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }*/
        private async void SignIn(object obj)
        {
            await _navigationService.NavigateAsync(nameof(LoginPage));
        }
    }
}
