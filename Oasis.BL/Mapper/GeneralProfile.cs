using AutoMapper;
using Oasis.BL.DTOs.ToDoDto;
using Oasis.Data.Entities;


namespace Oasis.BL.Mapper
{
    public class GeneralProfile :Profile
    {
        public GeneralProfile()
        {
            CreateMap<ViewToDoDto,ToDo>().ReverseMap();
            CreateMap<CreateToDoDto, ToDo>().ReverseMap();
            CreateMap<UpdateToDoDto, ToDo>().ReverseMap();
        }
    }
}
