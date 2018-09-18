using SenpaiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiPracticing.Data
{
    public interface IVocabItem
    {
        Word Source { get; set; }
        String ShownText { get; }
        String HiddenText1 { get; }
        String HiddenText2 { get; }
        
        /// <summary>
        /// returns the description, depending on the ShowDesc property
        /// </summary>
        String Description { get; }

        String Example { get; }
        
        int LastRound { get; set; }
        int NextRound { get; set; }
        float EFactor { get; set; }

        int Answer { get; set; }

        /// <summary>
        /// returns the descripton of the word
        /// </summary>
        String GetDescription();
    }
}
