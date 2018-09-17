using Nyantilities.Core;
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

namespace SenpaiControls
{
    public sealed partial class EditView : NyaControl
    {
        #region Dependency Properties

        public Word Word
        {
            get { return (Word)GetValue(WordProperty); }
            set { SetValue(WordProperty, value); }
        }

        public static readonly DependencyProperty WordProperty =
            DependencyProperty.Register("Word", typeof(Word), typeof(EditView), new PropertyMetadata(null, OnWordChanged));

        private static void OnWordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EditView view = d as EditView;

            if(view.Word != null)
            {
                view.FillProperties();
            }
        }

        private void FillProperties()
        {
            foreach (EVocabType type in EditTypeListview.Items)
            {
                if (type == Word.VocabType)
                {
                    EditTypeListview.SelectedItem = type;
                    break;
                }
            }

            foreach (EVisibilityType flag in ShowWordListview.Items)
            {
                if (flag == Word.ShowWord)
                {
                    ShowWordListview.SelectedItem = flag;
                    break;
                }
            }

            foreach (EVisibilityType flag in ShowDescListview.Items)
            {
                if (flag == Word.ShowDesc)
                {
                    ShowDescListview.SelectedItem = flag;
                    break;
                }
            }
        }

        #endregion

        #region Constructor

        public EditView()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization

        private void ThisView_Loaded(object sender, RoutedEventArgs e)
        {
            EditTypeListview.ItemsSource = EVocabType.GetList();
            ShowWordListview.ItemsSource = EVisibilityType.GetList();
            ShowDescListview.ItemsSource = EVisibilityType.GetList();
        }

        #endregion

        #region Events

        private void EditTypeListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EditTypeListview.SelectedItems.Count > 0)
            {
                Word.VocabType = EditTypeListview.SelectedItem as EVocabType;
            }
        }

        private void ShowWordListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShowWordListview.SelectedItems.Count > 0)
            {
                Word.ShowWord = ShowWordListview.SelectedItem as EVisibilityType;
            }
        }

        private void ShowDescListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShowDescListview.SelectedItems.Count > 0)
            {
                Word.ShowDesc = ShowDescListview.SelectedItem as EVisibilityType;
            }
        }

        #endregion
    }
}