
namespace Doctus.TechinalTest.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Doctus.TechinalTest.Web.Models;
    using Doctus.TechinalTest.Web.Models.Users;
    using Doctus.TechnicalTest.Application.Services;
    using Doctus.TechnicalTest.Domain.Entities;
    using Doctus.TechnicalTest.Infrastructure.Framework.Instrumentation.Exceptions;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        ISecurityService securityService;
        IMapper mapper;

        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            this.securityService = securityService;
            this.mapper = mapper;
        }

        [HttpPost("[action]")]
        public ActionResult Authenticate([FromBody]User request)
        {
            var model = new UserViewModel();
            try
            {
                model = mapper.Map<UserViewModel>(securityService.AuthenticateUser(request.UserName, request.Password));
                model.MessageType = MessageTypeEnum.success;
            }
            catch (UserNotFoundException)
            {
                model.MessageType = MessageTypeEnum.warning;
                model.Message = "Usuario o contraseña inconrrectos.";
                model.ShowMessage = true;
            }
            catch (Exception)
            {
                model.MessageType = MessageTypeEnum.danger;
                model.Message = "Error en la aplicación";
                model.ShowMessage = true;

            }

            return Json(model);
        }
    }
}