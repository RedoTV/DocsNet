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

        CreateMap<Document, DocumentHistory>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType))
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(30)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<Document, DocumentResponseDto>();
    }
}
