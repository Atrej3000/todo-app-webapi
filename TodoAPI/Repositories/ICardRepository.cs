using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> Get();
        Task<Card> Get(int id);
        Task<Card> Create(Card card);
        Task Update(Card card);
        Task Delete(int id);
    }
}
