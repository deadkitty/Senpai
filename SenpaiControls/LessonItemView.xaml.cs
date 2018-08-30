using Nyantilities.Core;
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

namespace SenpaiControls
{
    public sealed partial class LessonItemView : NyaControl
    {
        #region Properties

        public LessonItem LessonItem
        {
            get { return (LessonItem)GetValue(LearnItemProperty); }
            set { SetValue(LearnItemProperty, value); }
        }

        public static readonly DependencyProperty LearnItemProperty =
            DependencyProperty.Register("LessonItem", typeof(LessonItem), typeof(LessonItemView), null);

        #endregion

        #region Constructor

        public LessonItemView()
        {
            InitializeComponent();
        }

        #endregion
    }
}
