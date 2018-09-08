using BPM.Persistency;
using System;

namespace BPM.Production
{
    public class Stock : Supply
    {
        public double Amount { get; set; }


        public Stock()
        {
        }
    }
}