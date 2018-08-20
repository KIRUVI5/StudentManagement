using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManage.Models;

namespace StudentManage.Controllers
{
    [Route("api/Sudent")]
    [ApiController]
    public class SudentController : ControllerBase
    {
        private readonly StudentContext _context;

        // add default student
        public SudentController(StudentContext context)
        {
            _context = context;

            if(_context.Students.Count() == 0 )
            {
                _context.Students.Add(new Student {Name = "VKU", Degree = "CST" , Department = "SCT" });
                _context.SaveChanges();
            }
        }

        //get all students
        [HttpGet]
        public ActionResult<List<Student>> GetAllStudents() => _context.Students.ToList();

        //get Students  by Id
        [HttpGet("{stuid}" , Name = "GetStudent")]
        public ActionResult<Student> GetStudentByID(int stuid)
        {
            var stu = _context.Students.Find(stuid);
            if(stu == null)
            {
                return NotFound();
            }
            return stu;
        }

        //Add New Student
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

            return CreatedAtRoute("GetStudent", new { stuid = student.StudentID }, student);
        }

        //update Student
        [HttpPut ("{stuid}")]
        public IActionResult UpdateStudent(int stuid, Student student)
        {
            var upstu = _context.Students.Find(stuid);

            if(upstu == null)
            {
                return NotFound();
            }

            upstu.Name = student.Name;
            upstu.Degree = student.Degree;
            upstu.Department = student.Department;

            _context.Students.Update(upstu);
            _context.SaveChanges();
            return NoContent();
        }

        // Delete Students
        [HttpDelete ("{stuid}")]
        public IActionResult DeleteStudent(int stuid)
        {
            var delstu = _context.Students.Find(stuid);

            if(delstu == null)
            {
                return NotFound();
            }

            _context.Students.Remove(delstu);
            _context.SaveChanges();
            return NoContent();
        }
    }

}