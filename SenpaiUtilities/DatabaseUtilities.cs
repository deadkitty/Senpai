using SenpaiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiUtilities
{
    public static class DatabaseUtilities
    {
        public static List<Word> GetWords(this Lesson lesson)
        {
            return lesson == null ? null : DataManager.Database.Words.Where(x => x.Lesson == lesson).ToList();
        }

        public static List<LessonItem> GetItems(this Lesson lesson)
        {
            return lesson == null ? null : DataManager.Database.Words.Where(x => x.Lesson == lesson).ToList<LessonItem>();
        }
    }
}
