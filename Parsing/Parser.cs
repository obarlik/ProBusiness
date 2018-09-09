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

        public int Index { get; protected set; }


        protected virtual void Parse(string s)
        {
            Buffer = s.GetEnumerator();
            Index = -1;
            Next();
        }


        public char Current
        {
            get { return !Eof ? Buffer.Current : default(char); }
        }


        public bool Eof { get; protected set; }


        public void Next()
        {
            Eof = !Buffer.MoveNext();

            if (!Eof) Index++;
        }


        public IEnumerable<char> ReadWhile(
            Func<bool> condition, Func<int, bool> escapeCondition = null)
        {
            while (!Eof)
            {
                if (!condition())
                {
                    if (escapeCondition == null
                     || escapeCondition(0))
                        break;

                    Next();

                    if (Eof || escapeCondition(1))
                        break;
                }

                yield return Current;
                Next();
            }
        }


        public int SkipWhite()
        {
            return ReadWhile(c => char.IsWhiteSpace(c))
                   .Count();
        }


        
        public string ReadRegex(string regEx)
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


        public int ReadInteger()
        {
            return int.Parse(ReadRegex(@"\d+"));
        }



        public void Match(string m)
        {
            SkipWhite();

            var i = 0;

            if (ReadWhile(c => i < m.Length && c == m[i++]).Count() != m.Length)
                throw Error(m + " expected!");
        }


        public Exception Error(string msg)
        {
            return new InvalidOperationException(msg + " @" + Index);
        }



    }
}
