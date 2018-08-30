using SenpaiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiUtilities
{
    public class LessonComparer : IComparer<Lesson>
    {
        public int Compare(Lesson x, Lesson y)
        {
            return x.SortIndex - y.SortIndex;
        }
    }
}
