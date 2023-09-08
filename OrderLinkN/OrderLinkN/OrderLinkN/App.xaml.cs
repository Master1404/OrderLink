using OrderLinkN.ViewModels;
using OrderLinkN.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using OrderLinkN.Repository;

namespace OrderLinkN
{
    public partial class App : PrismApplication
    {
        public static App Instance { get; private set; }
        public static T Resolve<T>() => Current.Container.Resolve<T>();
        public App(
            Prism.IPlatformInitializer initializer = null)
             : base(initializer)
        {
            Instance = this;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPage, RegistrationViewModel>();
            containerRegistry.RegisterForNavigation<FormPage, FormViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModl>();

            // Зарегистрируйте ваши сервисы здесь
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
           // Prism.Navigation.NavigationParameters navigationParams = new Prism.Navigation.NavigationParameters();
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginPage)}"/*, navigationParams*/);
        }
    }
}
