using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Entities;

namespace Application.Mapper
{
    public static class ArticleCreateMapper
    {
        public static BlogPost toEntity(this ArticleCreateDto dto)
        {
            return new BlogPost(dto.Title, dto.Content);
        }
        public static ArticleCreateDto ToArticleCreateDto(this BlogPost entity) 
        {
            return new ArticleCreateDto(entity.Title, entity.Content);
        }
    }
}
