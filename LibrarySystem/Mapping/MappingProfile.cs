using AutoMapper;
using LibrarySystem.Models.Api;
using LibrarySystem.Models.View;
using LibrarySystemModels;
using System.Globalization;

namespace LibrarySystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Person, EditProfileView>();
            CreateMap<RegisterView, Person>();
            CreateMap<AddPositionApi, Position>();
            CreateMap<AddPersonView, Person>();
            CreateMap<AddPublisherApi, Publisher>();
            CreateMap<AddGenreApi, Genre>();
            CreateMap<AddLanguageApi, Language>();
            CreateMap<AddAuthorApi, Author>();
            CreateMap<AddStorageApi, Storage>();
            CreateMap<Book, UpdateBookView>();
            CreateMap<Person, UpdatePersonView>();
            CreateMap<UpdatePersonView, Person>();
            CreateMap<UpdateBookView, BookView>();
            CreateMap<UpdateBookView, Book>()
           .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateStorageApi, Storage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateAuthorApi, Author>()
              .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateLanguageApi, Language>()
              .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdatePublisherApi, Publisher>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateGenreApi, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdatePositionApi, Position>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EditProfileView, Person>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
         

        }
    }
}
