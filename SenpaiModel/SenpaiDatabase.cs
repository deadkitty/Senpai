using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiModel
{
    /// <summary>
    /// erstellen einer sqlite datenbank mit ef-core in UWP
    /// https://docs.microsoft.com/de-de/ef/core/get-started/uwp/getting-started
    /// nach der anleitung das model bauen und die migration erstellen und dann die migration in ein ef-core projekt kopieren und nutzen. 
    /// Dabei aber nicht vergessen überall die namespaces aus dem modelprojekt umzubenennen.
    /// </summary>
    public class SenpaiDatabase : DbContext
    {
        #region Properties

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Kanji> Kanjis { get; set; }

        #endregion

        #region Constructor

        public SenpaiDatabase()
        {
        
        }

        #endregion

        #region Database Creation

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SenpaiDatabase.db");
        }

        #endregion
    }
}
