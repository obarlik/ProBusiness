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


        protected IEnumerator<char> BufferEnumerator;

        public int Index { get; protected set; }


        string _Buffer;

        public string Buffer
        {
            get { return _Buffer; }
            set
            {
                _Buffer = value;
                Initialize(_Buffer);
            }
        }


        protected void Initialize(string s)
        {
            BufferEnumerator = s.GetEnumerator();
            Index = -1;
            Next();
        }


        public char Current
        {
            get { return !Eof ? BufferEnumerator.Current : default(char); }
        }


        public bool Eof { get; protected set; }


        public void Next()
        {
            Eof = !BufferEnumerator.MoveNext();

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
            return ReadWhile(() => char.IsWhiteSpace(Current))
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
                ReadWhile(() =>
                {
                    sb.Append(Current);

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

            if (ReadWhile(() => i < m.Length && Current == m[i++]).Count() != m.Length)
                throw Error(m + " expected!");
        }


        public Exception Error(string msg)
        {
            return new InvalidOperationException(msg + " @" + Index);
        }



    }
}
