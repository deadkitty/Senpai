using Nyantilities;
using Nyantilities.Core;
using Nyantilities.ViewModel;
using SenpaiBase.EnumerationTypes;
using SenpaiModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : NyaPage
    {
        #region Constructor

        public MainPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Page Navigation

        protected override void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            DebugHelper.WriteLine<MainPage>();

            ViewModel = ViewModelProvider.GetViewModel<MainViewModel>();
            
            FocusRoot.Focus(FocusState.Pointer);

            base.NavigationHelper_LoadState(sender, e);
        }

        protected override void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            base.NavigationHelper_SaveState(sender, e);
        }

        #endregion

        #region Events

        private void LessonsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainViewModel vm = ViewModel as MainViewModel;
            
            vm.LessonSelected(LessonsListView.SelectedItems.FirstOrDefault() as Lesson);
            vm.SelectedLessons = LessonsListView.SelectedItems.Cast<Lesson>().ToList();
        }

        private void SearchTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainViewModel vm = ViewModel as MainViewModel;

            vm.InitializeSearch(SearchTextbox.Text);
        }

        #endregion
    }
}