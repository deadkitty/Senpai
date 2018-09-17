using Nyantilities;
using Nyantilities.Core;
using SenpaiBase.EnumerationTypes;
using SenpaiModel;
using SenpaiUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Senpai
{
    sealed partial class App : NyaApp
    {
        #region Constructor

        public App()
            : base()
        {
            InitializeComponent();

            UnhandledException += App_UnhandledException;

            PracticeTimer.UpdateCurrentRound();

            //AppSettings.Initialize();
            DataManager.Initialize();
        }

        #endregion

        #region App Resuming/Suspending
        
        protected override void OnSuspending(object sender, SuspendingEventArgs args)
        {
            DataManager.SaveChanges();
            DataManager.Uninitialize();
        }

        protected override void OnResuming(object sender, object args)
        {
            
        }

        #endregion

        #region App Launching

        protected override void OnNavigateTo(Frame rootFrame, LaunchActivatedEventArgs args)
        {
            rootFrame.Navigate(typeof(MainPage), args.Arguments);
        }

        #endregion

        #region Unhandled Exception

        private void App_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            DebugHelper.WriteLine<App>(e.Message);

            //TODO: cleanup before app crashes
            DataManager.SaveChanges();

            SuspensionManager.UnregisterFrame(Window.Current.Content as Frame);

            throw e.Exception;
        }

        #endregion
    }
}
