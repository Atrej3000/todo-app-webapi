using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public interface ICardService
    {
        Task<Card> AddAsync(Card card);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Card>> GetAllAsync();
        Task<Card> GetOneAsync(int id);
        Task<Card> UpdateAsync(Card card);
    }
}