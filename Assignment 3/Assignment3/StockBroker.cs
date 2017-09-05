using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment3
{
    public class StockBroker
    {
        private string _brokerName;
        private List<Stock> _stocks;
        private string _stockInfo;
        private static readonly Object lockThis = new Object();

        static StockBroker()
        {
            Console.WriteLine("Broker".PadRight(20)
                              + "Stock".PadRight(20)
                              + "Value".PadRight(20)
                              + "Changes".PadRight(20));
            
            ReadWrite.SaveStockInfo("Date and Time".PadRight(40)
                                   + "Broker".PadRight(20)
                                   + "Stock".PadRight(20)
                                   + "Value".PadRight(20)
                                   + "Changes".PadRight(20));
        }
        
        public StockBroker()
        {
            _brokerName = "";
            _stockInfo = "";
            _stocks = new List<Stock>();
        }

        public StockBroker(string strBrokerName) : this()
        {
            _brokerName = strBrokerName;
        }

        public void AddStock(Stock objStock)
        {
            _stocks.Add(objStock);
            // Handle the event
            objStock.ThresholdReached += Notify;
        }

        private void Notify(object sender, ThresholdReachedEventArgs e)
        {
            lock (lockThis)
            {
                _stockInfo = _brokerName.PadRight(20)
                            + e.StockName.PadRight(20)
                            + e.CurrentValue.ToString().PadRight(20)
                            + e.Changes.ToString().PadRight(20);
                Console.WriteLine(_stockInfo);
                ReadWrite.SaveStockInfo(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt").PadRight(30) + _stockInfo);
            }
        }
    }
}
