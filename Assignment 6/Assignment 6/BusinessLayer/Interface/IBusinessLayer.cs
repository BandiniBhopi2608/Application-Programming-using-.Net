using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public interface IBusinessLayer
    {
        IList<Standard> getAllStandards();
        Standard GetStandardByID(int id);
        void addStandard(Standard objStandard);
        void updateStandard(Standard objStandard);
        void removeStandard(Standard objStandard);
        Standard GetStandardByName(string name);
        Standard GetStandardByName1(string name);
        IList<Student> getAllStudents();
        Student GetStudentByID(int id);
        void addStudent(Student objStudent);
        void UpdateStudent(Student objStudent);
        void RemoveStudent(Student objStudent);
        Student GetStudentByName(string name);
        Student GetStudentByName1(string name);
        Standard GetStandardByIDWithStudents(int id);
    }
}
