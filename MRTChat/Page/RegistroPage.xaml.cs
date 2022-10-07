using MRTChat.ViewModel;

namespace MRTChat.Page;

public partial class RegistroPage : ContentPage
{
	public RegistroPage(RegistroViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}