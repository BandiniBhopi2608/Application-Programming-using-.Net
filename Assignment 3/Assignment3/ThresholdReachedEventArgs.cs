using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    // Class to consume Event Data.
    public class ThresholdReachedEventArgs : EventArgs
    {
        public string StockName { get; set; }
        public int CurrentValue { get; set; }
        public int Changes { get; set; }
    }
}
