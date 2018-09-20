using Nyantilities.Core;
using SenpaiBase.EnumerationTypes;
using SenpaiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiPracticing.Data
{
    public class GerJapItem : NyaObservable, IVocabItem
    {
        private Word sourceWord;
        public Word Source
        {
            get => sourceWord;
            set => SetProperty(ref sourceWord, value);
        }
        
        public string ShownText => Source.Translation;

        public string HiddenText1 => String.IsNullOrEmpty(Source.Kanji) ? Source.Kana : Source.Kanji;
        public string HiddenText2 => String.IsNullOrEmpty(Source.Kanji) ? String.Empty : Source.Kana;

        public string Description1 => GetDescriptionText();
        public string Description2 => Source.ToDescriptionString();

        public string Example => Source.Example;

        public int LastRound { get => Source.LastRoundGer; set => Source.LastRoundGer = value; }
        public int NextRound { get => Source.NextRoundGer; set => Source.NextRoundGer = value; }
        public float EFactor { get => Source.  EFactorGer; set => Source.  EFactorGer = value; }

        public int Answer { get; set; } = -1;

        public GerJapItem(Word source)
        {
            Source = source;
        }

        #region ToString

        public override string ToString()
        {
            return String.Format("GerJapItem {0} = {1}", NextRound, Source.ToString());
        }
        
        private String GetDescriptionText()
        {
            switch (Source.ShowDesc)
            {
                case EVisibilityType._ShowNone: return ((EVocabType)Source.VocabType).TypeStr;
                case EVisibilityType._ShowGer : return Source.ToDescriptionString();
                case EVisibilityType._ShowJap : return ((EVocabType)Source.VocabType).TypeStr;
                case EVisibilityType._ShowBoth: return Source.ToDescriptionString();
            }

            return null;
        }
        
        #endregion
    }
}
