using AutoMapper;
using SharedLibrary.DbModels.Request;
using SharedLibrary.RequestModels;

namespace BusinessLayer.Mapping
{
	public class MappingProfile: Profile
	{
        public MappingProfile()
        {
			CreateMap<RegistrationRequest, UserEmailRequest>()
		   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

			CreateMap<RegistrationRequest, User>()
			.ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
			.ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

			CreateMap<LoginRequest, LoginRequestDB>()
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
		}
    }
}
