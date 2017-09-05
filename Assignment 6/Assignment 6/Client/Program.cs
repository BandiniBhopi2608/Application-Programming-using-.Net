using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DataAccessLayer;

namespace Client
{
    class Program
    {
        private static IBusinessLayer objBusiness = new BusinessLayer.BusinessLayer();
        static void Main(string[] args)
        {
            AskOption:
            {
                Console.WriteLine("Menu"
                                   + "\r\n----------------------------------------"
                                   + "\r\nWhat would you like to do?"
                                   + "\r\n 1) Add new Standard"
                                   + "\r\n 2) Update Standard"
                                   + "\r\n 3) Delete Standard"
                                   + "\r\n 4) Search Standard"
                                   + "\r\n 5) Display Students related to Standard"
                                   + "\r\n 6) Display all Standards"
                                   + "\r\n 7) Add new Student"
                                   + "\r\n 8) Update Student"
                                   + "\r\n 9) Delete Student"
                                   + "\r\n 10) Search Student"
                                   + "\r\n 11) Display all Students"
                                   + "\r\n 12) Exit the program");

                string strOption = Console.ReadLine();
                switch (strOption)
                {
                    case "1":
                        AddStandard();
                        break;
                    case "2":
                        Standard objStandard1 = null;
                        objStandard1 = SearchStandard(2);
                        if (objStandard1 != null)
                        {
                            UpdateStandard(objStandard1);
                        }
                        else
                            Console.WriteLine("No Standard found");
                        break;
                    case "3":
                        DeleteStandard();
                        break;
                    case "4":
                        Standard objStandard2 = null;
                        objStandard2 = SearchStandard(1);
                        if (objStandard2 != null)
                            Console.WriteLine("Standard Name: " + objStandard2.StandardName.PadRight(20)
                            + " Standard Description: " + objStandard2.Description);
                        else
                            Console.WriteLine("No Standard found");
                        break;
                    case "5":
                        SearchStandardStudents();
                        break;
                    case "6":
                        DisplayAllStandards();
                        break;
                    case "7":
                        AddStudent();
                        break;
                    case "8":
                        Student objStudent1 = SearchStudent(2);
                        if (objStudent1 != null)
                            UpdateStudent(objStudent1);
                        else
                            Console.WriteLine("No Student found");
                        break;
                    case "9":
                        DeleteStudent();
                        break;
                    case "10":
                        Student objStudent = SearchStudent(1);
                        if (objStudent != null)
                            Console.WriteLine("Student ID: " + objStudent.StudentID.ToString().PadRight(20)
                            + " Student Name: " + objStudent.StudentName);
                        else
                            Console.WriteLine("No Student found");
                        break;
                    case "11":
                        DisplayAllStudents();
                        break;
                    case "12":
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

        private static void AddStandard()
        {
            Console.WriteLine("Please enter Standard Name");
            string strStandardName = Console.ReadLine();
            Console.WriteLine("Please enter Standard Description");
            string strStandardDesc = Console.ReadLine();
            Standard objStandard = new Standard();
            objStandard.StandardName = strStandardName;
            objStandard.Description = strStandardDesc;
            objBusiness.addStandard(objStandard);
            Console.WriteLine("Standard added successfully.");
        }

        private static void AddStudent()
        {
            Console.WriteLine("Please enter Student Name");
            string strStudentName = Console.ReadLine();
            Console.WriteLine("Please enter Standard ID");
            int intStandardId = int.Parse(Console.ReadLine());
            Student objStudent = new Student();
            objStudent.StudentName = strStudentName;
            objStudent.StandardId = intStandardId;
            objBusiness.addStudent(objStudent);
            Console.WriteLine("Student added successfully.");
        }

        private static void DisplayAllStandards()
        {
            IList<Standard> standardList = objBusiness.getAllStandards();
            if (standardList != null && standardList.Count > 0)
            {
                Console.WriteLine("*****************List of standards*****************");
                Console.WriteLine("Standard ID".PadRight(20) + "Standard Name".PadRight(20) + "Description");
                foreach (Standard std in standardList)
                {
                    Console.WriteLine(std.StandardId.ToString().PadRight(20) + std.StandardName.PadRight(20)
                    + (String.IsNullOrEmpty(std.Description) ? "---" : std.Description));
                }
                Console.WriteLine("***************************************************");
            }
            else
            {
                Console.WriteLine("No standard found.");
                return;
            }
        }

        private static void DisplayAllStudents()
        {
            IList<Student> studentList = objBusiness.getAllStudents();
            if (studentList != null && studentList.Count > 0)
            {
                Console.WriteLine("*****************List of Students*****************");
                Console.WriteLine("Student ID".PadRight(20) + "Student Name".PadRight(20) + "Standard ID");
                foreach (Student student in studentList)
                {
                    Console.WriteLine(student.StudentID.ToString().PadRight(20) 
                                      + student.StudentName.PadRight(20)
                                      + student.StandardId);
                }
                Console.WriteLine("***************************************************");
            }
            else
            {
                Console.WriteLine("No Student found.");
                return;
            }
        }

        /// <summary>
        /// Search Standard by ID or name
        /// </summary>
        /// <param name="iOption">1 : it will call GetSingle method of repository, 2: iterate through all list and return Standard whose name matches with entered name</param>
        /// <returns></returns>
        private static Standard SearchStandard(int iOption)
        {
            Standard objStandard = null;
            Console.WriteLine("Press 1 to search by Standard ID and Press 2 to search Standard Name");
            TryAgain:
            string strOption = Console.ReadLine();
            switch (strOption)
            {
                case "1":
                    Console.WriteLine("Enter Standard ID:");
                    try
                    {
                        int intStdID = int.Parse(Console.ReadLine());
                        objStandard = objBusiness.GetStandardByID(intStdID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid Data. Unable to parse to int");
                    }
                    break;
                case "2":
                    Console.WriteLine("Enter Standard Name:");
                    string strStdName = Console.ReadLine();
                    objStandard = (iOption == 1 ? objBusiness.GetStandardByName(strStdName) : objBusiness.GetStandardByName1(strStdName));
                    break;
                default:
                    Console.WriteLine("Invalid Option. Please try again");
                    goto TryAgain;
            }
            return objStandard;
        }

        private static Student SearchStudent(int iOption)
        {
            Student objStudent = null;
            Console.WriteLine("Press 1 to search by Student ID and Press 2 to search Student Name");
            TryAgain:
            string strOption = Console.ReadLine();
            switch (strOption)
            {
                case "1":
                    Console.WriteLine("Enter Student ID:");
                    try
                    {
                        int intStdID = int.Parse(Console.ReadLine());
                        objStudent = objBusiness.GetStudentByID(intStdID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid Data. Unable to parse to int");
                    }
                    break;
                case "2":
                    Console.WriteLine("Enter Student Name:");
                    string strStdName = Console.ReadLine();
                    objStudent = (iOption == 1 ? objBusiness.GetStudentByName(strStdName) : objBusiness.GetStudentByName1(strStdName));
                    break;
                default:
                    Console.WriteLine("Invalid Option. Please try again");
                    goto TryAgain;
            }
            return objStudent;
        }

        private static void UpdateStandard(Standard objStandard)
        {
            Console.WriteLine("Current Standard Name : " + objStandard.StandardName);
            Console.WriteLine("Enter new Standard name");
            objStandard.StandardName = Console.ReadLine();
            Console.WriteLine("Current Standard Description : " + objStandard.Description);
            Console.WriteLine("Enter new Standard Description");
            objStandard.Description = Console.ReadLine();
            objBusiness.updateStandard(objStandard);
            Console.WriteLine("Standard updated successfully.");
        }

        private static void UpdateStudent(Student objStudent)
        {
            Console.WriteLine("Current Student Name : " + objStudent.StudentName);
            Console.WriteLine("Enter new Student name");
            objStudent.StudentName = Console.ReadLine();
            Console.WriteLine("Current Standard ID : " + objStudent.StandardId);
            Console.WriteLine("Enter new Standard ID");
            objStudent.StandardId = int.Parse(Console.ReadLine());
            objBusiness.UpdateStudent(objStudent);
            Console.WriteLine("Student updated successfully.");
        }

        private static void DeleteStandard()
        {
            Standard objStandard = null;
            objStandard = SearchStandard(2);
            if (objStandard != null)
            {
                Console.WriteLine("Standard Name : " + objStandard.StandardName
                + " And Standard Description : " + objStandard.Description);
                Console.WriteLine("Are you sure to delete? Press Y to continue else press any other key.");
                string strChoice = Console.ReadLine();
                if (strChoice.ToUpper() == "Y")
                {
                    objBusiness.removeStandard(objStandard);
                    Console.WriteLine("Standard removed successfully.");
                }
            }
            else
                Console.WriteLine("No Standard found.");
        }

        private static void DeleteStudent()
        {
            Student objStudent = SearchStudent(2);
            if (objStudent != null)
            {
                Console.WriteLine("Student ID : " + objStudent.StudentID
                + " And Student Name : " + objStudent.StudentName);
                Console.WriteLine("Are you sure to delete? Press Y to continue else press any other key.");
                string strChoice = Console.ReadLine();
                if (strChoice.ToUpper() == "Y")
                {
                    objBusiness.RemoveStudent(objStudent);
                    Console.WriteLine("Student removed successfully.");
                }
            }
            else
                Console.WriteLine("No Student found.");
        }

        private static void SearchStandardStudents()
        {
            Standard objStandard = null;
            Console.WriteLine("Enter Standard ID:");
            try
            {
                int intStdID = int.Parse(Console.ReadLine());
                objStandard = objBusiness.GetStandardByIDWithStudents(intStdID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Data. Unable to parse to int");
            }
            if (objStandard == null)
            {
                Console.WriteLine("Standard not found!");
                return;
            }
            else if (objStandard.Students == null || objStandard.Students.Count == 0)
            {
                Console.WriteLine("This Standard has no Students!");
                return;
            }
            Console.WriteLine("\nContaining Students: {0}", objStandard.Students.Count);
            foreach (Student student in objStandard.Students)
                Console.WriteLine("- " + student.StudentName);

        }
    }
}
