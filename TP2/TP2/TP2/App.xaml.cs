using System.Linq;
using System.Net.Http;
using Prism;
using Prism.Ioc;
using SQLite;
using TP2.Helpers;
using TP2.Models.Entities;
using TP2.Services;
using TP2.Services.Interfaces;
using TP2.ViewModels;
using TP2.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TP2
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            SeedTestData();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<WeatherPage, WeatherPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();

            containerRegistry.RegisterSingleton<IHttpClient, HttpClientService>();
            containerRegistry.RegisterSingleton<IApiService, ApiService>();

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<FavoriteRegionPage, FavoriteRegionPageViewModel>();
            containerRegistry.RegisterSingleton<IAuthentificationService, AuthentificationService>();
            containerRegistry.RegisterSingleton<ICryptoService, CryptoService>();
            containerRegistry.RegisterSingleton<ISecureStorageService, SecureStorageService>();

            var databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath("MyDatabase.db3");
            var databaseSqLiteConnection = new SQLiteConnection(databasePath);


            containerRegistry.RegisterInstance(databaseSqLiteConnection);
            containerRegistry.RegisterSingleton<IRepository<User>, SqLiteRepository<User>>();
        }

        private async void SeedTestData()
        {
            var userRepository = Container.Resolve<IRepository<User>>();
            if (userRepository.GetAll().Count() != 0)
                return;

            CryptoService cryptoService = new CryptoService();
            SecureStorageService secureStorageService = new SecureStorageService();
            var salt = cryptoService.GenerateSalt();
            var key = cryptoService.GenerateEncryptionKey();
            var user1 = new User()
            {
                Login = "123",
                HashedPassword = cryptoService.HashSHA512("456", salt),
                PasswordSalt = salt,
            };
            userRepository.Add(user1);
            await secureStorageService.SetEncryptionKeyAsync(userRepository.GetAll().FirstOrDefault(x => x.Login == "123").Id.ToString(), key);
        }
    }
}
