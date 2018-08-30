using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiModel
{
    public class Kanji : LessonItem
    {
        #region Fields

        public String Sign    { get; set; }
        public String Meaning { get; set; }
        public String Onyomi  { get; set; }
        public String Kunyomi { get; set; }
        public String Example { get; set; }

        public float EFactor { get; set; }
        public int LastRound { get; set; }
        public int NextRound { get; set; }
        public int Timestamp { get; set; }

        [JsonIgnore] public Lesson Lesson { get; set; }

        #endregion

        #region Constructor

        public Kanji()
        {

        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Sign + " - " + Meaning + " - " + Onyomi + "、" + Kunyomi;
        }

        #endregion

        #region LearnItem

        public override void Reset()
        {
            EFactor = 2.5f;
            LastRound = 0;
            NextRound = 0;
            Timestamp = 0;
        }

        #endregion
    }
}
