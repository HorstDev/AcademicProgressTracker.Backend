namespace AcademicProgressTracker.Domain.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;    // ДИПР
        public DateTime DateTimeOfUpdateDependenciesFromServer { get; set; }
        public DateTime DateTimeOfLastIncreaseCourse { get; set; }
        public byte[] CurriculumExcelDocument { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        public Group(string name, byte[] curriculumExcelDocument)
        {
            Name = name;
            CurriculumExcelDocument = curriculumExcelDocument;
        }

        // Тип обучения - магистратура
        public bool TypeOfStudyIsMasterDegree()
        {
            var indexOfStudyType = Name.IndexOf("_") - 1;
            return Name[indexOfStudyType] == 'М';
        }

        // Тип обучения - бакалавриат
        public bool TypeOfStudyIsBachelorDegree()
        {
            var indexOfStudyType = Name.IndexOf("_") - 1;
            return Name[indexOfStudyType] == 'Б';
        }

        // Тип обучения - специалитет
        public bool TypeOfStudyIsSpecialtyDegree()
        {
            var indexOfStudyType = Name.IndexOf("_") - 1;
            return Name[indexOfStudyType] == 'С';
        }

        public int Course()
        {
            var indexOfCourse = Name.IndexOf("_") + 1;
            return int.Parse(Name[indexOfCourse].ToString());
        }

        // Увеличение курса
        public void IncreaseCourse()
        {
            var indexOfCourse = Name.IndexOf("_") + 1;
            int increasedCourse = Course() + 1;
            char[] charArray =  Name.ToCharArray();
            charArray[indexOfCourse] = (char)('0' + increasedCourse);
            Name = new string(charArray);
            DateTimeOfLastIncreaseCourse = DateTime.Now;
        }

        // Возвращает true, если курс превысил допустимое значение для определенного типа обучения
        public bool StudyIsOver()
        {
            switch(Course())
            {
                // 3-го курса магистратуры не существует, поэтому обучение окончено, и так для всех остальных
                case 3:
                    return TypeOfStudyIsMasterDegree();
                case 5:
                    return TypeOfStudyIsBachelorDegree();
                case 6:
                    return TypeOfStudyIsSpecialtyDegree();
                default:
                    return false;
            }
        }
    }
}
