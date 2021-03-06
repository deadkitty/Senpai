﻿using Nyantilities;
using Nyantilities.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiBase.EnumerationTypes
{
    public class ESenpaiState : NyaEnum<String>
    {
        #region EnumValues

        public const String _PracticeMode = "PracticeMode";
        public const String _EditMode     = "EditMode";
        public const String _MainMenu     = "MainMenu";
        public const String _Undefined    = "Undefined";

        public static readonly ESenpaiState PracticeMode = new ESenpaiState("PracticeMode");
        public static readonly ESenpaiState EditMode     = new ESenpaiState("EditMode");
        public static readonly ESenpaiState MainMenu     = new ESenpaiState("MainMenu");
        public static readonly ESenpaiState Undefined    = new ESenpaiState("Undefined");
        
        #endregion

        #region Constructor

        public ESenpaiState(String value)
            : base(value)
        {
            
        }

        #endregion

        #region Implicit Operator

        public static implicit operator ESenpaiState(String @value)
        {
            switch (value)
            {
                case _PracticeMode: return PracticeMode;
                case _EditMode    : return EditMode;
                case _MainMenu    : return MainMenu;
                case _Undefined   : return Undefined;
            }

            throw new ArgumentOutOfRangeException();
        }

        #endregion
    }
}
