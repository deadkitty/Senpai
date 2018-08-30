using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nyantilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiModel
{
    public static class DataManager
    {
        #region Fields

        private static SenpaiDatabase database;

        #endregion

        #region Properties

        public static SenpaiDatabase Database => database;

        #endregion

        #region Initialization

        public static void Initialize()
        {
            if (database == null)
            {
                database = new SenpaiDatabase();
                database.Database.Migrate();
            }
        }

        public static void Uninitialize()
        {
            if (database != null)
            {
                database.Dispose();
                database = null;
            }
        }

        #endregion

        #region Save

        public static void SaveChanges()
        {
            database.SaveChanges();
        }

        #endregion

        #region Reset Data

        public static void ResetDatabase()
        {
            foreach (Lesson lesson in database.Lessons)
            {
                ResetLesson(lesson);
            }

            SaveChanges();
        }

        /// <summary>
        /// resets all items in the selected lesson
        /// </summary>
        public static void ResetLesson(Lesson lesson)
        {
            if (lesson.Words == null)
            {
                lesson.Words = Database.Words.Where(x => x.Lesson == lesson).ToList();
            }

            foreach (Word word in lesson.Words)
            {
                word.Reset();
            }

            if (lesson.Kanjis == null)
            {
                lesson.Kanjis = Database.Kanjis.Where(x => x.Lesson == lesson).ToList();
            }

            foreach (Kanji kanji in lesson.Kanjis)
            {
                kanji.Reset();
            }

            lesson.NextRound = 0;
        }

        #endregion

        #region Import

        public static String ImportFromFile(Stream contentStream)
        {
            List<Lesson> lessons;

            String updateStatus = null;
            //import from new json files
            using (StreamReader file = new StreamReader(contentStream))
            {
                JsonSerializer serializer = new JsonSerializer();
                lessons = serializer.Deserialize(file, typeof(List<Lesson>)) as List<Lesson>;
                updateStatus = ImportLessons(lessons);
            }
            return updateStatus;
        }

        private static String ImportLessons(List<Lesson> lessons)
        {
            StringBuilder sb = new StringBuilder();

            int importedLessons = lessons.Count;
            int importedWords   = 0;
            int importedKanjis  = 0;

            foreach (Lesson lesson in lessons)
            {
                importedWords  += lesson.Words.Count;
                importedKanjis += lesson.Kanjis.Count;

                if (lesson.Words.Count > 0)
                {
                    Database.Words.AddRange(lesson.Words);
                }
                if (lesson.Kanjis.Count > 0)
                {
                    Database.Kanjis.AddRange(lesson.Kanjis);
                }

                Database.Lessons.Add(lesson);
            }

            sb.AppendLine("Update erfolreich v(o__o)v");
            sb.AppendLine("Importierte Lektionen:\t" + importedLessons);
            sb.AppendLine("Importierte Wörter:\t"    + importedWords);
            sb.AppendLine("Importierte Kanjis:\t"    + importedKanjis);

            SaveChanges();

            return sb.ToString();
        }

        #endregion

        #region Export

        public static String Export(Stream exportStream)
        {
            List<Lesson> lessons = 
                database.Lessons.Include(x => x.Words)
                                .Include(x => x.Kanjis).ToList();
            
            //TODO: nochmal testen was das macht und dann raus damit (oder auch nich)
            //foreach (Lesson item in lessons)
            //{
            //    foreach (Word word in item.Words)
            //    {
            //        word.Lesson = null;
            //    }
            //    foreach (Kanji kanji in item.Kanjis)
            //    {
            //        kanji.Lesson = null;
            //    }
            //}
            //String json = JsonConvert.SerializeObject(lessons, Formatting.None);

            using (StreamWriter sw = new StreamWriter(exportStream))
            {
                try
                {
                    sw.Write(JsonConvert.SerializeObject(lessons, Formatting.Indented));
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteLine(ex.Message);
                }
            }

            int exportedLessons = lessons.Count;
            int exportedWords   = 0;
            int exportedKanjis  = 0;

            foreach (Lesson lesson in lessons)
            {
                exportedWords  += lesson.Words .Count;
                exportedKanjis += lesson.Kanjis.Count;
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Datenbank erfolgreich exportiert! :3");
            sb.AppendLine("Lektionen\t: " + exportedLessons);
            sb.AppendLine("Wörter\t\t: "  + exportedWords);
            sb.AppendLine("Kanjis\t\t: "  + exportedKanjis);
            
            return sb.ToString();
        }

        #endregion

        #region Searching

        public static List<Word> SearchForItems(String searchText)
        {
            if (!String.IsNullOrEmpty(searchText))
            {
                return Database.Words.Where(x => x.Kana                 .Contains(searchText)
                                              || x.Kanji                .Contains(searchText)
                                              || x.Translation.ToLower().Contains(searchText.ToLower())).ToList();
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
