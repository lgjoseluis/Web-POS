using AutoMapper;
using FluentValidation.Results;
using WebPOS.Application.Commons.Foundations;
using WebPOS.Application.Contracts;
using WebPOS.Application.Dtos.Request;
using WebPOS.Application.Dtos.Response;
using WebPOS.Application.Validators.Categories;
using WebPOS.Domain.Entities;
using WebPOS.Infrastructure.Commons.Foundation.Request;
using WebPOS.Infrastructure.Commons.Foundation.Response;
using WebPOS.Infrastructure.Persistences.Contracts;
using WebPOS.Utilitties.Statics.Strings;

namespace WebPOS.Application.Services
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CategoryValidator _validator;

        public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters)
        {
            BaseResponse<BaseEntityResponse<CategoryResponseDto>> response = new BaseResponse<BaseEntityResponse<CategoryResponseDto>>();
            BaseEntityResponse<Category> categories = await _unitOfWork.CategoryRepository.ListCategory(filters);

            if (categories is not null) 
            { 
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<CategoryResponseDto>>(categories);
                response.Message = MessagesReply.MESSAGE_QUERY_SUCCESS;

                return response;
            }
            
            response.IsSuccess = false;
            response.Message = MessagesReply.MESSAGE_QUERY_EMPTY;
            
            return response;
        }

        public async Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories()
        {
            BaseResponse<IEnumerable<CategorySelectResponseDto>> response = new BaseResponse<IEnumerable<CategorySelectResponseDto>>();
            IEnumerable<Category> categories = await _unitOfWork.CategoryRepository.ListSelectCategory();

            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDto>>(categories);
                response.Message = MessagesReply.MESSAGE_QUERY_SUCCESS;

                return response;
            }

            response.IsSuccess = false;
            response.Message = MessagesReply.MESSAGE_QUERY_EMPTY;

            return response;
        }
       
        public async Task<BaseResponse<CategoryResponseDto>> GetCategoryById(int categoryId)
        {
            BaseResponse<CategoryResponseDto> response = new BaseResponse<CategoryResponseDto>();
            Category? category = await _unitOfWork.CategoryRepository.CategoryById(categoryId);

            if(category is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<CategoryResponseDto>(category);
                response.Message = MessagesReply.MESSAGE_QUERY_SUCCESS;

                return response;
            }

            response.IsSuccess = false;
            response.Message = MessagesReply.MESSAGE_QUERY_EMPTY;

            return response;
        }

        public async Task<BaseResponse<bool>> AddCategory(CategoryRequestDto request)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            ValidationResult validationResult = await _validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                response.Message = MessagesReply.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;                
                response.IsSuccess = false;

                return response;
            }

            Category category = _mapper.Map<Category>(request);
            response.Data = await _unitOfWork.CategoryRepository.AddCategory(category);

            if(response.Data)
            {
                response.IsSuccess = true;
                response.Message= MessagesReply.MESSAGE_ADD;

                return response;
            }

            response.IsSuccess = false;
            response.Message = MessagesReply.MESSAGE_FAILEDY;

            return response;
        }

        public async Task<BaseResponse<bool>> UpdateCategory(int categoryId, CategoryRequestDto request)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            ValidationResult validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                response.Message = MessagesReply.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                response.IsSuccess = false;

                return response;
            }

            Category? categoryEdit = await _unitOfWork.CategoryRepository.CategoryById(categoryId);
            
            if(categoryEdit is null) 
            {
                response.IsSuccess = false;
                response.Message = MessagesReply.MESSAGE_QUERY_EMPTY;

                return response;
            }

            Category category = _mapper.Map<Category>(request);

            category.CategoryId = categoryId;

            response.Data = await _unitOfWork.CategoryRepository.UpdateCategory(category);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = MessagesReply.MESSAGE_UPDATE;

                return response;
            }

            response.IsSuccess = false;
            response.Message = MessagesReply.MESSAGE_FAILEDY;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteCategory(int categoryId)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            Category? category = await _unitOfWork.CategoryRepository.CategoryById(categoryId);

            if (category is null) 
            { 
                response.IsSuccess = false;
                response.Message= MessagesReply.MESSAGE_QUERY_EMPTY;

                return response;
            }

            response.Data = await _unitOfWork.CategoryRepository.DeleteCategory(categoryId);

            if (response.Data) 
            { 
                response.IsSuccess = true;
                response.Message = MessagesReply.MESSAGE_DELETE;

                return response;
            }

            response.IsSuccess = false;
            response.Message = MessagesReply.MESSAGE_FAILEDY;

            return response;
        }        
    }
}
