using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;
using TodoAPI.Repositories;

namespace TodoAPI.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _repo;
        public CardService(ICardRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await _repo.Get();
        }
        public async Task<Card> GetOneAsync(int id)
        {
            return await _repo.Get(id);
        }
        public async Task<Card> AddAsync(Card card)
        {
            await _repo.Create(card);
            return card;
        }
        public async Task<Card> UpdateAsync(Card card)
        {
            await _repo.Update(card);
            return card;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            await _repo.Delete(id);
            return true;
        }
    }
}
