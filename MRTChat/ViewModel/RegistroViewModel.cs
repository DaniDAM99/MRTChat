using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRTChat.ViewModel
{
    public partial class RegistroViewModel : ObservableObject
    {

        #region Properties
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;
        #endregion

        [RelayCommand]
        public async Task Register()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Username))
            {
                await Shell.Current.DisplayAlert("¡Atención!", "Introduce el email y la contraseña", "Ok");
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("API-key"));

            try
            {
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password, Username);
                string token = auth.FirebaseToken;
                if (token != null)
                    await Shell.Current.DisplayAlert("", "Usuario registrado correctamente", "Ok");

                await Shell.Current.GoToAsync("..");

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
                return;
            }

        }
    }
}
