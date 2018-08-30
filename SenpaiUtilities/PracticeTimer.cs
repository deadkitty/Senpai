using Nyantilities.Core;
using SenpaiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiUtilities
{
    public static class PracticeTimer
    {
        #region Properties

        private static int currentRound = 0;
        public static int CurrentRound
        {
                    get => currentRound;
            private set => currentRound = value;
        }

        public static readonly DateTime Origin = new DateTime(2018, 1, 1);

        #endregion

        #region Public Methods

        public static DateTime NextDueDate(Lesson lesson)
        {
            if (lesson?.NextRound == 0) return Origin;

            DateTime next = Origin;
            next = next.AddHours(lesson.NextRound);

            return next;
        }

        public static String NextDueString(Lesson lesson)
        {
            if (lesson?.NextRound == 0) return null;

            DateTime next = NextDueDate(lesson);

            return String.Format("Fälligkeit: {0}, {1} Uhr", next.ToShortDateString(), next.ToShortTimeString());
        }

        public static void UpdateCurrentRound()
        {
            TimeSpan diff = DateTime.Now - Origin;

            CurrentRound = (int)Math.Floor(diff.TotalHours);
        }

        #endregion
    }
}
