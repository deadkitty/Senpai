using Nyantilities.Collections;
using Nyantilities.Commands;
using Nyantilities.Core;
using SenpaiBase.EnumerationTypes;
using SenpaiModel;
using SenpaiPracticing.Data;
using SenpaiPracticing.Util;
using SenpaiUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SenpaiPracticing
{
    public class PracticeViewModel : NyaViewModel
    {
        #region Properties

        #region Initialization Properties

        private List<Lesson> practiceLessons;
        public List<Lesson> PracticeLessons
        {
            get => practiceLessons;
            set => SetProperty(ref practiceLessons, value);
        }

        #endregion

        #region Active Properties

        private List<IVocabItem> gerItems;
        private List<IVocabItem> japItems;
        private IVocabItem activeItem;

        public event PropertyChangedCallback ActiveItemChanged;
        
        public List<IVocabItem> GerItems
        {
            get => gerItems;
            set => SetProperty(ref gerItems, value);
        }

        public List<IVocabItem> JapItems
        {
            get => japItems;
            set => SetProperty(ref japItems, value);
        }

        public IVocabItem ActiveItem
        {
            get => activeItem;
            set
            {
                SetProperty(ref activeItem, value);
                Description = activeItem.Description;
                ActiveItemChanged?.Invoke(null, null);
            }
        }

        private List<IVocabItem> activeItems;
        public List<IVocabItem> ActiveItems
        {
            get => activeItems;
            set => SetProperty(ref activeItems, value);
        }

        private List<BackItem> backStack;
        public List<BackItem> BackStack
        {
            get => backStack;
            set => SetProperty(ref backStack, value);
        }
        
        private String description;
        public String Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        #endregion

        #region Status Properties

        private int itemsCorrect = 0;
        private int itemsWrong   = 0;
        private int itemsLeft    = 0;

        public int ItemsCorrect
        {
            get => itemsCorrect;
            set => SetProperty(ref itemsCorrect, value);
        }

        public int ItemsWrong
        {
            get => itemsWrong;
            set => SetProperty(ref itemsWrong, value);
        }

        public int ItemsLeft
        {
            get => itemsLeft;
            set => SetProperty(ref itemsLeft, value);
        }
        
        #endregion

        #region Example Sentences

        //private Paragraph paragraph;
        //public Paragraph Paragraph
        //{
        //    get => paragraph;
        //    set => SetProperty(ref paragraph, value);
        //}

        //private List<String> exampleParts;
        //public List<String> ExampleParts
        //{
        //    get => exampleParts;
        //    set => SetProperty(ref exampleParts, value);
            
        //    //how to add new text to a richtextblock ...
        //    //Paragraph p = new Paragraph();
        //    //Run r = new Run()
        //    //{
        //    //    Text = "大好き",
        //    //    Foreground = new SolidColorBrush(Colors.LimeGreen)
        //    //};

        //    //p.Inlines.Add(new Run() { Text = "Beispiel: 私はまみちゃんが" });
        //    //p.Inlines.Add(r);
        //    //p.Inlines.Add(new Run() { Text = "です。" });

        //    //testRichTextblock.Blocks.Add(p);
        //}

        #endregion
        
        #endregion

        #region Constructor

        public PracticeViewModel()
        {
            
        }

        #endregion

        #region Initialization

        public override void LoadState(LoadStateEventArgs args)
        {
            PracticeState = EPracticeState.Questioning;

            PracticeTimer.UpdateCurrentRound();
            
            LoadVocabItems();
        }
        
        private void LoadVocabItems()
        {
            GerItems = new List<IVocabItem>();
            JapItems = new List<IVocabItem>();

            foreach (Lesson lesson in PracticeLessons)
            {
                foreach (Word word in lesson.Words)
                {
                    if (word.NextRoundGer <= PracticeTimer.CurrentRound)
                    {
                        GerItems.Add(new GerJapItem(word));
                    }

                    if (word.NextRoundJap <= PracticeTimer.CurrentRound)
                    {
                        JapItems.Add(new JapGerItem(word));
                    }
                }
            }

            VocabItemComparer comparer = new VocabItemComparer();

            gerItems.Sort(comparer);
            japItems.Sort(comparer);

            //insert new items (nextRound == 0) at the end of the list
            int index = 0;

            while (index < GerItems.Count && GerItems[index++].NextRound == 0) GerItems.Add(GerItems.PopFront());

            index = 0;
            while (index < JapItems.Count && JapItems[index++].NextRound == 0) JapItems.Add(JapItems.PopFront());

            ActiveItems = new List<IVocabItem>();
            BackStack   = new List<BackItem>();

            //items regrouping, first a group of gerItems, than japItems, than again gerItems and so on ... the last two groups are maybe smaller ...
            while (GerItems.Count > 0 || JapItems.Count > 0)
            {
                int sectionSize = 20; // AppSettings.RoundSize;
                    
                for (int i = 0; i < sectionSize && GerItems.Count > 0; i++)
                {
                    ActiveItems.Add(GerItems.PopFront());
                }

                for (int i = 0; i < sectionSize && JapItems.Count > 0; i++)
                {
                    ActiveItems.Add(JapItems.PopFront());
                }
            }

            ItemsLeft    = ActiveItems.Count;
            ItemsCorrect = 0;
            ItemsWrong   = 0;

            ActiveItem = GetNextItem();

            LoadNewChibi();

            PracticeState = EPracticeState.Questioning;
        }

        #endregion

        #region Uninitialization

        public override void SaveState(SaveStateEventArgs args)
        {
            foreach (Lesson lesson in PracticeLessons)
            {
                lesson.NextRound = int.MaxValue;

                foreach (Word word in lesson.Words)
                {
                    if (word.NextRoundGer > 0)
                    {
                        lesson.NextRound = Math.Min(lesson.NextRound, word.NextRoundGer);
                    }

                    if (word.NextRoundJap > 0)
                    {
                        lesson.NextRound = Math.Min(lesson.NextRound, word.NextRoundJap);
                    }
                }

                if (lesson.NextRound == int.MaxValue)
                {
                    lesson.NextRound = 0;
                }
            }

            DataManager.SaveChanges();
        }

        #endregion

        #region Practice

        #region Properties

        private EPracticeState practiceState;
        public EPracticeState PracticeState
        {
            get => practiceState;
            set => SetProperty(ref practiceState, value);
        }

        #endregion

        #region Commands
        
        private DelegateCommand ShowAnswer;
        public DelegateCommand ShowAnswerCommand
        {
            get => ShowAnswer ?? (ShowAnswer = new DelegateCommand(OnShowAnswer));
            set => SetProperty(ref ShowAnswer, value);
        }

        private DelegateCommand SkipItem;
        public DelegateCommand SkipItemCommand
        {
            get => SkipItem ?? (SkipItem = new DelegateCommand(OnSkipItem));
            set => SetProperty(ref SkipItem, value);
        }

        private DelegateCommand GetHint;
        public DelegateCommand GetHintCommand
        {
            get => GetHint ?? (GetHint = new DelegateCommand(OnGetHint));
            set => SetProperty(ref GetHint, value);
        }

        private DelegateCommand GoBack;
        public DelegateCommand GoBackCommand
        {
            get => GoBack ?? (GoBack = new DelegateCommand(OnGoBack));
            set => SetProperty(ref GoBack, value);
        }

        private RelayCommand<int> Evaluate;
        public RelayCommand<int> EvaluateCommand
        {
            get => Evaluate ?? (Evaluate = new RelayCommand<int>(OnEvaluate));
            set => SetProperty(ref Evaluate, value);
        }

        #endregion

        private void OnShowAnswer()
        {
            PracticeState = EPracticeState.Answering;

            Description = ActiveItem.GetDescription();
        }

        private void OnSkipItem()
        {
            //TODO: BackItem hinzufügen und richtig auflösen
            ActiveItems.PushBack(ActiveItem);
            ActiveItem = GetNextItem();
        }

        private void OnGoBack()
        {
            if (BackStack.Count > 0)
            {
                ActiveItems.PushFront(ActiveItem);

                BackItem backItem = BackStack.PopBack();

                if (backItem.VocabItem.Answer < 2)
                {
                    --ItemsCorrect;
                }
                else
                {
                    --ItemsWrong;
                }

                ItemsLeft = activeItems.Count;

                backItem.RollBack();

                ActiveItem = backItem.VocabItem;

                //set the practicestate again so the example string can update and is set ...
                PracticeState = PracticeState;
            }
        }

        private void OnGetHint()
        {

        }

        private void OnEvaluate(int answer)
        {
            BackStack.PushBack(new BackItem(ActiveItem));

            //TODO: schauen dass das hier passt mit dem zeitfenster zurückstellen von die lernitems, sonst vieleicht das ganze anpassen und die zeit langsamer zurücklaufen lassen mit indem 
            //ich das (Util.CurrentRound - ActiveItem.NextRound) mit 0,5 multipliziere oder sowas ...
            int repetition = Math.Max(0, ActiveItem.NextRound - ActiveItem.LastRound - (PracticeTimer.CurrentRound - ActiveItem.NextRound));
            //int repetition = ActiveItem.NextRound - ActiveItem.LastRound;

            switch (answer)
            {
                case 0: Answer0(repetition); break;
                case 1: Answer1(repetition); break;
                case 2: Answer2(repetition); break;
                case 3: Answer3(repetition); break;
            }
            
            ActiveItem.EFactor = Math.Max(1.3f, ActiveItem.EFactor);
            ActiveItem.LastRound = PracticeTimer.CurrentRound;

            InsertItem(answer);

            UpdateStatusProperties(answer);

            if (ActiveItems.Count == 0)
            {
                PracticeState = EPracticeState.RoundFinished;
                ShowSummary();
            }
            else
            {
                ActiveItem = GetNextItem();

                PracticeState = EPracticeState.Questioning;
            }
        }

        private void Answer0(int repetition)
        {
            if (repetition < 5)
            {
                ActiveItem.NextRound = PracticeTimer.CurrentRound + 18;
            }
            else if (repetition < 20)
            {
                ActiveItem.NextRound = PracticeTimer.CurrentRound + 48;
            }
            else
            {
                ActiveItem.NextRound = PracticeTimer.CurrentRound + (int)(ActiveItem.EFactor * repetition);
            }

            ActiveItem.EFactor += 0.1f;
        }

        private void Answer1(int repetition)
        {
            if (repetition < 18)
            {
                ActiveItem.NextRound = PracticeTimer.CurrentRound + 18;
            }
            else
            {
                ActiveItem.NextRound = PracticeTimer.CurrentRound + repetition;
            }
        }

        private void Answer2(int repetition)
        {
            if (repetition < 18)
            {
                ActiveItem.NextRound = PracticeTimer.CurrentRound;
            }
            else
            {
                ActiveItem.NextRound = PracticeTimer.CurrentRound + 18;
            }

            ActiveItem.EFactor -= 0.1f;
        }

        private void Answer3(int repetition)
        {
            ActiveItem.NextRound = PracticeTimer.CurrentRound;

            ActiveItem.EFactor -= 0.2f;
        }

        private void InsertItem(int answer)
        {
            if (answer >= 2)
            {
                ActiveItems.Add(ActiveItem);
            }
        }

        private IVocabItem GetNextItem()
        {
            return ActiveItems.PopFront();
        }

        private void UpdateStatusProperties(int answer)
        {
            if (answer < 2)
            {
                ++ItemsCorrect;
            }
            else
            {
                ++ItemsWrong;
            }

            ItemsLeft = ActiveItems.Count;
        }

        #endregion

        #region Round Finished

        private async void ShowSummary()
        {
            MessageDialog msg = new MessageDialog("Keine Vokabeln mehr vorhanden ...", "Übung beendet.");

            msg.Commands.Add(new UICommand("Zurück", GoBackCallback));
            msg.Commands.Add(new UICommand("Beenden", EndPracticeCallback));

            await msg.ShowAsync();
        }

        private void GoBackCallback(IUICommand command)
        {
            OnGoBack();
        }

        private void EndPracticeCallback(IUICommand command)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        #endregion

        #region General

        #region Properties

        public event EventHandler EditFinished;

        private Word backupItem = null;
        private Word editItem = null;
        //private bool editModeActive = false;
        public Word EditItem
        {
            get => editItem;
            set => SetProperty(ref editItem, value);
        }

        #endregion

        #region Commands

        private DelegateCommand Quit;
        public DelegateCommand QuitCommand
        {
            get => Quit ?? (Quit = new DelegateCommand(OnQuit));
            set => SetProperty(ref Quit, value);
        }

        private DelegateCommand Settings;
        public DelegateCommand SettingsCommand
        {
            get => Settings ?? (Settings = new DelegateCommand(OnSettings));
            set => SetProperty(ref Settings, value);
        }

        private DelegateCommand Edit;
        public DelegateCommand EditCommand
        {
            get => Edit ?? (Edit = new DelegateCommand(OnEdit));
            set => SetProperty(ref Edit, value);
        }

        private DelegateCommand CancelEdit;
        public DelegateCommand CancelEditCommand
        {
            get => CancelEdit ?? (CancelEdit = new DelegateCommand(OnCancelEdit));
            set => SetProperty(ref CancelEdit, value);
        }

        #endregion

        #region Quit Practice

        private async void OnQuit()
        {
            //both commands use the same shortcut (escape) so i have to decide by the current state which code to be executed ... 
            //unfortunately i can't decide it by which view has the focus ... 
            if (PracticeState == EPracticeState.Editing)
            {
                OnCancelEdit();
            }
            else
            {
                MessageDialog quitDialog = new MessageDialog("Übung beenden");
                quitDialog.Commands.Add(new UICommand("Beenden", EndPracticeCallback));
                quitDialog.Commands.Add(new UICommand("Abbrechen"));

                await quitDialog.ShowAsync(); 
            }
        }

        #endregion

        #region Edit Word

        private void OnEdit()
        {
            if (PracticeState == EPracticeState.Editing)
            {
                CloseEditView();
            }
            else
            {
                OpenEditView();
            }
        }

        private void OpenEditView()
        {
            PracticeState = EPracticeState.Editing;

            backupItem = new Word(ActiveItem.Source);
            EditItem = ActiveItem.Source;
        }

        private void CloseEditView()
        {
            EditItem = null;

            if (PracticeState == EPracticeState.Questioning)
            {
                Description = ActiveItem.Description;
            }
            else
            {
                Description = ActiveItem.GetDescription();
            }
            
            NotifyPropertyChanged("ActiveItem");

            EditFinished?.Invoke(this, null);

            PracticeState = EPracticeState.Answering;
        }

        private void OnCancelEdit()
        {
            //revert changes made by the user
            ActiveItem.Source.CopyValues(backupItem);

            EditFinished?.Invoke(this, null);

            EditItem = null;

            PracticeState = EPracticeState.Answering;
        }

        #endregion

        #region Go to Settings Page

        private void OnSettings()
        {

        }

        #endregion

        #endregion

        #region Chibi

        private String chibiSource = @"D:\Soi Fon\Visual Studio 2017\Projects\Senpai\Senpai\Assets\Chibis\Chibi0.png";
        public String ChibiSource
        {
            get => chibiSource;
            set => SetProperty(ref chibiSource, value);
        }

        private void LoadNewChibi()
        {
            String[] files = Directory.GetFiles("Assets/Chibis");

            Random random = new Random();

            String file = files[random.Next(files.Length)];

            ChibiSource = "ms-appx:///" + file;
        }

        #endregion
    }
}