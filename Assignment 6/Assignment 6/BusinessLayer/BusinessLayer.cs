using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IStandardRepository _standardRepository;
        private readonly IStudentRepository _studentRepository;
        public BusinessLayer()
        {
            _standardRepository = new StandardRepository();
            _studentRepository = new StudentRepository();
        }

        public IList<Standard> getAllStandards()
        {
            return _standardRepository.GetAll().ToList();
        }

        public Standard GetStandardByID(int id)
        {
            return _standardRepository.GetById(id);
        }

        public Standard GetStandardByName(string name)
        {
            return _standardRepository.GetSingle(
                s => s.StandardName.Equals(name));
        }

        public Standard GetStandardByName1(string name)
        {
            IList<Standard> allStandards = getAllStandards();
            return allStandards.Where(n => n.StandardName.Equals(name)).FirstOrDefault();
        }

        public Standard GetStandardByIDWithStudents(int id)
        {
            return _standardRepository.GetSingle(
                s => s.StandardId == id,
                s => s.Students);
        }


        public Student GetStudentByName(string name)
        {
            return _studentRepository.GetSingle(
                s => s.StudentName.Equals(name));
        }

        public Student GetStudentByName1(string name)
        {
            IList<Student> allStudents = getAllStudents();
            return allStudents.Where(n => n.StudentName.Equals(name)).FirstOrDefault();
        }


        public void addStandard(Standard objStandard)
        {
            _standardRepository.Insert(objStandard);
        }

        public void updateStandard(Standard objStandard)
        {
            _standardRepository.Update(objStandard);
        }

        public void removeStandard(Standard objStandard)
        {
            _standardRepository.Delete(objStandard);
        }

        public IList<Student> getAllStudents()
        {
            return _studentRepository.GetAll().ToList();
        }

        public Student GetStudentByID(int id)
        {
            return _studentRepository.GetById(id);
        }

        public void addStudent(Student objStudent)
        {
            _studentRepository.Insert(objStudent);
        }

        public void UpdateStudent(Student objStudent)
        {
            _studentRepository.Update(objStudent);
        }

        public void RemoveStudent(Student objStudent)
        {
            _studentRepository.Delete(objStudent);
        }
    }
}
