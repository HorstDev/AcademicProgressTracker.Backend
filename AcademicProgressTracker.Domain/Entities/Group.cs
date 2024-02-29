namespace AcademicProgressTracker.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;    // ДИПР
        public byte[] CurriculumExcelDocument { get; set; }

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
