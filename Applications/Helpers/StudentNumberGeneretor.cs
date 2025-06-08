using Domain.Entities;

namespace Applications.Helpers
{
    public static class StudentNumberGeneretor
    {
        public static string GenerateStudentNumber(this Student student) 
            => Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }
}
