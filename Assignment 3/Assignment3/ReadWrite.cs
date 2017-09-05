using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Assignment3
{
    class ReadWrite
    {
        private const string dir = @"C:\C# 2015\Files\";
        private const string path = dir + "Stocks.txt";

        public static void SaveStockInfo(string strStockInfo)
        {
            // If the directory doesn't exist, create it.
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Create the output stream for a text file that exists.
            using (StreamWriter textOut = new StreamWriter(path,true))
            {
                textOut.WriteLine(strStockInfo);
            }
            
        }
    }
}
