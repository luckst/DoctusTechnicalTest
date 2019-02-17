namespace Doctus.TechinalTest.Web
{
    using AutoMapper;
    using Doctus.TechinalTest.Web.Models.Activities;
    using Doctus.TechinalTest.Web.Models.Users;
    using Doctus.TechnicalTest.Domain.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();

            CreateMap<Activity, ActivityViewModel>();
            CreateMap<ActivityViewModel, Activity>();

            CreateMap<Hour, HourViewModel>();
            CreateMap<HourViewModel, Hour>();
        }
    }
}
