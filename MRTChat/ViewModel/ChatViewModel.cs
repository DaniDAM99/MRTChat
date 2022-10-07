using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using MRTChat.Models;
using System.Collections.ObjectModel;

namespace MRTChat.ViewModel
{
    public partial class ChatViewModel : ObservableObject
    {

        private FirebaseClient _client;

        public ChatViewModel()
        {
            _client = new FirebaseClient("Real-time database url");
            Mensajes = new ObservableCollection<Mensaje>();
        }

        #region Properties
        [ObservableProperty]
        private ObservableCollection<Mensaje> _mensajes;

        [ObservableProperty]
        private string _mensaje;
        #endregion

        public void GetMessages()
        {
            var collection = _client.Child("Messages")
                .AsObservable<Mensaje>().Subscribe((dbevent) =>
                {
                    try
                    {
                        if (dbevent != null)
                        {
                            var mensaje = dbevent.Object;
                            switch (dbevent.EventType)
                            {
                                case FirebaseEventType.Delete:
                                    var i = Mensajes.IndexOf(mensaje);
                                    if (i != -1)
                                        Mensajes.RemoveAt(i);
                                    break;
                                case FirebaseEventType.InsertOrUpdate:
                                    if (Mensajes.ToList().Exists(x => x.Id == mensaje.Id)) // Update
                                    {
                                        var msg = Mensajes.ToList().Find(x => x.Id == mensaje.Id);
                                        var index = Mensajes.IndexOf(msg);
                                        Mensajes.RemoveAt(index);
                                        Mensajes.Insert(index, mensaje);
                                    }
                                    else // Insert
                                    {
                                        Mensajes.Add(dbevent.Object);
                                    }
                                    break;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Shell.Current.DisplayAlert(ex.Message, ex.StackTrace, "Ok");
                    }
                });
        }

        [RelayCommand]
        private async Task SendMessage()
        {
            var m = new Mensaje { Data = Mensaje };
            var mensaje = await _client.Child("Messages").PostAsync<Mensaje>(m);

            if (mensaje == null)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha podido enviar el mensaje", "Ok");
            }
            else
                Mensaje = "";


        }
    }
}
