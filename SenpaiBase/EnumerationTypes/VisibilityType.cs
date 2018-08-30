using Nyantilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiBase.EnumerationTypes
{
    public class EVisibilityType : NyaEnum<String>
    {
        public const String _ShowBoth = "ShowBoth";
        public const String _ShowGer  = "ShowGer";
        public const String _ShowJap  = "ShowJap";
        public const String _ShowNone = "ShowNone";

        public static readonly EVisibilityType ShowBoth = new EVisibilityType(_ShowBoth, "Immer anzeigen");
        public static readonly EVisibilityType ShowGer  = new EVisibilityType(_ShowGer , "Nur deutsch anz.");
        public static readonly EVisibilityType ShowJap  = new EVisibilityType(_ShowJap , "Nur japanisch anz.");
        public static readonly EVisibilityType ShowNone = new EVisibilityType(_ShowNone, "Nie anzeigen");

        public String TypeStr { get; private set; }

        protected EVisibilityType(String value, String typeStr)
            : base(value)
        {
            TypeStr = typeStr;
        }

        public static implicit operator EVisibilityType(String @value)
        {
            switch (value)
            {
                case _ShowBoth: return ShowBoth;
                case _ShowGer : return ShowGer;
                case _ShowJap : return ShowJap;
                case _ShowNone: return ShowNone;
            }

            throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            return TypeStr;
        }

        public static List<EVisibilityType> GetList()
        {
            return new List<EVisibilityType>()
            {
                  ShowBoth
                , ShowGer
                , ShowJap
                , ShowNone
            };
        }
    }
}
