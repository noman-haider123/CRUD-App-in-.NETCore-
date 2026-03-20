using Noman.Models;

namespace Noman.Respository
{
    public interface IStudent
    {
        public Task<List<Student>> Index();
        public Task<Student> Store(Student student);
        public Task<Student> Get(int id);
        public Task<Student> Update(int id,Student student);
        public Task<Student> Delete(int id);
    }
}
