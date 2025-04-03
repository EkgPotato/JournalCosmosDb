using AutoMapper;
using WebApi.Models.DTOs;
using WebApi.Models.Entities;
using WebApi.Models.ViewModels;

namespace WebApi.Mapper;

public class EntryMapper : Profile
{
    public EntryMapper()
    {
        CreateMap<Entry, EntryDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(e => e.Title))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(e => e.Category))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(e => e.Content));

        CreateMap<Entry, EntryViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(e => e.Title))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(e => e.Category))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(e => e.Content))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(e => e.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(e => e.UpdatedAt));

        CreateMap<EntryDTO, Entry>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(e => e.Title))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(e => e.Category))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(e => e.Content));
    }
}
