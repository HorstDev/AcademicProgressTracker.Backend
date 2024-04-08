namespace AcademicProgressTracker.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;    // ДИПР
        public DateTime DateTimeOfUpdateDependenciesFromServer { get; set; }
        public byte[] CurriculumExcelDocument { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        public Group(string name, byte[] curriculumExcelDocument)
        {
            Name = name;
            CurriculumExcelDocument = curriculumExcelDocument;
        }

        // Получение текущего семестра (считается с помощью года создания)
        public int GetSemester()
        {
            return 1;
        }
    }
}
