using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Oasis.BL.DTOs.ToDoDto;
using Oasis.BL.IServices;
using Oasis.Data;
using Oasis.Data.Entities;
using Oasis.Data.IPersistance;
using Oasis.Data.IRepositories;


namespace Oasis.BL.Services
{
    public class ToDoServices : IToDoServices
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoRepository _toDoRepository;
        private readonly UserManager<UserTable> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ToDoServices(DataContext context,
                            IMapper mapper,
                            UserManager<UserTable> userManager,
                            SignInManager<UserTable> signInManager,
                            IUnitOfWork unitOfWork,
                            IToDoRepository toDoRepository,
                            IHttpContextAccessor httpContextAccessor)
        {

            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _toDoRepository = toDoRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        public async Task<bool> CreateToDoAsync(CreateToDoDto toDoitem)
        {
            try
            {
                var EntityItem = _mapper.Map<ToDo>(toDoitem);
                //

                //
                // get current user to get UserId column and put in ToDoEntity 
                var CurrentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                // this line will be edit in feature 
                EntityItem.UserId = CurrentUser.UserId;

                _toDoRepository.Add(EntityItem);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool DeleteToDo(int Id)
        {
            try
            {
                var Item = _toDoRepository.GetById(Id);
                _toDoRepository.Delete(Item);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public ViewToDoDto GetToDoItemById(int Id)
        {
            var Item = _toDoRepository.GetById(Id);
            var ItemDto = _mapper.Map<ViewToDoDto>(Item);
            return ItemDto;
        }

        public bool UpdateToDoItem(UpdateToDoDto dto)
        {
            try
            {
                var model = _mapper.Map<ToDo>(dto);
                _toDoRepository.Update(model);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
