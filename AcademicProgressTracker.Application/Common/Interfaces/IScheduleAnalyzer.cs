using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Interfaces
{
    public interface IScheduleAnalyzer
    {
        public List<Lesson> GetLessonsOfSubjectFromLessonsDTO(DateOnly semesterStart, List<LessonDTO> lessonsDTO, string subjectName,
                                                                int labLessonCount, int lectureLessonCount, int practiceLessonCount);
    }
}
