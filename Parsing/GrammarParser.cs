using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsing
{
    public class Grammar : Dictionary<string, Rule>
    {
        
    }


    public class Rule
    {
        public string RuleName;
        public RHS Rhs;        
    }


    public class RHS : List<Sequence>
    {
    }


    public class Sequence
    {
        public SType SeqType;
        public string NT;
        public RHS ORG;

        public Sequence(SType seqType, string nt, RHS org)
        {
            SeqType = seqType;
            NT = nt;
            ORG = org;
        }
    }


    public enum SType
    {
        Empty,
        RuleName,
        Terminal,
        Option,
        Repetition,
        Group
    }



    public class GrammarParser : Parser
    {
        public GrammarParser()
        {
        }
        

        public Grammar ParseGrammar(string s)
        {
            Parse(s);
            return ReadGrammar();
        }
        

        public void Parse(string s, Grammar grammar)
        {

        }



        public Grammar ReadGrammar()
        {
            var grammar = new Grammar();
            Rule rule;

            while ((rule = ReadRule()) != null)
            {
                grammar.Add(rule.RuleName, rule);
            }

            return grammar;
        }



        public string ReadRuleName()
        {
            SkipWhite();
            return ReadRegex("[A-Za-z][A-Za-z0-9_]*");
        }


        public Rule ReadRule()
        {
            var rule = new Rule();

            SkipWhite();
            rule.RuleName = ReadRuleName();

            Match("=");

            rule.Rhs = ReadRhs();

            Match(";");

            return rule;
        }


        public RHS ReadRhs()
        {
            var rhs = new RHS();

            while (!Eof)
            {
                SkipWhite();

                if (IsCharacter)
                    rhs.Add(new Sequence(SType.RuleName, ReadRuleName(), null));
                else
                {
                    switch (Current)
                    {
                        case '\'':
                        case '"':
                            rhs.Add(new Sequence(SType.Terminal, ReadTerminal(), null));
                            break;

                        case '[':
                            Next();
                            rhs.Add(new Sequence(SType.Option, null, ReadRhs()));
                            Match("]");
                            break;

                        case '{':
                            Next();
                            rhs.Add(new Sequence(SType.Repetition, null, ReadRhs()));
                            Match("}");
                            break;

                        case '(':
                            Next();
                            rhs.Add(new Sequence(SType.Group, null, ReadRhs()));
                            Match(")");
                            break;

                        case ',':
                            Next();
                            continue;
                    }

                }

                break;
            }

            return rhs;
        }


        public bool IsLower
        {
            get { return "abcdefghijklmnopqrstuvwxyz".Contains(Current); }
        }


        public bool IsUpper
        {
            get { return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(Current); }
        }


        public bool IsLetter
        {
            get { return IsLower || IsUpper; }
        }


        public bool IsDigit
        {
            get { return "0123456789".Contains(Current); }
        }


        public bool IsSpecial
        {
            get { return "()[]{}<=>-+*/\\\"'.,:;|@$%&#_!?^~".Contains(Current); }
        }


        public bool IsCharacter
        {
            get { return IsLetter || IsDigit || IsSpecial; }
        }


        public string ReadTerminal()
        {
            var end = Current;

            if (end != '"' && end != '\"')
                throw Error("Terminal expected!");

            Next();

            var sb = new StringBuilder();

            while (!Eof)
            {
                sb.Append(
                    ReadWhile(() => Current != end && IsCharacter,
                              s => s == 1 || Current == '\\')
                    .ToArray());

                Next();
                SkipWhite();

                end = Current;

                if (end != '"' && end != '\"')
                    break;
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
