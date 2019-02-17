
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
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Exceptions;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class HoursController : Controller
    {
        IHourService hourService;
        IMapper mapper;

        public HoursController(IHourService hourService, IMapper mapper)
        {
            this.hourService = hourService;
            this.mapper = mapper;
        }

        [HttpGet("[action]")]
        public IEnumerable<Hour> Hours(int activityId)
        {
            return hourService.GetHours(activityId);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody]Hour hour)
        {
            var model = new HourViewModel();
            try
            {
                model = mapper.Map<HourViewModel>(hourService.AddHour(hour));
                model.MessageType = MessageTypeEnum.success;
                model.Message = "Hora creada";
            }
            catch (ExtraHoursException)
            {
                model.MessageType = MessageTypeEnum.warning;
                model.Message = "La cantidad de horas ingresadas supera las 8 permitidas por actividad.";
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