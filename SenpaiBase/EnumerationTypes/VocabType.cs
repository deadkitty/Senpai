using Nyantilities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiBase.EnumerationTypes
{
    public class EVocabType : NyaEnum<String>
    {
        #region Constants

        public const String _Other        = "Other";
        public const String _Noun         = "Noun";
        public const String _Verb1Trans   = "Verb1Trans";
        public const String _Verb2Trans   = "Verb2Trans";
        public const String _Verb1Intrans = "Verb1Intrans";
        public const String _Verb2Intrans = "Verb2Intrans";
        public const String _Verb3        = "Verb3";
        public const String _IAdjective   = "IAdjective";
        public const String _NaAdjective  = "NaAdjective";
        public const String _NoAdjective  = "NoAdjective";
        public const String _IrrAdjective = "IrrAdjective";
        public const String _AdjNoConj    = "AdjNoConj";
        public const String _DemPronoun   = "DemPronoun";
        public const String _DemAdjective = "DemAdjective";
        public const String _CounterWord  = "CounterWord";
        public const String _Interjection = "Interjection";
        public const String _Adverb       = "Adverb";
        public const String _Particle     = "Particle";
        public const String _Conjugation  = "Conjugation";
        public const String _Conjunction  = "Conjunction";
        public const String _Suffix       = "Suffix";
        public const String _Prefix       = "Prefix";
        public const String _Phrase       = "Phrase";
        public const String _Name         = "Name";
        public const String _Pronoun      = "Pronoun";

        #endregion

        #region Items

        public static readonly EVocabType Other        = new EVocabType(_Other       , null);
        public static readonly EVocabType Noun         = new EVocabType(_Noun        , "Nomen");
        public static readonly EVocabType Verb1Trans   = new EVocabType(_Verb1Trans  , "う-Verb Transitiv");
        public static readonly EVocabType Verb2Trans   = new EVocabType(_Verb2Trans  , "る-Verb Transitiv");
        public static readonly EVocabType Verb1Intrans = new EVocabType(_Verb1Intrans, "う-Verb Intransitiv");
        public static readonly EVocabType Verb2Intrans = new EVocabType(_Verb2Intrans, "る-Verb Intransitiv");
        public static readonly EVocabType Verb3        = new EVocabType(_Verb3       , "する-Verb");
        public static readonly EVocabType IAdjective   = new EVocabType(_IAdjective  , "い-Adjektiv");
        public static readonly EVocabType NaAdjective  = new EVocabType(_NaAdjective , "な-Adjektiv");
        public static readonly EVocabType NoAdjective  = new EVocabType(_NoAdjective , "の-Adjektiv");
        public static readonly EVocabType IrrAdjective = new EVocabType(_IrrAdjective, "irreguläres Adjektiv");
        public static readonly EVocabType AdjNoConj    = new EVocabType(_AdjNoConj   , "nicht konjugierbares Adjektiv");
        public static readonly EVocabType DemPronoun   = new EVocabType(_DemPronoun  , "demonstrativ Pronomen");
        public static readonly EVocabType DemAdjective = new EVocabType(_DemAdjective, "demonstrativ Adjektiv");
        public static readonly EVocabType CounterWord  = new EVocabType(_CounterWord , "Zähleinheitssuffix");
        public static readonly EVocabType Interjection = new EVocabType(_Interjection, "Interjektion");
        public static readonly EVocabType Adverb       = new EVocabType(_Adverb      , "Adverb");
        public static readonly EVocabType Particle     = new EVocabType(_Particle    , "Partikel");
        public static readonly EVocabType Conjugation  = new EVocabType(_Conjugation , "Konjugation");
        public static readonly EVocabType Conjunction  = new EVocabType(_Conjunction , "Konjunktion");
        public static readonly EVocabType Suffix       = new EVocabType(_Suffix      , "Suffix");
        public static readonly EVocabType Prefix       = new EVocabType(_Prefix      , "Präfix");
        public static readonly EVocabType Phrase       = new EVocabType(_Phrase      , "Phrase");
        public static readonly EVocabType Name         = new EVocabType(_Name        , "Name");
        public static readonly EVocabType Pronoun      = new EVocabType(_Pronoun     , "Pronomen");

        #endregion

        #region Properties

        public String TypeStr { get; private set; }

        #endregion

        #region Constructor

        protected EVocabType(String value, String typeStr)
            : base(value)
        {
            TypeStr = typeStr;
        }

        #endregion

        #region NyaEnum
        
        public static implicit operator EVocabType(String @value)
        {
            switch (value)
            {
                case _Noun        : return Noun;
                case _Verb1Trans  : return Verb1Trans;
                case _Verb2Trans  : return Verb2Trans;
                case _Verb1Intrans: return Verb1Intrans;
                case _Verb2Intrans: return Verb2Intrans;
                case _Verb3       : return Verb3;
                case _IAdjective  : return IAdjective;
                case _NaAdjective : return NaAdjective;
                case _NoAdjective : return NoAdjective;
                case _IrrAdjective: return IrrAdjective;
                case _AdjNoConj   : return AdjNoConj;
                case _DemPronoun  : return DemPronoun;
                case _DemAdjective: return DemAdjective;
                case _CounterWord : return CounterWord;
                case _Interjection: return Interjection;
                case _Adverb      : return Adverb;
                case _Particle    : return Particle;
                case _Conjugation : return Conjugation;
                case _Conjunction : return Conjunction;
                case _Suffix      : return Suffix;
                case _Prefix      : return Prefix;
                case _Phrase      : return Phrase;
                case _Name        : return Name;
                case _Pronoun     : return Pronoun;
            }

            return Other;
        }

        public override string ToString()
        {
            return TypeStr;
        }

        public static List<EVocabType> GetList()
        {
            return new List<EVocabType>()
            {
                  new EVocabType("Other", "Sonstige")
                , Noun
                , Verb1Trans
                , Verb2Trans
                , Verb1Intrans
                , Verb2Intrans
                , Verb3
                , IAdjective
                , NaAdjective
                , NoAdjective
                , IrrAdjective
                , AdjNoConj
                , DemPronoun
                , DemAdjective
                , CounterWord
                , Interjection
                , Adverb
                , Particle
                , Conjugation
                , Conjunction
                , Suffix
                , Prefix
                , Phrase
                , Name
                , Pronoun
            };
        }

        #endregion
    }
}
