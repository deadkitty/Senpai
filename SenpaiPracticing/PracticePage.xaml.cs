using Nyantilities;
using Nyantilities.Core;
using Nyantilities.ViewModel;
using SenpaiBase.EnumerationTypes;
using SenpaiUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SenpaiPracticing
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PracticePage : NyaPage
    {
        #region Fields

        private PracticeViewModel practiceVM;

        #endregion

        #region Constructor

        public PracticePage()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Page Navigation

        protected override void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            DebugHelper.WriteLine<PracticePage>();
            
            ViewModel = ViewModelProvider.GetViewModel<PracticeViewModel>();

            practiceVM = ViewModel as PracticeViewModel;
            practiceVM.ActiveItemChanged += PracticeViewModel_ActiveItemChanged;

            //practiceViewModel.EditFinished += PracticePage_EditFinished;
            //EPracticeState.CurrentChanged += EPracticeState_CurrentChanged;

            FocusRoot.Focus(FocusState.Pointer);

            base.NavigationHelper_LoadState(sender, e);
        }

        //private void PracticePage_EditFinished(object sender, EventArgs e)
        //{
        //    FocusRoot.Focus(FocusState.Pointer);
        //}

        protected override void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //EPracticeState.CurrentChanged -= EPracticeState_CurrentChanged;
            //(ViewModel as PracticeViewModel).EditFinished -= PracticePage_EditFinished;
            
            practiceVM.ActiveItemChanged -= PracticeViewModel_ActiveItemChanged;

            base.NavigationHelper_SaveState(sender, e);
        }

        #endregion

        #region ActiveItem changed
        
        private void PracticeViewModel_ActiveItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            String example = practiceVM.ActiveItem.Example;

            ExampleTextblock1.Inlines.Clear();
            ExampleTextblock2.Inlines.Clear();

            if (example?.Length > 0) 
            {
                TextUtilities.SetExampleText(ExampleTextblock1, ExampleTextblock2, example);
            }
        }

        #endregion
    }
}