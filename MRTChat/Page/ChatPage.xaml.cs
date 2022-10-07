using MRTChat.ViewModel;

namespace MRTChat.Page;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		(BindingContext as ChatViewModel).GetMessages();
		base.OnAppearing();
	}
}