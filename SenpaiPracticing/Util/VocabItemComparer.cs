using SenpaiPracticing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiPracticing.Util
{
    public class VocabItemComparer : IComparer<IVocabItem>
    {
        public int Compare(IVocabItem x, IVocabItem y)
        {
            return x.NextRound - y.NextRound;
        }
    }
}
