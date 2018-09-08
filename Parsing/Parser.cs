using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsing
{
    public abstract class Parser
    {
        public Parser()
        {
        }


        protected IEnumerator<char> Buffer;
        protected int Index;


        protected virtual void Parse(string s)
        {
            Buffer = s.GetEnumerator();
            Index = -1;
            Next();
        }


        protected char Current
        {
            get { return !Eof ? Buffer.Current : default(char); }
        }


        protected bool Eof;


        protected void Next()
        {
            Eof = !Buffer.MoveNext();

            if (!Eof) Index++;
        }


        protected IEnumerable<char> ReadWhile(Func<char, bool> condition)
        {
            while (!Eof && condition(Current))
            {
                yield return Current;
                Next();
            }
        }


        protected int SkipWhite()
        {
            return ReadWhile(c => char.IsWhiteSpace(c))
                   .Count();
        }


        protected string ReadRegex(string regEx)
        {
            var sb = new StringBuilder();

            if (!regEx.StartsWith("^"))
                regEx = "^" + regEx;

            if (!regEx.EndsWith("$"))
                regEx = regEx + "$";

            return new string(
                ReadWhile(c =>
                {
                    sb.Append(c);

                    return Regex.IsMatch(sb.ToString(), regEx);
                })
                .ToArray());
        }


        protected int ReadInteger()
        {
            return int.Parse(ReadRegex(@"\d+"));
        }



        protected void Match(string m)
        {
            SkipWhite();

            var i = 0;

            if (ReadWhile(c => i < m.Length && c == m[i++]).Count() != m.Length)
                throw Error(m + " expected!");
        }


        protected Exception Error(string msg)
        {
            return new InvalidOperationException(msg + " @" + Index);
        }



    }
}
