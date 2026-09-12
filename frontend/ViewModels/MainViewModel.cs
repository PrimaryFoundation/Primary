using CommunityToolkit.Mvvm.ComponentModel;

namespace frontend.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private string _greeting = "Hello, World!";

    public string Greeting
    {
        get => _greeting;
        set => SetProperty(ref _greeting, value);
    }
}
