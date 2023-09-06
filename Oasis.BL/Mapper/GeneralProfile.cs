using AutoMapper;
using Oasis.BL.DTOs.ToDoDto;
using Oasis.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.Mapper
{
    public class GeneralProfile :Profile
    {
        public GeneralProfile()
        {
            CreateMap<ViewToDoDto,ToDo>().ReverseMap();
            CreateMap<Create_Update_ToDoDto, ToDo>().ReverseMap();
        }
    }
}
