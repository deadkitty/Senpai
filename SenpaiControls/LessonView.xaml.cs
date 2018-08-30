using Nyantilities.Core;
using SenpaiModel;
using SenpaiUtilities;
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
    public sealed partial class LessonView : NyaControl
    {
        #region Properties

        public Lesson Lesson
        {
            get { return (Lesson)GetValue(LessonProperty); }
            set { SetValue(LessonProperty, value); }
        }

        public static readonly DependencyProperty LessonProperty =
            DependencyProperty.Register("Lesson", typeof(Lesson), typeof(LessonView), new PropertyMetadata(null, OnLessonChanged));

        private static void OnLessonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LessonView view = d as LessonView;

            if (view.Lesson == null)
            {
                return;
            }

            if (view.Lesson.NextRound <= PracticeTimer.CurrentRound && view.Lesson.NextRound > 0)
            {
                view.LayoutRoot.Background = view.Resources["DueBrush"] as LinearGradientBrush;
            }
            else
            {
                view.LayoutRoot.Background = view.Resources["NormalBrush"] as LinearGradientBrush;
            }
        }

        #endregion

        #region Constructor

        public LessonView()
        {
            InitializeComponent();
        }

        #endregion
    }
}
