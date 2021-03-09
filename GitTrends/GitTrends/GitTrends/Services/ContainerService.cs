using System;
using Autofac;
//using GitHubApiStatus;
//using GitTrends.Mobile.Common;
//using GitTrends.Shared;
//using Plugin.StoreReview;
//using Plugin.StoreReview.Abstractions;
//using Shiny;
//using Shiny.Notifications;
//using Xamarin.Essentials.Implementation;
//using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace GitTrends
{
    public static class ContainerService
    {
        readonly static Lazy<IContainer> _containerHolder = new(CreateContainer);

        public static IContainer Container => _containerHolder.Value;

        static IContainer CreateContainer()
        {
            Device.SetFlags(new[] { "Markup_Experimental", "IndicatorView_Experimental", "AppTheme_Experimental", "SwipeView_Experimental" });

            var builder = new ContainerBuilder();

            //Register Services
            builder.RegisterType<App>().AsSelf().SingleInstance();

            return builder.Build();
        }
    }
}
