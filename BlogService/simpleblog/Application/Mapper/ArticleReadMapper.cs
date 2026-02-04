using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Entities;

namespace Application.Mapper
{
    public static class ArticleReadMapper
    {
        public static BlogPost toEntity(this ArticleReadDto dto)
        {
            return new BlogPost(new Guid(dto.Id),dto.Title, dto.Content, dto.CreatedAt);
        }
        public static ArticleReadDto ToArticleReadDto(this BlogPost entity)
        {
            return new ArticleReadDto
                (
                    entity.Id.ToString(),
                    entity.Title,
                    entity.Content,
                    entity.CreatedAt
                );
        }
    }
}
