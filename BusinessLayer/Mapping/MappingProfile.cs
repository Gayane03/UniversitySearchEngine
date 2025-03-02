using AutoMapper;
using BusinessLayer.Helper;
using SharedLibrary.DbModels.Request;
using SharedLibrary.DbModels.Response;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels.CoreResponse;

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
			.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHelper.HashAndStore(src.Password)));

			CreateMap<LoginRequest, LoginRequestDB>()
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

			CreateMap<UniversityResponseDB, UniversityResponse>()
				.ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.Name));
				//.ForMember(dest => dest.UniversityLogo, opt => opt.MapFrom(src => src.LogoUrl));

			CreateMap<FacultyResponseDB, FacultyResponse>()
				.ForMember(dest => dest.FacultyId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FacultyDescription, opt => opt.MapFrom(src => src.Description))
				.ForMember(dest => dest.FacultyName, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.FreeSpots, opt => opt.MapFrom(src => src.FreeSpots))
				.ForMember(dest => dest.PaidSpots, opt => opt.MapFrom(src => src.PaidSpots))
				.ForMember(dest => dest.TuitionFee, opt => opt.MapFrom(src => src.TuitionFee))
				.ForMember(dest => dest.UniversityId, opt => opt.MapFrom(src => src.UniversityId))
			    .ForMember(dest => dest.LastYearMinScoreForFreeTrain, opt => opt.MapFrom(src => src.LastYearMinScoreForFreeTrain))
				//.ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.IsFavorite))
				.ForMember(dest => dest.UniversityName, opt => opt.MapFrom(src => src.UniversityName));
		}
    }
}
