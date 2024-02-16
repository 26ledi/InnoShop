using AutoMapper;
using InnoShop.Messages.Shared;
using UserManagement.API.Requests;
using UserManagement.API.Responses;
using UserManagement.BusinessLogic.DTO_s;
using UserManagement.DataAccess.Entities;

namespace UserManagement.API.Profiles
{
    /// <summary>
    /// The profiles of the application
    /// </summary>
    public class UserProfile : Profile
    {
        // <summary>
        /// Initializes a new instance of <see cref="UserProfile"/>
        /// </summary>
        public UserProfile()
        {
            CreateMap<UserLoginRequest, User>().ReverseMap();

            CreateMap<UserLoginRequest, UserLoginDto>().ReverseMap();

            CreateMap<UserRegisterRequest, User>().ReverseMap();

            CreateMap<UserResponse, UserDto>().ReverseMap();

            CreateMap<UserRegisterRequest, UserDto>().ReverseMap();

            CreateMap<UserChangePasswordResponse, UserDto>().ReverseMap();

            CreateMap<UserChangePasswordResponse, UserResponse>().ReverseMap();

            CreateMap<UserResponse, TokenDto>().ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<UserResponse, UserDto>().ReverseMap()
            .ForMember(dest => dest.AccesToken, opt => opt.Ignore());

            CreateMap<UserRegisterResponse, UserDto>().ReverseMap();

            CreateMap<UserChangePasswordRequest, UserChangePasswordDto>().ReverseMap();
            CreateMap<UserConfirmPasswordRequest, UserConfirmPasswordDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserLoginDto>().ReverseMap();

            CreateMap<UserRegisterMessage, UserDto>().ReverseMap()
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());
            CreateMap<UserRegisterMessage, UserDto>().ReverseMap()
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());

        }
    }
}
