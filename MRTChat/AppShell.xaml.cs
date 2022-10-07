using MRTChat.Page;

namespace MRTChat;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        // Route registration
        Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
        Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
    }
}
