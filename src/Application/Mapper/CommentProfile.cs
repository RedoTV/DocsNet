using AutoMapper;
using Domain.Dtos.Comment;
using Domain.Entities;

namespace Application.Mapper;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<DocumentComment, CommentResponseDto>();
    }
}
