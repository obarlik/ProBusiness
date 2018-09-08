using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Parsing
{
    public class Calculator : Parser
    {
        public Calculator()
        {
        }


        Dictionary<string, decimal> Variables = new Dictionary<string, decimal>();


        public decimal this[string variable]
        {
            get
            {
                decimal result;
                return Variables.TryGetValue(variable, out result) ? result : 0m;
            }

            set
            {
                Variables[variable] = value;
            }
        }


        decimal CalculationResult;


        public decimal Calculate(string expression)
        {
            Parse(expression);
            return CalculationResult;
        }


        protected override void Parse(string s)
        {
            base.Parse(s);

            CalculationResult = ReadExpression();
        }


        private decimal ReadExpression()
        {
            var result = ReadTerm();

            while (!Eof)
            {
                SkipWhite();

                switch (Current)
                {
                    case '+': Next(); result += ReadTerm(); break;
                    case '-': Next(); result -= ReadTerm(); break;
                    case '=': Next(); result = result == ReadTerm() ? 1 : 0; break;
                    case '>': Next(); result = result > ReadTerm() ? 1 : 0; break;

                    case '<':
                    case '!':
                        var old = Current;
                        Next();

                        if ((old == '<' && Current == '>')
                         || (old == '!' && Current == '='))
                        {
                            Next();
                            result = result != ReadTerm() ? 1 : 0;
                        }
                        else if (old == '<')
                            result = result < ReadTerm() ? 1 : 0;
                        else if (old == '!')
                            throw Error("= expected!");
                        
                        break;

                    case '?':
                        Next();
                        var fTrue = ReadExpression();
                        Match(":");
                        var fFalse = ReadExpression();

                        result = result != 0m ? fTrue : fFalse;
                        break;

                    default:
                        return result;
                }
            }

            return result;
        }


        private decimal ReadTerm()
        {
            var result = ReadFactor();

            while (!Eof)
            {
                SkipWhite();

                switch (Current)
                {
                    case '*':
                        Next();
                        result *= ReadFactor();
                        break;

                    case '/':
                        Next();
                        result /= ReadFactor();
                        break;

                    default:
                        return result;
                }
            }

            return result;
        }


        private decimal ReadFactor()
        {
            SkipWhite();
            var result = 0m;

            switch (Current)
            {
                case '(':
                    {
                        Next();
                        result = ReadExpression();
                        Match(")");
                    }
                    break;

                default:
                    if (char.IsDigit(Current))
                    {
                        result = ReadNumber();
                    }
                    else
                    {
                        var vName = ReadRegex("[A-Za-z]+");

                        if (!Variables.ContainsKey(vName))
                            throw Error("Variable '" + vName + "' not found!");

                        result = this[vName];
                    }

                    SkipWhite();

                    if (Current == '^')
                    {
                        Next();
                        result = (decimal)Math.Pow((double)result, (double)ReadFactor());
                    }

                    break;
            }

            return result;
        }


        private decimal ReadNumber()
        {
            return decimal.Parse(ReadRegex(@"\d+(\.\d+)?"), CultureInfo.InvariantCulture);
        }

    }
}
