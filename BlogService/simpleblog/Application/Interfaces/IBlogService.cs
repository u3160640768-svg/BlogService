using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogService
    {
        public Task CreateArticleAsync(ArticleCreateDto articleDto);
        public Task <ArticleReadDto?> GetArticleByIdAsync(string id);
        public Task<IEnumerable<ArticleReadDto>> GetAllArticlesAsync();
        public Task UpdateArticleAsync(string id, ArticleCreateDto articleDto);    
        public Task DeleteArticleAsync(string id);
    }
}
