using RssReader.Controls;
using RssReader.ViewModels;
using RssReader.Views;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace RssReader
{
    /// <summary>
    /// The "chrome" layer of the app that provides top-level navigation with
    /// proper keyboarding navigation.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public static AppShell Current = null;

        public MainViewModel ViewModel { get; } = new MainViewModel();

        public AppShell()
        {
            this.InitializeComponent();
            Current = this;


        }

        public Frame AppFrame => AppSh
    }
}
