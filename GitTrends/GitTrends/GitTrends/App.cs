using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GitTrends
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {

        }

        protected override async void OnStart()
        {
            base.OnStart();

            var appInitializationCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
        }
    }
}
