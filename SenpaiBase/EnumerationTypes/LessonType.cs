using Nyantilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SenpaiBase.EnumerationTypes
{
    public class ELessonType : NyaEnum<String>
    {
        public static readonly ELessonType Vocab   = new ELessonType("Vocab");
        public static readonly ELessonType Kanji   = new ELessonType("Kanji");
        public static readonly ELessonType Grammar = new ELessonType("Grammar");
        
        protected ELessonType(string value)
            : base(value)
        {
        }

        public static implicit operator ELessonType(String value)
        {
            return Convert(value);
        }

        public static ELessonType Convert(String value)
        {
            switch (value)
            {
                case "Vocab"  : return Vocab;
                case "Kanji"  : return Kanji;
                case "Grammar": return Grammar;
            }

            throw new ArgumentOutOfRangeException();
        }
    }

    public class LessonTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ELessonType.Convert(parameter as String);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return parameter;
        }
    }
}
