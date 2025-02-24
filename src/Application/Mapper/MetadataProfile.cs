using AutoMapper;
using Domain.Dtos.Metadata;
using Domain.Entities;

namespace Application.Mapper;

public class MetadataProfile : Profile
{
    public MetadataProfile()
    {
        CreateMap<AddMetadataRequestDto, DocumentMetadata>();
        CreateMap<DocumentMetadata, MetadataResponseDto>();
    }
}
