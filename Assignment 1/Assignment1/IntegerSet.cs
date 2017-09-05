using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class IntegerSet
    {
        private bool[] arrIntegerSet;

        // Default constructor
        public IntegerSet()
        {
            // By default boolean array intialiazed to false.
            // Therefore just initializing the below array, it will create empty set 
            // i.e. it contains all false value.
            arrIntegerSet = new bool[101];
        }

        // Parameterized Constructor
        public IntegerSet(int[] intArray) : this()
        {
            foreach (int number in intArray)
            {
                if (number >= 0 && number < 101)
                {
                    arrIntegerSet[number] = true;
                }
            }
        }

        // Method Union creates a third set that is the set-theoretic union of two existing sets
        public IntegerSet Union(IntegerSet set2)
        {
            IntegerSet set3 = new IntegerSet();
            for (int i = 0; i < arrIntegerSet.Length; i++)
            {
                set3.arrIntegerSet[i] = this.arrIntegerSet[i] || set2.arrIntegerSet[i];
            }
            return set3;
        }

        // Method Intersection creates a third set which is the set-theoretic intersection of two existing sets
        public IntegerSet Intersection(IntegerSet set2)
        {
            IntegerSet set3 = new IntegerSet();
            for (int i = 0; i < arrIntegerSet.Length; i++)
            {
                set3.arrIntegerSet[i] = this.arrIntegerSet[i] && set2.arrIntegerSet[i];
            }
            return set3;
        }

        // Method InsertElement inserts a new integer intNumber into a set
        public void InsertElement(int intNumber)
        {
            if (intNumber >= 0 && intNumber < 101)
            {
                arrIntegerSet[intNumber] = true;
            }
        }

        // Method DeleteElement deletes integer intNumber from set
        public void DeleteElement(int intNumber)
        {
            if (intNumber >= 0 && intNumber < 101)
            {
                arrIntegerSet[intNumber] = false;
            }
        }

        // Method ToString returns a string containing a set as a list of numbers separated by spaces.
        // If set is empty, returns "---" 
        public override string ToString()
        {
            string strNumberList = "";
            for (int i = 0; i < arrIntegerSet.Length; i++)
            {
                if (arrIntegerSet[i] == true)
                {
                    strNumberList += i + " ";
                }
            }
            if (String.IsNullOrEmpty(strNumberList))
                return "---";
            else
                return strNumberList.TrimEnd();
        }

        // Method IsEqualTo determines whether two sets are equal.
        public bool IsEqualTo(IntegerSet set2)
        {
            bool IsEqual = true;
            for (int i = 0; i < arrIntegerSet.Length; i++)
            {
                if (arrIntegerSet[i] != set2.arrIntegerSet[i])
                {
                    IsEqual = false;
                    break;
                }
            }

            return IsEqual;
        }
    }
}
