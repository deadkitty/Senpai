using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiModel
{
    public class Lesson
    {
        #region Fields

        public int Id { get; set; }

        public String Name { get; set; }

        public String Type { get; set; }

        public int Size { get; set; }

        public int NextRound { get; set; }

        public int SortIndex { get; set; }

        public List<Word> Words { get; set; }
        public List<Kanji> Kanjis { get; set; }

        [NotMapped]
        public String Details
        {
            get => NextDueString();
        }

        #endregion

        #region Constructor

        public Lesson()
        {

        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Name;
        }

        public String NextDueString()
        {
            if (NextRound == 0) return null;

            DateTime next = new DateTime(2018, 1, 1);
            next = next.AddHours(NextRound);

            return String.Format("Fälligkeit: {0}, {1} Uhr", next.ToShortDateString(), next.ToShortTimeString());
        }

        #endregion
    }
}
