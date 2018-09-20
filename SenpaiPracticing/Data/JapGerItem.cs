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
    public class JapGerItem : NyaObservable, IVocabItem
    {
        private Word sourceWord;
        public Word Source
        {
            get => sourceWord;
            set => SetProperty(ref sourceWord, value);
        }

        public string ShownText => String.IsNullOrWhiteSpace(Source.Kanji) ? Source.Kana : Source.Kanji;

        public string HiddenText1 => String.IsNullOrWhiteSpace(Source.Kanji) ? Source.Translation : Source.Kana;
        public string HiddenText2 => String.IsNullOrWhiteSpace(Source.Kanji) ? String.Empty : Source.Translation;
        
        public string Description1 => GetDescriptionText();
        public string Description2 => Source.ToDescriptionString();

        public string Example => Source.Example;
        
        public int LastRound { get => Source.LastRoundJap; set => Source.LastRoundJap = value; }
        public int NextRound { get => Source.NextRoundJap; set => Source.NextRoundJap = value; }
        public float EFactor { get => Source.  EFactorJap; set => Source.  EFactorJap = value; }

        public int Answer { get; set; } = -1;
        
        public JapGerItem(Word source)
        {
            Source = source;
        }
        
        #region ToString
        
        public override string ToString()
        {
            return String.Format("JapGerItem {0} = {1}", NextRound, Source.ToString());
        }

        private String GetDescriptionText()
        {
            switch (Source.ShowDesc)
            {
                case EVisibilityType._ShowNone: return ((EVocabType)Source.VocabType).TypeStr;
                case EVisibilityType._ShowGer : return ((EVocabType)Source.VocabType).TypeStr;
                case EVisibilityType._ShowJap : return Source.ToDescriptionString();
                case EVisibilityType._ShowBoth: return Source.ToDescriptionString();
            }

            return null;
        }

        #endregion
    }
}
