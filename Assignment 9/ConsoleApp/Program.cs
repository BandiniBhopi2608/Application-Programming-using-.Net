using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AskOption:
            Console.WriteLine("Menu"
                            + "\r\n----------------------------------------"
                            + "\r\nWhat would you like to do?"
                            + "\r\n 1) Display all students"
                            + "\r\n 2) Add new Student"
                            + "\r\n 3) Update Student"
                            + "\r\n 4) Delete Student"
                            + "\r\n 5) Search Student"
                            //+ "\r\n 6) Display Students related to Standard"
                            + "\r\n 7) Exit the program");

            string strOption = Console.ReadLine();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56372/api/");
                switch (strOption)
                {
                    case "1":
                        //HTTP GET
                        #region Get All Students
                        var responseTask = client.GetAsync("student");
                        responseTask.Wait();

                        var getResult = responseTask.Result;
                        if (getResult.IsSuccessStatusCode)
                        {

                            var readTask = getResult.Content.ReadAsAsync<Student[]>();
                            readTask.Wait();

                            var students = readTask.Result;
                            Console.WriteLine("*****************List of Students*****************");
                            Console.WriteLine("Student ID".PadRight(20) + "Student Name".PadRight(20) + "Standard ID");
                            foreach (var student in students)
                            {
                                Console.WriteLine(student.Id.ToString().PadRight(20)
                                      + student.StudentName.PadRight(20)
                                      + student.StandardId);
                            }
                            Console.WriteLine("***************************************************");
                        }
                        #endregion
                        break;
                    case "2":
                        //HTTP POST
                        #region Add Student
                        Console.WriteLine("Please enter Student Name");
                        string strStudentName = Console.ReadLine();
                        Console.WriteLine("Please enter Standard ID");
                        int intStandardId = int.Parse(Console.ReadLine());
                        var newStudent = new Student() { StudentName = strStudentName, StandardId = intStandardId };

                        client.BaseAddress = new Uri("http://localhost:56372/api/student");
                        var postTask = client.PostAsJsonAsync<Student>("student", newStudent);
                        postTask.Wait();

                        var postResult = postTask.Result;
                        if (postResult.IsSuccessStatusCode)
                        {

                            Console.WriteLine("Student added successfully.");
                        }
                        else
                        {
                            Console.WriteLine(postResult.StatusCode);
                        }
                        #endregion
                        break;
                    case "3":
                        #region Update Student
                        Student objStudent1 = SearchStudent();
                        if (objStudent1 != null)
                        {
                            Console.WriteLine("Current Student Name : " + objStudent1.StudentName);
                            Console.WriteLine("Enter new Student name");
                            objStudent1.StudentName = Console.ReadLine();
                            Console.WriteLine("Current Standard ID : " + objStudent1.StandardId);
                            Console.WriteLine("Enter new Standard ID");
                            objStudent1.StandardId = int.Parse(Console.ReadLine());

                            client.BaseAddress = new Uri("http://localhost:56372/api/student");

                            //HTTP POST
                            var putTask = client.PutAsJsonAsync<Student>("student", objStudent1);
                            putTask.Wait();


                            var result = putTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Student updated successfully.");
                            }
                        }
                        else
                            Console.WriteLine("No Student found");
                        #endregion
                        break;
                    case "4":
                        #region Delete Student
                        Student studentToDelete = SearchStudent();
                        if (studentToDelete != null)
                        {
                            Console.WriteLine("Student ID : " + studentToDelete.Id
                                            + " And Student Name : " + studentToDelete.StudentName);
                            Console.WriteLine("Are you sure to delete? Press Y to continue else press any other key.");
                            string strDelChoice = Console.ReadLine();
                            if (strDelChoice.ToUpper() == "Y")
                            {
                                //HTTP DELETE
                                var deleteTask = client.DeleteAsync("student/" + studentToDelete.Id.ToString());
                                deleteTask.Wait();

                                var result = deleteTask.Result;
                                if (result.IsSuccessStatusCode)
                                {
                                    Console.WriteLine("Student removed successfully.");
                                }

                            }
                        }
                        else
                            Console.WriteLine("No Student found");
                        #endregion
                        break;
                    case "5":
                        #region Search Student
                        Student objStudent = SearchStudent();
                        if (objStudent != null)
                            Console.WriteLine("Student ID: " + objStudent.Id.ToString().PadRight(20)
                            + " Student Name: " + objStudent.StudentName.PadRight(20)
                            + "Standard ID:" + objStudent.StandardId);
                        else
                            Console.WriteLine("No Student found");
                        #endregion
                        break;
                    case "6":
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                }
            }

            Console.WriteLine("Do you want to continue? Press Y to continue.");
            string strChoice = Console.ReadLine();
            if (strChoice.ToUpper() == "Y")
                goto AskOption;
            else
                Environment.Exit(0);
        }

        private static Student SearchStudent()
        {
            Student objStudent = null;
            Console.WriteLine("Press 1 to search by Student ID and Press 2 to search Student Name");
            TryAgain:
            string strOption = Console.ReadLine();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56372/api/");
                switch (strOption)
                {
                    case "1":
                        Console.WriteLine("Enter Student ID:");
                        try
                        {
                            int intStdID = int.Parse(Console.ReadLine());
                            //HTTP GET
                            var responseTaskID = client.GetAsync("student?id=" + intStdID.ToString());
                            responseTaskID.Wait();

                            var resultByID = responseTaskID.Result;
                            if (resultByID.IsSuccessStatusCode)
                            {
                                var readTask = resultByID.Content.ReadAsAsync<Student>();
                                readTask.Wait();

                                objStudent = readTask.Result;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid Data. Unable to parse to int");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Enter Student Name:");
                        string strStdName = Console.ReadLine();
                        //HTTP GET
                        var responseTaskName = client.GetAsync("student?name=" + strStdName.ToString());
                        responseTaskName.Wait();

                        var resultByName = responseTaskName.Result;
                        if (resultByName.IsSuccessStatusCode)
                        {
                            var readTask = resultByName.Content.ReadAsAsync<Student>();
                            readTask.Wait();

                            objStudent = readTask.Result;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Option. Please try again");
                        goto TryAgain;
                }
            }
            return objStudent;
        }
    }
}
