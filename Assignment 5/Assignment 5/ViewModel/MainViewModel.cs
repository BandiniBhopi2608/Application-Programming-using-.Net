using System;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Assignment_5.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            BooksEntities dbcontext = new BooksEntities();

            var titleAndAuthors = from title in dbcontext.Titles
                                  from author in title.Authors
                                  orderby title.Title1
                                  select new { title.Title1, author.FirstName, author.LastName };

            Result += "Titles and Authors:";
            foreach (var element in titleAndAuthors)
            {
                Result += String.Format("\r\n\t{0,-10} {1} {2}",
                          element.Title1, element.FirstName, element.LastName);
            } // end foreach

            var authorsAndTitles = from book in dbcontext.Titles
                                   from author in book.Authors
                                   orderby book.Title1, author.LastName, author.FirstName
                                   select new { author.FirstName, author.LastName, book.Title1 };

            Result += "\r\n\r\nAuthors and titles with authors sorted for each title:";
            foreach (var element in authorsAndTitles)
            {
                Result += String.Format("\r\n\t{0,-10} {1} {2}",
                          element.Title1, element.FirstName, element.LastName);
            } // end foreach

            var titlesByAuthor = from book in dbcontext.Titles
                                 orderby book.Title1
                                 select new
                                 {
                                     Title = book.Title1,
                                     AuthorNames =
                                           from author in book.Authors
                                           orderby author.LastName, author.FirstName
                                           select author.FirstName + " " + author.LastName
                                 };

            Result += "\r\n\r\nTitles grouped by author:";
            foreach (var book in titlesByAuthor)
            {
                Result += "\r\n\t" + book.Title + ":";

                foreach (var Name in book.AuthorNames)
                {
                    Result += "\r\n\t\t" + Name;
                } 
            }
        }

        public string Result { get; set; }
    }
}