using AutoMapper;

namespace Epam.NoteAppUI.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => RoleEnum.User))
            .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore());
        }
    }
}
