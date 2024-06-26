﻿using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Schedule
{
    public class ScheduleAnalyzer : IScheduleAnalyzer
    {
        // Номер пары и соответствующее время начала пары
        private List<TimeOnly> StartTimes { get; set; } = new List<TimeOnly>
        {
            { new TimeOnly(8, 30) },
            { new TimeOnly(10, 15) },
            { new TimeOnly(12, 0) },
            { new TimeOnly(14, 0) },
            { new TimeOnly(15, 45) },
            { new TimeOnly(17, 30) },
            { new TimeOnly(19, 15) },
        };

        // Номер пары и соответствующее время начала пары
        private List<TimeOnly> EndTimes { get; set; } = new List<TimeOnly>
        {
            { new TimeOnly(10, 00) },
            { new TimeOnly(11, 45) },
            { new TimeOnly(13, 30) },
            { new TimeOnly(15, 30) },
            { new TimeOnly(17, 15) },
            { new TimeOnly(19, 00) },
            { new TimeOnly(20, 45) },
        };

        /// <summary>
        /// Возвращает номер недели для определенной даты
        /// </summary>
        /// <param name="currentWeek">Текущая неделя</param>
        /// <param name="date">Дата</param>
        /// <returns>номер недели для определенной даты</returns>
        private int GetWeekNumberForDate(DateOnly date)
        {
            // Получаем дату с первой неделей (уменьшаем дату до 1 сентября)
            var dateWithFirstWeek = new DateOnly(date.Year, date.Month, date.Day);
            // Уменьшаем месяцы до сентября
            while (dateWithFirstWeek.Month != 9)
                dateWithFirstWeek = dateWithFirstWeek.AddMonths(-1);
            // Уменьшаем дни до 1 сентября
            while (dateWithFirstWeek.Day != 1)
                dateWithFirstWeek = dateWithFirstWeek.AddDays(-1);

            // Приводим к тому же дню недели, что и искомая дата, чтобы удобно было считать
            while(dateWithFirstWeek.DayOfWeek < date.DayOfWeek)
                dateWithFirstWeek = dateWithFirstWeek.AddDays(1);
            while (dateWithFirstWeek.DayOfWeek > date.DayOfWeek)
                dateWithFirstWeek = dateWithFirstWeek.AddDays(-1);

            // Считаем разницу в неделях между датами
            int differenceInWeeks = 0;
            while(dateWithFirstWeek != date)
            {
                dateWithFirstWeek = dateWithFirstWeek.AddDays(7);
                differenceInWeeks++;
            }

            // Если разница в неделях между датами четная, то это первая неделя, иначе вторая
            if (differenceInWeeks % 2 == 0)
                return 1;
            else
                return 2;
        }

        public List<Lesson> GetLessonsOfSubjectFromLessonsDTO(DateOnly semesterStart, List<LessonDTO> lessonsDTO, string subjectName,
                                                                int labLessonCount, int lectureLessonCount, int practiceLessonCount)
        {
            var lessonsResult = new List<Lesson>();
            int weekNumberSemesterStart = GetWeekNumberForDate(semesterStart);

            // Извлекаем занятия предмета subjectName
            var lessonsOfSubject = lessonsDTO.Where(lesson => lesson.Entries.Any(entry => entry.Discipline == subjectName));

            var labLessonsOfDays = new Dictionary<int, List<LessonDTO>>();
            var lectureLessonsOfDays = new Dictionary<int, List<LessonDTO>>();
            var practiceLessonsOfDays = new Dictionary<int, List<LessonDTO>>();

            // Пробегаемся по дням (всего 12 дней в двух неделях без воскресенья)
            for (int dayId = 0; dayId < 12; dayId++)
            {
                var labsOfDay = lessonsOfSubject.Where(lesson => lesson.DayId == dayId && lesson.Entries.Any(entry => entry.Type == "laboratory")).ToList();
                var lecturesOfDay = lessonsOfSubject.Where(lesson => lesson.DayId == dayId && lesson.Entries.Any(entry => entry.Type == "lecture")).ToList();
                var practicesOfDay = lessonsOfSubject.Where(lesson => lesson.DayId == dayId && lesson.Entries.Any(entry => entry.Type == "practice")).ToList();

                labLessonsOfDays.Add(dayId, labsOfDay);
                lectureLessonsOfDays.Add(dayId , lecturesOfDay);
                practiceLessonsOfDays.Add(dayId, practicesOfDay);
            }

            var labLessons = GetLabLessons(semesterStart, weekNumberSemesterStart, labLessonCount, labLessonsOfDays);
            var lectureLessons = GetLectureLessons(semesterStart, weekNumberSemesterStart, lectureLessonCount, lectureLessonsOfDays);
            var practiceLessons = GetPracticeLessons(semesterStart, weekNumberSemesterStart, practiceLessonCount, practiceLessonsOfDays);

            lessonsResult.AddRange(labLessons);
            lessonsResult.AddRange(lectureLessons);
            lessonsResult.AddRange(practiceLessons);

            return lessonsResult;
        }

        private List<LabLesson> GetLabLessons(DateOnly semesterStart, int weekNumberSemesterStart, int labLessonCount, Dictionary<int, List<LessonDTO>> labLessonsOfDays)
        {
            var labLessonsResult = new List<LabLesson>();

            var currentDate = new DateOnly(semesterStart.Year, semesterStart.Month, semesterStart.Day);
            int currentWeekNumber = weekNumberSemesterStart;
            int currentLabLessonNumber = 0;
            int countDays = 0; // показывает, сколько дней мы прошли в поиске нужных занятий

            while (labLessonCount > 0)
            {
                // Если воскресенье, изменяем текущую неделю на противоположную
                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    currentDate = currentDate.AddDays(1);
                    currentWeekNumber = currentWeekNumber == 1 ? 2 : 1;
                }
                // Так как DayOfWeek (индекс) начинается с воскресенья, а в расписании АГТУ с понедельника, то убавляем на 1
                int dayId = (currentWeekNumber == 1) ? (int)currentDate.DayOfWeek - 1 : (int)currentDate.DayOfWeek + 5;
                foreach (var labLesson in labLessonsOfDays[dayId])
                {
                    // Дополнительная проверка на случай, если в одном дне 2 лабораторных занятия
                    if (labLessonCount > 0)
                    {
                        labLessonsResult.Add(new LabLesson
                        {
                            Number = currentLabLessonNumber++,
                            Start = new DateTime(currentDate, StartTimes[labLesson.LessonOrderId]),
                            End = new DateTime(currentDate, StartTimes[labLesson.LessonOrderId].AddMinutes(90)),
                        });
                        labLessonCount--;
                    }
                }
                currentDate = currentDate.AddDays(1);

                countDays++;
                // Если прошло 365 дней и мы все еще ищем нужные занятия, то был загружен неправильный документ с учебным планом
                // или же было загружено несоответствующее расписание для группы
                if (countDays >= 365)
                    throw new InvalidDataException("Ошибка. Загружаемое расписание не соответствует учебному плану!");
            }

            return labLessonsResult;
        }

        private List<LectureLesson> GetLectureLessons(DateOnly semesterStart, int weekNumberSemesterStart, int lectureLessonCount, Dictionary<int, List<LessonDTO>> lectureLessonsOfDays)
        {
            var lectureLessonResult = new List<LectureLesson>();

            var currentDate = new DateOnly(semesterStart.Year, semesterStart.Month, semesterStart.Day);
            int currentWeekNumber = weekNumberSemesterStart;
            int currentLectureLessonNumber = 0;
            int countDays = 0; // показывает, сколько дней мы прошли в поиске нужных занятий

            while (lectureLessonCount > 0)
            {
                // Если воскресенье, изменяем текущую неделю на противоположную
                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    currentDate = currentDate.AddDays(1);
                    currentWeekNumber = currentWeekNumber == 1 ? 2 : 1;
                }
                // Так как DayOfWeek (индекс) начинается с воскресенья, а в расписании АГТУ с понедельника, то убавляем на 1
                int dayId = (currentWeekNumber == 1) ? (int)currentDate.DayOfWeek - 1 : (int)currentDate.DayOfWeek + 5;
                foreach (var lectureLesson in lectureLessonsOfDays[dayId])
                {
                    // Дополнительная проверка на случай, если в одном дне 2 лабораторных занятия
                    if (lectureLessonCount > 0)
                    {
                        lectureLessonResult.Add(new LectureLesson
                        {
                            Number = currentLectureLessonNumber++,
                            Start = new DateTime(currentDate, StartTimes[lectureLesson.LessonOrderId]),
                            End = new DateTime(currentDate, StartTimes[lectureLesson.LessonOrderId].AddMinutes(90)),
                        });
                        lectureLessonCount--;
                    }
                }
                currentDate = currentDate.AddDays(1);

                countDays++;
                // Если прошло 365 дней и мы все еще ищем нужные занятия, то был загружен неправильный документ с учебным планом
                // или же было загружено несоответствующее расписание для группы
                if (countDays >= 365)
                    throw new InvalidDataException("Ошибка. Загружаемое расписание не соответствует учебному плану!");
            }

            return lectureLessonResult;
        }

        private List<PracticeLesson> GetPracticeLessons(DateOnly semesterStart, int weekNumberSemesterStart, int practiceLessonCount, Dictionary<int, List<LessonDTO>> practiceLessonsOfDays)
        {
            var practiceLessonResult = new List<PracticeLesson>();

            var currentDate = new DateOnly(semesterStart.Year, semesterStart.Month, semesterStart.Day);
            int currentWeekNumber = weekNumberSemesterStart;
            int currentPracticeLessonNumber = 0;
            int countDays = 0; // показывает, сколько дней мы прошли в поиске нужных занятий

            while (practiceLessonCount > 0)
            {
                // Если воскресенье, изменяем текущую неделю на противоположную
                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    currentDate = currentDate.AddDays(1);
                    currentWeekNumber = currentWeekNumber == 1 ? 2 : 1;
                }
                // Так как DayOfWeek (индекс) начинается с воскресенья, а в расписании АГТУ с понедельника, то убавляем на 1
                int dayId = (currentWeekNumber == 1) ? (int)currentDate.DayOfWeek - 1 : (int)currentDate.DayOfWeek + 5;
                foreach (var practiceLesson in practiceLessonsOfDays[dayId])
                {
                    // Дополнительная проверка на случай, если в одном дне 2 лабораторных занятия
                    if (practiceLessonCount > 0)
                    {
                        practiceLessonResult.Add(new PracticeLesson
                        {
                            Number = currentPracticeLessonNumber++,
                            Start = new DateTime(currentDate, StartTimes[practiceLesson.LessonOrderId]),
                            End = new DateTime(currentDate, StartTimes[practiceLesson.LessonOrderId].AddMinutes(90)),
                        });
                        practiceLessonCount--;
                    }
                }
                currentDate = currentDate.AddDays(1);

                countDays++;
                // Если прошло 365 дней и мы все еще ищем нужные занятия, то был загружен неправильный документ с учебным планом
                // или же было загружено несоответствующее расписание для группы
                if (countDays >= 365)
                    throw new InvalidDataException("Ошибка. Загружаемое расписание не соответствует учебному плану!");
            }

            return practiceLessonResult;
        }

        public void CurrentLessonDateTime(out DateTime start, out DateTime end)
        {
            var currentTime = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute);
            start = DateTime.MinValue;
            end = DateTime.MinValue;

            // Определяем начало и конец пары
            for (int i = 0; i < StartTimes.Count; i++)
            {
                if (currentTime >= StartTimes[i] && currentTime <= EndTimes[i])
                {
                    start = new DateTime(new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), StartTimes[i]);
                    end = new DateTime(new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), EndTimes[i]);
                    return;
                }
            }
        }
    }
}
