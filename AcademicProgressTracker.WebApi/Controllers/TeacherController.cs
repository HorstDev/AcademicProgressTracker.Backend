using AcademicProgressTracker.Application.Common.ViewModels.Group;
using AcademicProgressTracker.Application.Common.ViewModels.Subject;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public TeacherController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
