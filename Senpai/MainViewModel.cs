using Microsoft.Toolkit.Uwp.Helpers;
using Nyantilities.Commands;
using Nyantilities.Core;
using Nyantilities.ViewModel;
using SenpaiBase.EnumerationTypes;
using SenpaiModel;
using SenpaiPracticing;
using SenpaiUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace Senpai
{
    public class MainViewModel : NyaViewModel
    {
        #region Properties

        private List<Lesson> lessons;
        public List<Lesson> Lessons
        {
            get => lessons;
            set => SetProperty(ref lessons, value);
        }

        private List<Lesson> selectedLessons;
        public List<Lesson> SelectedLessons
        {
            get => selectedLessons;
            set => SetProperty(ref selectedLessons, value);
        }

        private List<LessonItem> lessonItems;
        public List<LessonItem> LessonItems
        {
            get => lessonItems;
            set => SetProperty(ref lessonItems, value);
        }

        private List<LessonItem> searchResults;
        public List<LessonItem> SearchResults
        {
            get => searchResults;
            set => SetProperty(ref searchResults, value);
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {

        }

        #endregion

        #region Load/Save State

        public override void LoadState(LoadStateEventArgs args)
        {
            ESenpaiState.Current = ESenpaiState.MainMenu;
            
            AddSortIndexToKanjiLessons();

            Lessons = DataManager.Database.Lessons.OrderBy(x => x.SortIndex).ToList();
            
            Lesson lesson1 = new Lesson()
            {
                  Name      = "Minna no Nihongo 1"
                , Size      = 0
                , SortIndex = 0
                , Type      = ELessonType.Vocab
            };
            
            Lesson lesson2 = new Lesson()
            {
                  Name      = "Minna no Nihongo 2"
                , Size      = 0
                , SortIndex = 1
                , Type      = ELessonType.Vocab
            };
            
            Lesson lesson3 = new Lesson()
            {
                  Name      = "Tobira 1"
                , Size      = 0
                , SortIndex = 2
                , Type      = ELessonType.Vocab
            };
            
            Lesson lesson4 = new Lesson()
            {
                  Name      = "Tobira 2"
                , Size      = 0
                , SortIndex = 3
                , Type      = ELessonType.Vocab
            };

            Lessons = new List<Lesson>()
            {
                lesson1,
                lesson2,
                lesson3,
                lesson4
            };
        }

        #endregion

        #region Lesson selection

        public void LessonSelected(Lesson selectedLesson)
        {
            LessonItems = selectedLesson == null ? null : DataManager.Database.Words.Where(x => x.Lesson == selectedLesson).ToList<LessonItem>();
        }

        #endregion

        #region Start Practicing
        
        private DelegateCommand StartLesson;
        public DelegateCommand StartLessonCommand => StartLesson ?? (StartLesson = new DelegateCommand(OnStartLesson));
        
        private DelegateCommand QuickStart;
        public DelegateCommand QuickStartCommand => QuickStart ?? (QuickStart = new DelegateCommand(OnQuickStart));
        
        private void OnStartLesson()
        {
            PracticeTimer.UpdateCurrentRound();

            StartPractice();
        }
        
        private async void OnQuickStart()
        {
            PracticeTimer.UpdateCurrentRound();

            SelectedLessons = new List<Lesson>();
            foreach (Lesson lesson in DataManager.Database.Lessons)
            {
                if (lesson.NextRound > 0 && lesson.NextRound <= PracticeTimer.CurrentRound)
                {
                    SelectedLessons.Add(lesson);
                }
            }

            if (SelectedLessons.Count == 0)
            {
                MessageDialog messageDialog = new MessageDialog("Derzeit sind keine Lektionen zum lernen vorhanden ...");

                messageDialog.Commands.Add(new UICommand("Ok"));

                await messageDialog.ShowAsync();
            }
            else
            {
                StartPractice();
            }
        }

        private void StartPractice()
        {
            SelectedLessons.Sort(new LessonComparer());

            PracticeViewModel practiceVM = ViewModelProvider.GetViewModel<PracticeViewModel>();

            foreach (Lesson lesson in SelectedLessons)
            {
                lesson.Words = DataManager.Database.Words.Where(x => x.Lesson == lesson).ToList();
            }

            practiceVM.PracticeLessons = SelectedLessons;

            NyavigationHelper.NyavigateTo<PracticePage>();
        }

        #endregion

        #region Create Lesson

        private DelegateCommand CreateLesson;
        public DelegateCommand CreateLessonCommand => CreateLesson ?? (CreateLesson = new DelegateCommand(OnCreateLesson));

        private void OnCreateLesson()
        {
            //NyavigationHelper.NyavigateTo<CreateLesson>();
        }

        #endregion

        #region Edit Lesson

        private DelegateCommand EditLesson;
        public DelegateCommand EditLessonCommand => EditLesson ?? (EditLesson = new DelegateCommand(OnEditLesson));

        private void OnEditLesson()
        {
            //NyavigationHelper.NyavigateTo<CreateLesson>(SelectedLessons.First());
        }

        #endregion

        #region Import Lessons
        
        private DelegateCommand ImportLessons;
        public DelegateCommand ImportLessonsCommand => ImportLessons ?? (ImportLessons = new DelegateCommand(OnImportLessons));

        private async void OnImportLessons()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();

            fileOpenPicker.FileTypeFilter.Add(".json");
            fileOpenPicker.FileTypeFilter.Add(".txt");
            fileOpenPicker.FileTypeFilter.Add(".nya");

            StorageFile file = await fileOpenPicker.PickSingleFileAsync();

            if (file != null)
            {
                Stream importStream = await file.OpenStreamForReadAsync();

                String results = DataManager.ImportFromFile(importStream);

                importStream.Close();

                ShowResultsDialog(results, "Import Lessons");

                Lessons = DataManager.Database.Lessons.OrderBy(x => x.SortIndex).ToList();
            }
        }

        private async void ShowResultsDialog(String results, String title)
        {
            MessageDialog msgDialog = new MessageDialog(results, title);

            msgDialog.Commands.Add(new UICommand("OK"));

            await msgDialog.ShowAsync();
        }

        #endregion

        #region Export Lessons

        private DelegateCommand ExportLessons;
        public DelegateCommand ExportLessonsCommand => ExportLessons ?? (ExportLessons = new DelegateCommand(OnExportLessons));

        private async void OnExportLessons()
        {
            FileSavePicker fileSavePicker = new FileSavePicker();

            fileSavePicker.FileTypeChoices.Add("JSON", new List<string>() { ".json" });

            StorageFile storageFile = await fileSavePicker.PickSaveFileAsync();

            if (storageFile != null)
            {
                Stream stream = await storageFile.OpenStreamForWriteAsync();

                String results = DataManager.Export(stream);

                stream.Close();

                ShowResultsDialog(results, "Export Lessons");
            }
        }

        #endregion

        #region Settings

        private DelegateCommand GoToSettings;
        public DelegateCommand GoToSettingsCommand => GoToSettings ?? (GoToSettings = new DelegateCommand(OnGoToSettings));

        private void OnGoToSettings()
        {
            //NyavigationHelper.NyavigateTo<SettingsPage>();
        }

        #endregion

        #region Searching

        #region Fields
        
        private Timer startSearchTimer;
        private CancellationTokenSource cancellationSource;

        #endregion

        #region Properties

        private bool isSearching = false;
        public bool IsSearching
        {
            get => isSearching;
            set => SetProperty(ref isSearching, value);
        }

        private String isSearchingText = "Wörter werden durchsucht ...";
        public String IsSearchingText
        {
            get => isSearchingText;
            set => SetProperty(ref isSearchingText, value);
        }

        #endregion

        /// <summary>
        /// Initializes a Search for words wich contain the given searchtext in kana, kanji or translation.
        /// <para>TODO: extend search to look for kanjis and grammar as well</para>
        /// </summary>
        public void InitializeSearch(String searchText)
        {
            if (startSearchTimer != null)
            {
                startSearchTimer.Dispose();
                startSearchTimer = null;
            }

            cancellationSource = new CancellationTokenSource();

            startSearchTimer = new Timer(StartSearch, searchText, 750, Timeout.Infinite);
        }

        private async void StartSearch(object searchText)
        {
            if (IsSearching)
            {
                cancellationSource.Cancel();
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                IsSearchingText = "Wörterbuch wird durchsucht ...";
                IsSearching = true;
            });

            List<Word> words = null;

            await Task.Factory.StartNew(() =>
            {
                words = DataManager.SearchForItems(searchText as String);
            }, cancellationSource.Token);

            startSearchTimer.Dispose();
            startSearchTimer = null;

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                IsSearching = false;

                LessonItems = words == null ? null : new List<LessonItem>(words);
            });
        }

        #endregion

        #region Others

        /// <summary>
        /// adds a sort index from 100 up, so that i don't have to see them while i actually work with the vocab lessons ;D
        /// </summary>
        private void AddSortIndexToKanjiLessons()
        {
            List<Lesson> lessons = DataManager.Database.Lessons.Where(x => x.Type == ELessonType.Kanji).ToList();

            int i = 0;

            foreach (Lesson lesson in lessons)
            {
                if (lesson.Type == ELessonType.Kanji)
                {
                    lesson.SortIndex = 100 + i;
                    ++i;
                }
            }

            DataManager.SaveChanges();
        }
        
        #endregion
    }
}