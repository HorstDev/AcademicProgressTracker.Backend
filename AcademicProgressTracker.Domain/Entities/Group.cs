﻿namespace AcademicProgressTracker.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;    // ДИПР
        public int Course { get; set; }
        public int YearCreated { get; set; }
        public byte[]? CurriculumExcelDocument { get; set; }

        // Получение текущего семестра (считается с помощью года создания)
        public int GetSemester()
        {
            return 1;
        }
    }
}
