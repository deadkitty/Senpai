using Nyantilities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiModel
{
    public class LessonItem : NyaObservable
    {
        public int Id { get; set; }
        public String Type { get; set; }

        [NotMapped] public virtual String Content { get; }
        [NotMapped] public virtual String Details { get; }

        public virtual void Reset() { }
    }
}
