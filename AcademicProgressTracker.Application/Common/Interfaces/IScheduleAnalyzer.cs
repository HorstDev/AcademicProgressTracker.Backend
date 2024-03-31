using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Interfaces
{
    public interface IScheduleAnalyzer
    {
        /// <summary>
        /// Возвращает занятия для дисциплины
        /// </summary>
        /// <param name="semesterStart">Дата начала семестра</param>
        /// <param name="lessonsDTO">Занятия DTO</param>
        /// <param name="subjectName">Название предмета</param>
        /// <param name="labLessonCount">Количество лабораторных занятий</param>
        /// <param name="lectureLessonCount">Количество лекционных занятий</param>
        /// <param name="practiceLessonCount">Количество практических занятий</param>
        /// <returns>Возвращает занятия для дисциплины</returns>
        public List<Lesson> GetLessonsOfSubjectFromLessonsDTO(DateOnly semesterStart, List<LessonDTO> lessonsDTO, string subjectName,
                                                                int labLessonCount, int lectureLessonCount, int practiceLessonCount);

        /// <summary>
        /// Определяет время текущего занятия
        /// </summary>
        /// <param name="start">Время начала занятия</param>
        /// <param name="end">Время конца занятия</param>
        void CurrentLessonDateTime(out DateTime start, out DateTime end);
    }
}
