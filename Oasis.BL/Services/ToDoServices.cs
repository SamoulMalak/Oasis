using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Oasis.BL.DTOs.ToDoDto;
using Oasis.BL.IServices;
using Oasis.Data;
using Oasis.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.Services
{
    public class ToDoServices : IToDoServices
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly UserManager<UserTable> userManager;
        private readonly SignInManager<UserTable> signInManager;

        public ToDoServices(DataContext context,
                            IMapper mapper,
                            UserManager<UserTable> userManager,
                            SignInManager<UserTable> signInManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> CreateToDoAsync(Create_Update_ToDoDto toDoitem)
        {
            var EntityItem = mapper.Map<ToDo>(toDoitem);
            // get current user to get UserId column and put in ToDoEntity 
             var CurrentUser =await userManager.GetUserAsync(signInManager.Context.User);
            // this line will be edit in feature 
            // EntityItem.UserId= CurrentUser.UserId;

            EntityItem.UserId = 1;
            context.ToDos.Add(EntityItem);
            var result =context.SaveChanges();
            return IsApplyOnDataBase(result);


        }

        public bool DeleteToDo(int Id)
        {
            var Item = context.ToDos.FirstOrDefault(x => x.Id == Id);
            if (Item != null)
            {
                context.ToDos.Remove(Item);
                var result =context.SaveChanges();
                return IsApplyOnDataBase(result);
            }
            return false;
        }

        public ViewToDoDto GetToDoItemById(int Id)
        {
            var Item =context.ToDos.Find(Id);
            var ItemDto =mapper.Map<ViewToDoDto>(Item);
            return ItemDto;
        }

        public bool UpdateToDoItem(int Id, Create_Update_ToDoDto NewItem)
        {
            var ExistingItem = context.ToDos.Find(Id);

            if (ExistingItem != null)
            {
                ExistingItem.Title = NewItem.Title;
                ExistingItem.Completed = NewItem.Completed;
             
                var result = context.SaveChanges();
                return IsApplyOnDataBase(result);
            }
            return false;
         
        }

        // function to check if the data apply on database successfully or not 
        // make this function to not repeat code 
        private bool IsApplyOnDataBase(int result)
        {
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
