using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using MRTChat.Page;
using Newtonsoft.Json;

namespace MRTChat.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {

        public MainViewModel()
        {
        }

        #region Properties
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;
        #endregion

        [RelayCommand]
        public async Task Login()
        {
            if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Email))
            {
                await Shell.Current.DisplayAlert("¡Atención!", "Introduce el email y la contraseña", "Ok");
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("API-key"));

            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email, Password);

                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);

                Preferences.Set("FreshFirebaseToken", serializedContent);
                Preferences.Set("Email", Email);
                Preferences.Set("UID", content.User.LocalId);
                Preferences.Set("Username", content.User.DisplayName);

                Email = "";
                Password = "";

                await Shell.Current.GoToAsync(nameof(ChatPage));

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }

        }

        [RelayCommand]
        public async Task GoToRegister()
        {
            await Shell.Current.GoToAsync(nameof(RegistroPage));
        }

        
    }
}
