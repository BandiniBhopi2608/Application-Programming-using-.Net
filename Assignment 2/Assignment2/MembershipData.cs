using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class MembershipData
    {
        private const string dir = @"C:\C# 2015\Files\";
        private const string path = dir + "Members.txt";

        public List<Member> GetMemberships()
        {
            List<Member> members = new List<Member>();
            if (File.Exists(path))
            {
                // Create the object for the input stream for a text file.
                using (StreamReader textIn = new StreamReader(
                                             new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read)))
                {
                    // Read the data from the file and store it in the ArrayList.
                    while (textIn.Peek() != -1)
                    {
                        string row = textIn.ReadLine();
                        string[] columns = row.Split('|');
                        Member objMember = new Member();
                        objMember.FirstName = columns[0];
                        objMember.LastName = columns[1];
                        objMember.Email = columns[2];
                        members.Add(objMember);
                    }
                }
            }
            return members;
        }

        public void SaveMemberships(List<Member> members)
        {
            // If the directory doesn't exist, create it.
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Create the output stream for a text file that exists.
            using (StreamWriter textOut = new StreamWriter(
                                          new FileStream(path, FileMode.Create, FileAccess.Write)))
            {

                // Write each customer from the list to the file.
                foreach (Member objMember in members)
                {
                    textOut.Write(objMember.FirstName + "|");
                    textOut.Write(objMember.LastName + "|");
                    textOut.WriteLine(objMember.Email);
                }
            }
        }
    }
}
