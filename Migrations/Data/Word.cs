using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiModel
{
    public class Word : LessonItem
    {
        #region Fields

        public String Kana        { get; set; }
        public String Kanji       { get; set; }
        public String Translation { get; set; }
        public String Description { get; set; }
        public String Example     { get; set; }
        
        public float EFactorJap { get; set; }
        public int LastRoundJap { get; set; }
        public int NextRoundJap { get; set; }

        public float EFactorGer { get; set; }
        public int LastRoundGer { get; set; }
        public int NextRoundGer { get; set; }
        
        public String VocabType { get; set; }
        public String ShowWord  { get; set; }
        public String ShowDesc  { get; set; }

        public Lesson Lesson { get; set; }
        
        public override string Content => ToString();
        public override string Details => ToDescriptionString();

        #endregion

        #region Constructor

        public Word()
        {

        }

        public Word(Word other)
        {
            CopyValues(other);
        }

        #endregion

        #region ToString

        /// <summary>
        /// <para>Creates a string containing kanji,kana and translation of the given word</para>
        /// <para>Patterns:</para>
        /// <para>1. kanji - kana - translation</para>
        /// <para>2. kana - translation</para>
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Kanji != "")
            {
                sb.Append(Kanji);
                sb.Append(" - ");
            }

            sb.Append(Kana);
            sb.Append(" - ");
            sb.Append(Translation);

            return sb.ToString();
        }

        /// <summary>
        /// <para>Creates a string containing type and or description of the given word</para>
        /// <para>Patterns:</para>
        /// <para>1. type - description</para>
        /// <para>2. type</para>
        /// <para>3. description</para>
        /// </summary>
        public String ToDescriptionString()
        {
            return "";
        }

        #endregion

        #region LearnItem

        public override void Reset()
        {
            //reset translation members
            EFactorJap   = 2.5f;
            LastRoundJap = 0;
            NextRoundJap = 0;

            //reset japanese members
            EFactorGer   = 2.5f;
            LastRoundGer = 0;
            NextRoundGer = 0;
        }

        #endregion

        #region Util

        public void CopyValues(Word other)
        {
            Kana        = other.Kana;
            Kanji       = other.Kanji;
            Translation = other.Translation;
            Description = other.Description;
            Example     = other.Example;
            VocabType   = other.VocabType;
            ShowWord    = other.ShowWord;
            ShowDesc    = other.ShowDesc;
        }

        #endregion
    }
}
