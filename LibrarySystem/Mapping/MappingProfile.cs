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
            CreateMap<AddPositionApi, Position>();
            CreateMap<AddPublisherApi, Publisher>();
            CreateMap<AddGenreApi, Genre>();
            CreateMap<AddLanguageApi, Language>();
            CreateMap<AddAuthorApi, Author>();
            CreateMap<AddStorageApi, Storage>();
            CreateMap<UpdateStorageApi, Storage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LogsId, opt => opt.Ignore());
            CreateMap<UpdateAuthorApi, Author>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.LogsId, opt => opt.Ignore());
            CreateMap<UpdateLanguageApi, Language>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.LogsId, opt => opt.Ignore());
            CreateMap<UpdatePublisherApi, Publisher>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.LogsId, opt => opt.Ignore());
            CreateMap<UpdateGenreApi, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LogsId, opt => opt.Ignore());
            CreateMap<UpdatePositionApi, Position>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LogsId, opt => opt.Ignore());
            CreateMap<EditProfileView, Person>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LogsId, opt => opt.Ignore());

        }
    }
}
