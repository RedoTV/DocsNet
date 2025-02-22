using AutoMapper;
using DocsNetAPI.Dtos.Document;
using Domain.Dtos.Document;
using Domain.Dtos.File;

namespace DocsNetAPI.Mapper;

public class DocumentFormProfile : Profile
{
    public DocumentFormProfile()
    {
        CreateMap<DocumentUploadRequest, DocumentUploadDto>()
            .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(dest => dest.DocumentDescription, opt => opt.MapFrom(src => src.DocumentDescription))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.FileData, opt => opt.MapFrom(src => new FileData
            {
                FileName = src.File.FileName,
                ContentType = src.File.ContentType,
                FileStream = src.File.OpenReadStream()
            }));

        CreateMap<DocumentUpdateRequest, DocumentUpdateDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.DocumentName))
            .ForMember(dest => dest.DocumentDescription, opt => opt.MapFrom(src => src.DocumentDescription))
            .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.FileData, opt => opt.MapFrom((src, dest) =>
                src.File != null && src.File.Length > 0
                    ? new FileData
                    {
                        FileName = src.File.FileName,
                        ContentType = src.File.ContentType,
                        FileStream = src.File.OpenReadStream()
                    }
                    : null));
    }
}
