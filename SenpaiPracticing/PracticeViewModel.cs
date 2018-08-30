using Nyantilities.Core;
using SenpaiModel;
using SenpaiPracticing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

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
        private String itemsLeft = null;

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

        public String ItemsLeft
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

        //#region Initialization

        //public override void LoadState(LoadStateEventArgs args)
        //{
        //    ESenpaiState.Current = ESenpaiState.PracticeMode;

        //    Util.RenewCurrentRound();

        //    AnswerVisibility = Visibility.Collapsed;
        //    QuestionVisibility = Visibility.Visible;
            
        //    LoadVocabItems();

        //    if (GerItems.Count == 0 && JapItems.Count == 0)
        //    {
        //        StartNewRoundCallback(null);
        //    }
        //    else
        //    {
        //        LoadNewRound();
        //    }
        //}
        
        //private void LoadVocabItems()
        //{
        //    GerItems = new List<IVocabItem>();
        //    JapItems = new List<IVocabItem>();
            
        //    foreach (Lesson lesson in PracticeLessons)
        //    {
        //        foreach (Word word in lesson.Words)
        //        {
        //            if (word.NextRoundGer <= Util.CurrentRound)
        //            {
        //                GerItems.Add(new GerJapItem(word));
        //            }
        //        }
        //    }

        //    foreach (Lesson lesson in PracticeLessons)
        //    {
        //        foreach (Word word in lesson.Words)
        //        {
        //            if (word.NextRoundJap <= Util.CurrentRound)
        //            {
        //                JapItems.Add(new JapGerItem(word));
        //            }
        //        }
        //    }

        //    GerItems.Sort1(this);
        //    JapItems.Sort1(this);

        //    for (int i = 0; i < GerItems.Count; i++)
        //    {
        //        if (GerItems[0].NextRound == 0)
        //        {
        //            GerItems.Add(GerItems.PopFront());
        //        }
        //    }

        //    for (int i = 0; i < JapItems.Count; i++)
        //    {
        //        if (JapItems[0].NextRound == 0)
        //        {
        //            JapItems.Add(JapItems.PopFront());
        //        }
        //    }
        //}

        //private void LoadNewRound()
        //{
        //    Util.RenewCurrentRound();

        //    ActiveItems = new List<IVocabItem>();
        //    BackStack   = new List<BackItem>();

        //    Func<bool> gerItemsCondition;
        //    Func<bool> japItemsCondition;

        //    if (GerItems.Count + JapItems.Count < AppSettings.RoundSize + 20)
        //    {
        //        gerItemsCondition = () => { return GerItems.Count > 0; };
        //        japItemsCondition = () => { return JapItems.Count > 0; };                
        //    }
        //    else
        //    {
        //        gerItemsCondition = () => { return ActiveItems.Count < AppSettings.RoundSize / 2 && GerItems.Count > 0; };
        //        japItemsCondition = () => { return ActiveItems.Count < AppSettings.RoundSize     && JapItems.Count > 0; };
        //    }

        //    while (gerItemsCondition())
        //    {
        //        ActiveItems.Add(GerItems.PopFront());
        //    }

        //    while (japItemsCondition())
        //    {
        //        ActiveItems.Add(JapItems.PopFront());
        //    }

        //    ItemsLeft    = String.Format("{0} / {1}", ActiveItems.Count, GerItems.Count + JapItems.Count);
        //    ItemsCorrect = 0;
        //    ItemsWrong   = 0;

        //    ActiveItem = GetNextItem();
            
        //    LoadNewChibi();

        //    EPracticeState.Current = EPracticeState.Questioning;
        //}

        ///// <summary>
        ///// <para>Sortiert die Liste an VocabItems so um das wechselnd gruppen von erst GerJapItems der angegebenen größe entstehen und 
        ///// danach eine gruppe von JapGerItems. also zb erst fünf GerJapItem und danach fünf JapGerItems und immer abwechselnd.
        ///// die letzen beiden gruppen sind eventuell kleiner wenn nicht genügend items mehr da sind um noch zwei gruppen von der angegebenen
        ///// größe zu erstellen.</para>
        ///// <para>Bei den items wird davon ausgegangen das die erste hälfte der liste nur aus GerJapItems und die zweite hälfte komplett aus JapGerItems besteht.</para>
        ///// </summary>
        ///// <param name="items"></param>
        //private void RegroupItems(List<IVocabItem> gerJapItems, List<IVocabItem> japGerItems, int groupSize, out List<IVocabItem> groupedItems)
        //{
        //    groupedItems = new List<IVocabItem>();

        //    while (gerJapItems.Count > 0 || japGerItems.Count > 0)
        //    {
        //        groupSize = Math.Min(groupSize, gerJapItems.Count);

        //        for (int i = 0; i < groupSize; i++)
        //        {
        //            groupedItems.Add(gerJapItems.PopFront());
        //        }

        //        groupSize = Math.Min(groupSize, japGerItems.Count);

        //        for (int i = 0; i < groupSize; i++)
        //        {
        //            groupedItems.Add(japGerItems.PopFront());
        //        }
        //    }
        //}
        
        //#endregion

        //#region Uninitialization

        //public override void SaveState(SaveStateEventArgs args)
        //{
        //    foreach (Lesson lesson in PracticeLessons)
        //    {
        //        lesson.NextRound = int.MaxValue;

        //        foreach (Word word in lesson.Words)
        //        {
        //            if (word.NextRoundGer > 0)
        //            {
        //                lesson.NextRound = Math.Min(lesson.NextRound, word.NextRoundGer);
        //            }

        //            if (word.NextRoundJap > 0)
        //            {
        //                lesson.NextRound = Math.Min(lesson.NextRound, word.NextRoundJap);
        //            }
        //        }

        //        if (lesson.NextRound == int.MaxValue)
        //        {
        //            lesson.NextRound = 0;
        //        }
        //    }

        //    DataManager.SaveChanges();
        //    base.SaveState(args);
        //}

        //#endregion

        //#region Practice

        //#region Commands

        //private DelegateCommand ShowAnswer;
        //public DelegateCommand ShowAnswerCommand
        //{
        //    get => ShowAnswer ?? (ShowAnswer = new DelegateCommand(OnShowAnswer));
        //    set => SetProperty(ref ShowAnswer, value);
        //}
        
        //private DelegateCommand SkipItem;
        //public DelegateCommand SkipItemCommand
        //{
        //    get => SkipItem ?? (SkipItem = new DelegateCommand(OnSkipItem));
        //    set => SetProperty(ref SkipItem, value);
        //}

        //private DelegateCommand GetHint;
        //public DelegateCommand GetHintCommand
        //{
        //    get => GetHint ?? (GetHint = new DelegateCommand(OnGetHint));
        //    set => SetProperty(ref GetHint, value);
        //}

        //private DelegateCommand GoBack;
        //public DelegateCommand GoBackCommand
        //{
        //    get => GoBack ?? (GoBack = new DelegateCommand(OnGoBack));
        //    set => SetProperty(ref GoBack, value);
        //}
        
        //private RelayCommand<int> Evaluate;
        //public RelayCommand<int> EvaluateCommand
        //{
        //    get => Evaluate ?? (Evaluate = new RelayCommand<int>(OnEvaluate));
        //    set => SetProperty(ref Evaluate, value);
        //}

        //#endregion
        
        //private void OnShowAnswer()
        //{
        //    EPracticeState.Current = EPracticeState.Answered;

        //    Description = ActiveItem.GetDescription();

        //    SwitchVisibilities();
        //}

        //private void OnSkipItem()
        //{
        //    //TODO: BackItem hinzufügen und richtig auflösen
        //    ActiveItems.PushBack(ActiveItem);
        //    ActiveItem = GetNextItem();
        //}

        //private void OnGoBack()
        //{
        //    if (BackStack.Count > 0)
        //    {
        //        ActiveItems.PushFront(ActiveItem);

        //        BackItem backItem = BackStack.PopBack();

        //        if (backItem.VocabItem.Answer < 2)
        //        {
        //            --ItemsCorrect;
        //        }
        //        else
        //        {
        //            --ItemsWrong;
        //        }

        //        ItemsLeft = CreateItemsLeftString();

        //        backItem.RollBack();

        //        ActiveItem = backItem.VocabItem;

        //        //set the practicestate again so the example string can update and is set ...
        //        EPracticeState.Current = EPracticeState.Current;
        //    }
        //}

        //private void OnGetHint()
        //{

        //}

        //private void OnEvaluate(int answer)
        //{
        //    BackStack.PushBack(new BackItem(ActiveItem));

        //    //TODO: schauen dass das hier passt mit dem zeitfenster zurückstellen von die lernitems, sonst vieleicht das ganze anpassen und die zeit langsamer zurücklaufen lassen mit indem 
        //    //ich das (Util.CurrentRound - ActiveItem.NextRound) mit 0,5 multipliziere oder sowas ...
        //    int repetition = Math.Max(0, ActiveItem.NextRound - ActiveItem.LastRound - (Util.CurrentRound - ActiveItem.NextRound));
        //    //int repetition = ActiveItem.NextRound - ActiveItem.LastRound;

        //    switch (answer)
        //    {
        //        case 0: Answer0(repetition); break;
        //        case 1: Answer1(repetition); break;
        //        case 2: Answer2(repetition); break;
        //        case 3: Answer3(repetition); break;
        //    }

        //    //PracticeSummary[answer].Add(ActiveItem);

        //    ActiveItem.EFactor = Math.Max(1.3f, ActiveItem.EFactor);
        //    ActiveItem.LastRound = Util.CurrentRound;

        //    InsertItem(answer);

        //    UpdateStatusProperties(answer);
            
        //    if (ActiveItems.Count == 0)
        //    {
        //        EPracticeState.Current = EPracticeState.RoundFinished;
        //        ShowSummary();
        //    }
        //    else
        //    {
        //        ActiveItem = GetNextItem();

        //        EPracticeState.Current = EPracticeState.Questioning;
        //        SwitchVisibilities();
        //    }
        //}

        //private void Answer0(int repetition)
        //{
        //    if (repetition < 5)
        //    {
        //        ActiveItem.NextRound = Util.CurrentRound + 18;
        //    }
        //    else if (repetition < 20)
        //    {
        //        ActiveItem.NextRound = Util.CurrentRound + 48;
        //    }
        //    else
        //    {
        //        ActiveItem.NextRound = Util.CurrentRound + (int)(ActiveItem.EFactor * repetition);
        //    }

        //    ActiveItem.EFactor += 0.1f;
        //}

        //private void Answer1(int repetition)
        //{
        //    if (repetition < 18)
        //    {
        //        ActiveItem.NextRound = Util.CurrentRound + 18;
        //    }
        //    else
        //    {
        //        ActiveItem.NextRound = Util.CurrentRound + repetition;
        //    }
        //}

        //private void Answer2(int repetition)
        //{
        //    if (repetition < 18)
        //    {
        //        ActiveItem.NextRound = Util.CurrentRound;
        //    }
        //    else
        //    {
        //        ActiveItem.NextRound = Util.CurrentRound + 18;
        //    }

        //    ActiveItem.EFactor -= 0.1f;
        //}

        //private void Answer3(int repetition)
        //{
        //    ActiveItem.NextRound = Util.CurrentRound;

        //    ActiveItem.EFactor -= 0.2f;
        //}

        //private void InsertItem(int answer)
        //{
        //    if (answer >= 2)
        //    {
        //        ActiveItems.Add(ActiveItem);
        //    }
        //}

        //private IVocabItem GetNextItem()
        //{
        //    return ActiveItems.PopFront();
        //}

        //private void UpdateStatusProperties(int answer)
        //{
        //    if (answer < 2)
        //    {
        //        ++ItemsCorrect;
        //    }
        //    else
        //    {
        //        ++ItemsWrong;
        //    }

        //    ItemsLeft = CreateItemsLeftString();
        //}

        //#endregion

        //#region Round Finished

        //private async void ShowSummary()
        //{
        //    MessageDialog msg = new MessageDialog("Runde beendet");

        //    msg.Commands.Add(new UICommand("Weiter", StartNewRoundCallback));
        //    msg.Commands.Add(new UICommand("Zurück", GoBackCallback));
        //    msg.Commands.Add(new UICommand("Beenden", EndPracticeCallback));

        //    await msg.ShowAsync();
        //}

        //private async void StartNewRoundCallback(IUICommand command)
        //{
        //    if (GerItems.Count == 0 && JapItems.Count == 0)
        //    {
        //        MessageDialog messageDialog = new MessageDialog("Keine Vokabeln mehr vorhanden ...");

        //        messageDialog.Commands.Add(new UICommand("Ok", EndPracticeCallback));

        //        await messageDialog.ShowAsync();
        //    }
        //    else
        //    {
        //        LoadNewRound();

        //        SwitchVisibilities();
        //    }
        //}

        //private void GoBackCallback(IUICommand command)
        //{
        //    OnGoBack();
        //}

        //private void EndPracticeCallback(IUICommand command)
        //{
        //    Frame frame = Window.Current.Content as Frame;

        //    if (frame.CanGoBack)
        //    {
        //        frame.GoBack();
        //    }
        //}

        //#endregion

        //#region General

        //#region Properties

        //public event EventHandler EditFinished;

        //private Word backupItem = null;
        //private Word editItem = null;
        //public Word EditItem
        //{
        //    get => editItem;
        //    set => SetProperty(ref editItem, value);
        //}
        
        //#endregion

        //#region Commands

        //private DelegateCommand Quit;
        //public DelegateCommand QuitCommand
        //{
        //    get => Quit ?? (Quit = new DelegateCommand(OnQuit));
        //    set => SetProperty(ref Quit, value);
        //}

        //private DelegateCommand Settings;
        //public DelegateCommand SettingsCommand
        //{
        //    get => Settings ?? (Settings = new DelegateCommand(OnSettings));
        //    set => SetProperty(ref Settings, value);
        //}

        //private DelegateCommand Edit;
        //public DelegateCommand EditCommand
        //{
        //    get => Edit ?? (Edit = new DelegateCommand(OnEdit));
        //    set => SetProperty(ref Edit, value);
        //}

        //private DelegateCommand CancelEdit;
        //public DelegateCommand CancelEditCommand
        //{
        //    get => CancelEdit ?? (CancelEdit = new DelegateCommand(OnCancelEdit));
        //    set => SetProperty(ref CancelEdit, value);
        //}

        //#endregion

        //#region Quit Practice

        //private async void OnQuit()
        //{
        //    //both commands use the same shortcut (escape) so i have to decide by the current state which code to be executed ... 
        //    //unfortunately i can't decide it by which view has the focus ... 
        //    if (ESenpaiState.Current == ESenpaiState.PracticeMode)
        //    {
        //        MessageDialog quitDialog = new MessageDialog("Übung beenden");
        //        quitDialog.Commands.Add(new UICommand("Beenden", EndPracticeCallback));
        //        quitDialog.Commands.Add(new UICommand("Abbrechen"));

        //        await quitDialog.ShowAsync();
        //    }
        //    else if (ESenpaiState.Current == ESenpaiState.EditMode)
        //    {
        //        OnCancelEdit();
        //    }
        //}

        //#endregion

        //#region Edit Word
        
        //private void OnEdit()
        //{
        //    if (ESenpaiState.Current == ESenpaiState.PracticeMode) //go into edit mode and edit word
        //    {
        //        ESenpaiState.Current = ESenpaiState.EditMode;

        //        backupItem = new Word(ActiveItem.Source);
        //        EditItem   = ActiveItem.Source;
        //    }
        //    else //go back to practice mode and update changes
        //    {
        //        ESenpaiState.Current = ESenpaiState.PracticeMode;
                
        //        EditItem = null;
        //        EPracticeState.Current = EPracticeState.Current;

        //        if (EPracticeState.Current == EPracticeState.Questioning)
        //        {
        //            Description = ActiveItem.Description;
        //        }
        //        else
        //        {
        //            Description = ActiveItem.GetDescription();
        //        }

        //        NotifyPropertyChanged("ActiveItem");
        //    }

        //    EditFinished?.Invoke(this, null);

        //    SwitchEditViewVisibility();
        //}

        //private void OnCancelEdit()
        //{
        //    //go back to practice mode but don't update changes
        //    ESenpaiState.Current = ESenpaiState.PracticeMode;

        //    //revert changes made by the user
        //    ActiveItem.Source.OverrideValues(backupItem);

        //    EditFinished?.Invoke(this, null);

        //    EditItem = null;
        //    SwitchEditViewVisibility();
        //}

        //#endregion

        //#region Go to Settings Page

        //private void OnSettings()
        //{

        //}

        //#endregion

        //#endregion

        //#region Chibi

        //private String chibiSource = @"D:\Soi Fon\Visual Studio 2017\Projects\NihongoSenpai\NihongoSenpai\Assets\Chibis\chibi9.PNG";
        //public String ChibiSource
        //{
        //    get => chibiSource;
        //    set => SetProperty(ref chibiSource, value);
        //}

        //private void LoadNewChibi()
        //{
        //    String[] files = Directory.GetFiles("Assets/Chibis");

        //    Random random = new Random();

        //    String file = files[random.Next(files.Length)];
            
        //    ChibiSource = "ms-appx:///" + file;
        //}

        //#endregion

        //#region IComparer

        //public int Compare(IVocabItem x, IVocabItem y)
        //{
        //    return x.NextRound - y.NextRound;
        //}

        //#endregion

        //#region Visibility
        
        //private Visibility answerVisibility = Visibility.Collapsed;
        //public Visibility AnswerVisibility
        //{
        //    get => answerVisibility;
        //    set => SetProperty(ref answerVisibility, value);
        //}
        
        //private Visibility questionVisibility = Visibility.Visible;
        //public Visibility QuestionVisibility
        //{
        //    get => questionVisibility;
        //    set => SetProperty(ref questionVisibility, value);
        //}
        
        //private void SwitchVisibilities()
        //{
        //    if (EPracticeState.Current == EPracticeState.Questioning)
        //    {
        //        AnswerVisibility = Visibility.Collapsed;
        //        QuestionVisibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        AnswerVisibility = Visibility.Visible;
        //        QuestionVisibility = Visibility.Collapsed;
        //    }
        //}

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //private Visibility editViewVisibility = Visibility.Collapsed;
        //public Visibility EditViewVisibility
        //{
        //    get => editViewVisibility;
        //    set => SetProperty(ref editViewVisibility, value);
        //}
        
        //private Visibility practiceViewVisibility = Visibility.Visible;
        //public Visibility PracticeViewVisibility
        //{
        //    get => practiceViewVisibility;
        //    set => SetProperty(ref practiceViewVisibility, value);
        //}

        //private void SwitchEditViewVisibility()
        //{
        //    if(ESenpaiState.Current == ESenpaiState.EditMode)
        //    {
        //        EditViewVisibility     = Visibility.Visible;
        //        PracticeViewVisibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        EditViewVisibility     = Visibility.Collapsed;
        //        PracticeViewVisibility = Visibility.Visible;
        //    }
        //}

        //#endregion

        //#region Others

        //private String CreateItemsLeftString()
        //{
        //    return String.Format("{0} / {1}", ActiveItems.Count, GerItems.Count + JapItems.Count);
        //}

        //#endregion
    }
}
