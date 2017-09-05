using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment3
{
    public class Stock
    {
        private string _stockName;
        private int _stockInitialValue;
        private int _stockCurrentValue;
        private int _maxChange;
        private int _threshold;
        private Thread _thread;
        private int _numberChanges = 0;
        // Declare the event
        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;

        public Stock(string strStockName, int intStockInitialValue, int intMaxChange, int intThreshold)
        {
            _stockName = strStockName;
            _stockInitialValue = intStockInitialValue;
            _stockCurrentValue = _stockInitialValue;
            _maxChange = intMaxChange;
            _threshold = intThreshold;
            _thread = new Thread(new ThreadStart(Activate));
            _thread.Name = _stockName;
            _thread.Start();
        }

        private void Activate()
        {
            for (int i = 0; i < 15; i++)
            {
                Thread.Sleep(500);
                ChangeStockValue();
            }
        }

        // Raise the event
        private void ChangeStockValue()
        {
            Random rnd = new Random();
            _stockCurrentValue += rnd.Next(1, _maxChange);
            
            if (_stockCurrentValue - _stockInitialValue > _threshold)
            {
                _numberChanges++;
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.StockName = _stockName;
                args.CurrentValue = _stockCurrentValue;
                args.Changes = _numberChanges;
                OnThresholdReached(args);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            /*
            // C# 5.0
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
            */
            // C# 6.0
            ThresholdReached?.Invoke(this, e);
        }
    }
}
