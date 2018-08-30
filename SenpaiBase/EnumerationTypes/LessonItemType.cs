using Nyantilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiBase.EnumerationTypes
{
    public class ELessonItemType : NyaEnum<String>
    {
        public static readonly ELessonItemType Vocab   = new ELessonItemType("Vocab");
        public static readonly ELessonItemType Kanji   = new ELessonItemType("Kanji");
        public static readonly ELessonItemType Grammar = new ELessonItemType("Grammar");

        protected ELessonItemType(String value)
            : base(value)
        {
        }
        
        public static implicit operator ELessonItemType(String @value)
        {
            switch (value)
            {
                case "Vocab":   return Vocab;
                case "Kanji":   return Kanji;
                case "Grammar": return Grammar;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
