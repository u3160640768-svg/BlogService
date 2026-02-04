using Application.Dto;
using Application.Interfaces;
using Application.Mapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repository;
        public BlogService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateArticleAsync(ArticleCreateDto articleDto)
        {
            var entity = articleDto.toEntity();
            await _repository.SaveAsync(entity);
        }

        public async Task DeleteArticleAsync(string id)
        {
            await _repository.DeleteAsync(id);    
        }

        public async Task<IEnumerable<ArticleReadDto>> GetAllArticlesAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => e.ToArticleReadDto());
        }

        public async Task<ArticleReadDto?> GetArticleByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return entity.ToArticleReadDto();

        }

        public async Task UpdateArticleAsync(string id, ArticleCreateDto articleDto)
        {
            var entity= await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("NO");
            entity.Title=articleDto.Title;
            entity.Content=articleDto.Content;
            await _repository.UpdateAsync(entity);
        }
    }
}
