using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsing
{
    public class GrammarParser : Parser
    {
        public class Grammar : Parser
        {
            public Dictionary<string, Rule> Rules = new Dictionary<string, Rule>();
        }


        public class Rule
        {
            public string RuleName;
            public Sequence Sequence;
        }


        public class Sequence
        {
            public Sequence Alternative;
            public Sequence Next;
        }


        public class Empty : Sequence
        {

        }

        public class Terminal : Sequence
        {
            public string Value;
        }


        public class RuleName : Sequence
        {
            public string Name;
        }



        public class Optional : Sequence
        {
            public Sequence Sequence { get; internal set; }
        }


        public class Repetition : Sequence
        {
            public Sequence Sequence { get; internal set; }
        }


        public class Grouping : Sequence
        {
            public Sequence Sequence { get; internal set; }
        }


        public GrammarParser()
        {
        }
        

        public Grammar ReadEbnfGrammar(string text)
        {
            Buffer = text;

            var grammar = new Grammar();
            
            while (!Eof)
            {
                var rule = ReadRule();

                grammar.Rules.Add(rule.RuleName, rule);
            }

            return grammar;
        }



        protected string ReadRuleName()
        {
            SkipWhite();
            return ReadRegex("[A-Za-z][A-Za-z0-9_]*");
        }


        protected Rule ReadRule()
        {
            var rule = new Rule();

            SkipWhite();
            rule.RuleName = ReadRuleName();

            Match("=");

            rule.Sequence = ReadSequence();

            Match(";");

            return rule;
        }


        protected Sequence ReadSequence()
        {
            Sequence seq = null;

            if (!Eof)
            {
                SkipWhite();

                if (IsLetter)
                    seq = new RuleName() { Name = ReadRuleName() };
                else
                {
                    switch (Current)
                    {
                        case '\'':
                        case '"':
                            seq = new Terminal() { Value = ReadTerminal() };
                            break;

                        case '[':
                            Next();
                            seq = new Optional() { Sequence = ReadSequence() };
                            Match("]");
                            break;

                        case '{':
                            Next();
                            seq = new Repetition() { Sequence = ReadSequence() };
                            Match("}");
                            break;

                        case '(':
                            Next();
                            seq = new Grouping() { Sequence = ReadSequence() };
                            Match(")");
                            break;
                    }
                }

                SkipWhite();

                if (!Eof)
                    switch (Current)
                    {
                        case ',':
                            Next();

                            if (seq == null)
                                seq = new Empty();

                            seq.Next = ReadSequence();
                            break;

                        case '|':
                            Next();

                            if (seq == null)
                                seq = new Empty();

                            seq.Alternative = ReadSequence();
                            break;
                    }
            }

            if (seq == null)
                seq = new Empty();

            return seq;
        }


        protected bool IsLower
        {
            get { return "abcdefghijklmnopqrstuvwxyz".Contains(Current); }
        }


        protected bool IsUpper
        {
            get { return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(Current); }
        }


        protected bool IsLetter
        {
            get { return IsLower || IsUpper; }
        }


        protected bool IsDigit
        {
            get { return "0123456789".Contains(Current); }
        }


        protected bool IsSpecial
        {
            get { return "()[]{}<=>-+*/\\\"'.,:;|@$%&#_!?^~".Contains(Current); }
        }


        protected bool IsCharacter
        {
            get { return IsLetter || IsDigit || IsSpecial; }
        }


        protected string ReadTerminal()
        {
            var end = Current;

            if (end != '"' && end != '\'')
                throw Error("Terminal expected!");

            Next();

            var sb = new StringBuilder();

            if (!Eof)
            {
                sb.Append(
                    ReadWhile(() => Current != end && IsCharacter,
                              s => s == 1 || Current == '\\')
                    .ToArray());
            }

            return sb.ToString();
        }





        /*
  Even EBNF can be described using EBNF. Consider the sketched grammar below: 
  letter = "A" |  "B" |  "C" |  "D" |  "E" |  "F" |  "G"
        |  "H" |  "I" |  "J" |  "K" |  "L" |  "M" |  "N"
        |  "O" |  "P" |  "Q" |  "R" |  "S" |  "T" |  "U"
        |  "V" |  "W" |  "X" |  "Y" |  "Z" |  "a" |  "b"
        |  "c" |  "d" |  "e" |  "f" |  "g" |  "h" |  "i"
        |  "j" |  "k" |  "l" |  "m" |  "n" |  "o" |  "p"
        |  "q" |  "r" |  "s" |  "t" |  "u" |  "v" |  "w"
        |  "x" |  "y" |  "z" ;
 digit = "0" |  "1" |  "2" |  "3" |  "4" |  "5" |  "6" |  "7" |  "8" |  "9" ;
 symbol = "[" |  "]" |  "{" |  "}" |  "(" | ")" |  "<" |  ">"
        |  "'" |  '"' | "=" | "|" | "." | "," | ";" ;
 character = letter |  digit |  symbol |  "_" ;
 
 identifier = letter , { letter |  digit |  "_" } ;
 terminal = "'" , character , { character } , "'" 
          |  '"' , character , { character } , '"' ;
 
 lhs = identifier ;
 rhs = identifier
      |  terminal
      |  "[" , rhs , "]"
      |  "{" , rhs , "}"
      |  "(" , rhs , ")"
      |  rhs , "|"  , rhs
      |  rhs , "," , rhs ;

 rule = lhs , "=" , rhs , ";"  ;
 grammar = { rule } ;


         BNF's syntax itself may be represented with a BNF like the following: 
  
         <syntax> ::= <rule> | <rule> <syntax>
         <rule> ::= <opt-whitespace> "<" <rule-name> ">" <opt-whitespace> " ::= " <opt-whitespace> <expression> <line-end>
         <opt-whitespace> ::= " " <opt-whitespace> | ""
         <expression> ::= <list> | <list> <opt-whitespace> "|" <opt-whitespace> <expression>
         <line-end> ::= <opt-whitespace> <EOL> | <line-end> <line-end>
         <list> ::= <term> | <term> <opt-whitespace> <list>
         <term> ::= <literal> | "<" <rule-name> ">"
         <literal> ::= '"' <text1> '"' | "'" <text2> "'"
         <text1> ::= "" | <character1> <text1>
         <text2> ::= "" | <character2> <text2>
         <character> ::= <letter> | <digit> | <symbol>
         <letter> ::= "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K" | "L" | "M" | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V" | "W" | "X" | "Y" | "Z" | "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m" | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z"
         <digit> ::= "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
         <symbol> ::= "|" | " " | "!" | "#" | "$" | "%" | "&" | "(" | ")" | "*" | "+" | "," | "-" | "." | "/" | ":" | ";" | ">" | "=" | "<" | "?" | "@" | "[" | "\" | "]" | "^" | "_" | "`" | "{" | "}" | "~"
         <character1> ::= <character> | "'"
         <character2> ::= <character> | '"'
         <rule-name> ::= <letter> | <rule-name> <rule-char>
         <rule-char> ::= <letter> | <digit> | "-"

        */
    }
}
