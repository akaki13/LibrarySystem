using AutoMapper;
using LibrarySystem.Models.Api;
using LibrarySystem.Models.View;
using LibrarySystemModels;

namespace LibrarySystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Person, EditProfileView>();
            CreateMap<RegisterView, Person>();
            CreateMap<PositionApi, Position>();
            CreateMap<UpdatePositionApi, Position>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LogsId, opt => opt.Ignore());
            CreateMap<EditProfileView, Person>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LogsId, opt => opt.Ignore());

        }
    }
}
