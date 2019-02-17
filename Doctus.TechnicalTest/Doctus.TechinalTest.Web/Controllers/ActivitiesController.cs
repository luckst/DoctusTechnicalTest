
namespace Doctus.TechinalTest.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Doctus.TechinalTest.Web.Models;
    using Doctus.TechinalTest.Web.Models.Activities;
    using Doctus.TechnicalTest.Application.Services;
    using Doctus.TechnicalTest.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ActivitiesController : Controller
    {
        IActivityService activityService;
        IMapper mapper;

        public ActivitiesController(IActivityService activityService, IMapper mapper)
        {
            this.activityService = activityService;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public IEnumerable<Activity> Activities(int userId)
        {
            return activityService.GetActivities(userId);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]Activity activity)
        {
            var model = new ActivityViewModel();
            try
            {
                model = mapper.Map<ActivityViewModel>(activityService.AddActivity(activity));
                model.MessageType = MessageTypeEnum.success;
                model.Message = "Actividad creada";
            }
            catch (Exception)
            {
                model.MessageType = MessageTypeEnum.danger;
                model.Message = "Error en la aplicación";
            }

            model.ShowMessage = true;
            return Json(model);
        }
    }
}