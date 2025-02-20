using AutoMapper;
using Domain.Dtos.Document;
using Domain.Entities;

namespace Application.Mapper;

public class DocumentProfile : Profile
{
    public DocumentProfile()
    {
        CreateMap<DocumentUploadDto, Document>()
           .ForMember(dest => dest.Name,
               opt => opt.MapFrom(src => src.DocumentName))
           .ForMember(dest => dest.Description,
               opt => opt.MapFrom(src => src.DocumentDescription))
           .ForMember(dest => dest.ExpirationDate,
               opt => opt.MapFrom(src => src.ExpirationDate));

        CreateMap<DocumentUpdateDto, Document>();
    }
}
