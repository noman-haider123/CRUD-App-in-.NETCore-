using Microsoft.EntityFrameworkCore;
using Noman.Models;

namespace Noman.Respository
{
    public class StudentRepository : IStudent
    {
        private readonly StudentDbContext _studentcontext;
        public StudentRepository(StudentDbContext studentcontext)
        {
            _studentcontext = studentcontext;
        }

        public async Task<Student> Delete(int id)
        {
           var student = await _studentcontext.Students.FindAsync(id);
           if(student == null)
           {
                return null;
           }
           _studentcontext.Students.Remove(student);
           await _studentcontext.SaveChangesAsync();
           return student;
        }

        public async Task<Student> Get(int id)
        {
           return await _studentcontext.Students.FindAsync(id);
        }

        public async Task<List<Student>> Index()
        {
           return await _studentcontext.Students.ToListAsync();
        }

        public async Task<Student> Store(Student student)
        {
            await _studentcontext.Students.AddAsync(student);
            await _studentcontext.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Update(int id, Student student)
        {
            var existingstudent = await _studentcontext.Students.FindAsync(id);
            if (existingstudent != null)
            {
                existingstudent.Name = student.Name;
                existingstudent.Description = student.Description;
                existingstudent.Age = student.Age;
                existingstudent.Image = student.Image;
                _studentcontext.Students.Update(existingstudent);
                await _studentcontext.SaveChangesAsync();
                return existingstudent;
            }
            return null;
        }

    }
}
