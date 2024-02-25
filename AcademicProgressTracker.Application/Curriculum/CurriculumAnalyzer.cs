using AcademicProgressTracker.Application.Common.Enums.Curriculum;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;

namespace AcademicProgressTracker.Application.Curriculum
{
    public class CurriculumAnalyzer
    {
        private byte[]? _curriculumExcelDocument { get; set; }

        public CurriculumAnalyzer(byte[] curriculumExcelDocument) 
        {
            _curriculumExcelDocument = curriculumExcelDocument;
        }

        /// <summary>
        /// Возвращает количество лабораторных занятий
        /// </summary>
        /// <param name="subject">Предмет</param>
        /// <param name="semester">Семестр</param>
        /// <returns>количество лабораторных занятий</returns>
        /// <exception cref="ApplicationException"></exception>
        public int GetNumberOfLaboratoryLesson(string subject, int semester)
        {
            if (_curriculumExcelDocument != null)
            {
                using (var package = new ExcelPackage(new MemoryStream(_curriculumExcelDocument)))
                {
                    var worksheet = package.Workbook.Worksheets[(int)Worksheet.Plan];
                    int columnCount = worksheet.Dimension.End.Column;
                    int rowCount = worksheet.Dimension.End.Row;
                    int rowWithLabHours = 0, columnWithLabHours = 0;    // строка и столбец, которые соответствуют лабораторным занятиям
                    int countColumnsWithLab = 0;                        // счетчик столбцов, в которых есть "Лаб" (столбцы с часами ЛР)

                    // Находим колонку с количеством часов лабораторных, которые соответствуют семестру (semester)
                    // P.S. В enum.Columns нет константных столбцов, так как они могут отличаться в зависимости от учебного плана
                    for (int column = 1; column <= columnCount; column++)
                    {
                        if (worksheet.Cells[3, column].Text == "Лаб")
                            countColumnsWithLab++;

                        if (countColumnsWithLab == semester)
                        {
                            columnWithLabHours = column;
                            break;
                        }
                    }

                    // Находим строку, которая соответствует нужным часам лабораторных работ
                    for (int row = 1; row <= rowCount; row++)
                    {
                        if (worksheet.Cells[row, (int)Column.NamesOfSubjects].Text == subject)
                        {
                            rowWithLabHours = row;
                            break;
                        }
                    }

                    // Возвращаем количество лабораторных занятий (2 часа == 1 занятие)
                    if (worksheet.Cells[rowWithLabHours, columnWithLabHours].Text.IsNullOrEmpty())
                        return 0;
                    return int.Parse(worksheet.Cells[rowWithLabHours, columnWithLabHours].Text) / 2;
                }
            }
            throw new ArgumentNullException("Документ с учебным планом не был загружен");
        }

        /// <summary>
        /// Возвращает один общий семестр для всех дисциплин
        /// </summary>
        /// <param name="subjects">список дисциплин</param>
        /// <returns>один общий семестр для всех дисциплин</returns>
        public int GetCommonSemesterForSubjects(IEnumerable<string> subjects)
        {
            List<List<int>> semestersList = GetSemestersOfSubjects(subjects);

            // Возвращаем общий семестр для всех дисциплин
            return semestersList
                .Aggregate((currentList, nextList) => currentList.Intersect(nextList).ToList())
                .Single();
        }

        /// <summary>
        /// Возвращает список семестров, в которые проводится дисциплина
        /// </summary>
        /// <param name="subject">дисциплина</param>
        /// <returns>список семестров, в которые проводится дисциплина</returns>
        public List<List<int>> GetSemestersOfSubjects(IEnumerable<string> subjects)
        {
            if (_curriculumExcelDocument != null)
            {
                var subjectsSemesters = new List<List<int>>();
                using (var package = new ExcelPackage(new MemoryStream(_curriculumExcelDocument)))
                {
                    var worksheet = package.Workbook.Worksheets[(int)Worksheet.Plan];
                    int rowCount = worksheet.Dimension.End.Row;

                    // Пробегаемся по всем строкам с дисциплинами
                    for (int row = 1; row <= rowCount; row++)
                    {
                        // Если рассматриваемый предмет в учебном планге содержится в subjects, добавляем его семестры в массив семестров
                        if (subjects.Contains(worksheet.Cells[row, (int)Column.NamesOfSubjects].Text))
                        {
                            char[] semestersOfExam = worksheet.Cells[row, (int)Column.SemestersOfExam].Text.ToCharArray();
                            char[] semesterOfCredit = worksheet.Cells[row, (int)Column.SemestersOfCredit].Text.ToCharArray();
                            char[] semesterOfCreditWithAssessment = worksheet.Cells[row, (int)Column.SemestersOfCreditWithAssessment].Text.ToCharArray();
                            char[] semesterOfCourseProject = worksheet.Cells[row, (int)Column.SemestersOfCourseProject].Text.ToCharArray();
                            var semestersOfSubject = semestersOfExam.Concat(semesterOfCredit).Concat(semesterOfCreditWithAssessment).Concat(semesterOfCourseProject)
                                .Select(c => int.Parse(c.ToString()))
                                .Distinct()
                                .ToList();
                            subjectsSemesters.Add(semestersOfSubject);
                        }
                    }
                    return subjectsSemesters;
                }
            }

            throw new ArgumentNullException("Документ с учебным планом не был загружен");
        }
    }
}
