using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly CardContext _context;
        public CardRepository(CardContext context)
        {
            _context = context;
        }
        public async Task<Card> Create(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task Delete(int id)
        {
            var cardToDelete = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(cardToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> Get()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task<Card> Get(int id)
        {
            return await _context.Cards.FindAsync(id);
        }

        public async Task Update(Card card)
        {
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
