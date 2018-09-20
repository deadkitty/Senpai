using Nyantilities;
using Nyantilities.Core;
using Nyantilities.ViewModel;
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

            PracticeViewModel practiceViewModel = ViewModel as PracticeViewModel;

            practiceViewModel.EditFinished += PracticePage_EditFinished;
            //EPracticeState.CurrentChanged += EPracticeState_CurrentChanged;

            FocusRoot.Focus(FocusState.Pointer);

            base.NavigationHelper_LoadState(sender, e);
        }

        private void PracticePage_EditFinished(object sender, EventArgs e)
        {
            FocusRoot.Focus(FocusState.Pointer);
        }

        protected override void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //EPracticeState.CurrentChanged -= EPracticeState_CurrentChanged;
            (ViewModel as PracticeViewModel).EditFinished -= PracticePage_EditFinished;
            
            base.NavigationHelper_SaveState(sender, e);
        }

        #endregion

        #region ActiveItemChanged Callback

        private void EPracticeState_CurrentChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if(EPracticeState.Current == EPracticeState.Undefined)
            //{
            //    return;
            //}
            
            //PracticeViewModel viewModel = ViewModel as PracticeViewModel;

            //String example = viewModel.ActiveItem.Example;
            
            //ExampleTextblock.Inlines.Clear();
            
            //while (example?.Length > 0)
            //{
            //    int openingBrackedPosition = example.IndexOf('<');
            //    int closingBrackedPosition = example.IndexOf('>');

            //    if (openingBrackedPosition >= 0 && closingBrackedPosition > openingBrackedPosition)
            //    {
            //        String text = example.Substring(0, openingBrackedPosition);

            //        Run run = new Run()
            //        {
            //            Text = text
            //        };

            //        ExampleTextblock.Inlines.Add(run);
            //        example = example.Substring(openingBrackedPosition);

            //        closingBrackedPosition = example.IndexOf('>');
            //        text = example.Substring(1, closingBrackedPosition - 1);

            //        if (EPracticeState.Current == EPracticeState.Answered)
            //        {
            //            run = new Run()
            //            {
            //                  Text = text
            //                , Foreground = new SolidColorBrush(Colors.LimeGreen)
            //            };
            //        }
            //        else
            //        {
            //            run = new Run()
            //            {
            //                  Text = "__"
            //                , Foreground = new SolidColorBrush(Colors.LimeGreen)
            //            };
            //        }

            //        ExampleTextblock.Inlines.Add(run);
            //        example = example.Substring(closingBrackedPosition + 1);
            //    }
            //    else
            //    {
            //        Run run = new Run()
            //        {
            //            Text = example
            //        };

            //        ExampleTextblock.Inlines.Add(run);
            //        example = "";
            //    }
            //}
        }

        #endregion
    }
}