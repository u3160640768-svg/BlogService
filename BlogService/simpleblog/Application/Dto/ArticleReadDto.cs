using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Application.Dto
{
    public record ArticleReadDto
    (
        string Id,
        string Title,
        string Content,
        DateTime CreatedAt
    );
}
