using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiPracticing.Data
{
    /// <summary>
    /// Saves the old state of an item when inserting in the vocab backstack,
    /// if the user goes back the item is used to restore the old values of
    /// the last item
    /// </summary>
    public class BackItem
    {
        public IVocabItem VocabItem { get; set; }

        private int LastRound;
        private int NextRound;
        private float EFactor;
        private int Answer;

        public BackItem(IVocabItem item)
        {
            VocabItem = item;
            LastRound = item.LastRound;
            NextRound = item.NextRound;
            EFactor   = item.EFactor;
            Answer    = item.Answer;
        }

        public void RollBack()
        {
            VocabItem.LastRound = LastRound;
            VocabItem.NextRound = NextRound;
            VocabItem.EFactor   = EFactor;
            VocabItem.Answer    = Answer;
        }

        public override string ToString()
        {
            return "BackItem = " + VocabItem.ToString();
        }
    }
}
