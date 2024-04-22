using AcademicProgressTracker.Application.Common.Interfaces.Repositories;
using AcademicProgressTracker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.WebApi.Services
{
    public class IncreaseCoursesService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public IncreaseCoursesService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while(!cancellationToken.IsCancellationRequested)
            {
                // Если август или больше
                if (DateTime.Now.Month >= 8)
                {
                    using(var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AcademicProgressDataContext>();
                        var allGroups = await dbContext.Groups.ToListAsync();
                        if (allGroups.Any())
                        {
                            var groupRepository = scope.ServiceProvider.GetRequiredService<IGroupRepository>();
                            // Получаем дату последнего увеличения курса у 1-ой группы (т.к. у всех групп даты одинаковы)
                            // Если вдруг неодинаковы (по какой-то странной причине), делаем то же самое, чтобы сделать одинаковыми
                            var dateTimeOfIncreaseCourse = allGroups[0].DateTimeOfLastIncreaseCourse;
                            TimeSpan difference = DateTime.Now - dateTimeOfIncreaseCourse;
                            // Разница между увеличениями курсов должна быть больше 250 дней. Разница проверяется на случай, чтобы
                            // не увеличивался курс каждый день. А 250 дней, а не год - на случай, если сервер упадет в день обновления,
                            // то дата увеличения курса не сместится на более позднюю дату
                            if (difference.Days > 250)
                            {
                                foreach (var group in allGroups)
                                {
                                    group.IncreaseCourse();
                                    if (group.StudyIsOver())
                                        await groupRepository.DeleteAsync(group);
                                }
                                await dbContext.SaveChangesAsync();
                            }
                        }
                    }
                }
                await Task.Delay(TimeSpan.FromDays(1));
            }
        }
    }
}
