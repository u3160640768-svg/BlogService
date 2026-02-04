using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlogRepository
    {
        Task SaveAsync(BlogPost article);
        Task<BlogPost?> GetByIdAsync(string id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task UpdateAsync(BlogPost article);

        Task DeleteAsync(string id);

        
    }
}
