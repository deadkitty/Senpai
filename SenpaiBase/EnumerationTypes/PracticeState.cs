using Nyantilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiBase.EnumerationTypes
{
    public class EPracticeState : NyaEnum<String>
    {
        #region EnumValues

        public const String _Answering     = "Answering";
        public const String _Questioning   = "Questioning";
        public const String _RoundFinished = "RoundFinished";
        public const String _Editing       = "Editing";
        public const String _Undefined     = "Undefined";

        public static readonly EPracticeState Answering      = new EPracticeState(_Answering);
        public static readonly EPracticeState Questioning   = new EPracticeState(_Questioning);
        public static readonly EPracticeState RoundFinished = new EPracticeState(_RoundFinished);
        public static readonly EPracticeState Editing       = new EPracticeState(_Editing);
        public static readonly EPracticeState Undefined     = new EPracticeState(_Undefined);

        #endregion

        #region Constructor

        public EPracticeState(String value)
            : base(value)
        {

        }

        #endregion

        #region Implicit Operator

        public static implicit operator EPracticeState(String @value)
        {
            switch (value)
            {
                case _Answering    : return Answering;
                case _Questioning  : return Questioning;
                case _RoundFinished: return RoundFinished;
                case _Editing      : return Editing;
            }

            return Undefined;
        }

        #endregion
    }
}
