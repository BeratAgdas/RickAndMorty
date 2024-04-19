using Business.Interface;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class CharacterRepository : IRepository<Character>
    {
        private readonly RickAndMortyDbContext _dbContext;

        public CharacterRepository(RickAndMortyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Character entity)
        {
            _dbContext.Characters.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Character entity)
        {
            _dbContext.Characters.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Character>> GetAllAsync()
        {
            return await _dbContext.Characters.ToListAsync();
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            return await _dbContext.Characters.FindAsync(id);
        }

        public async Task<List<Character>> GetCharactersBySearchCriteria(string character = null, string property = null)
        {
            var characters = new List<Character>();

            using (var context = _dbContext)
            {
                var query = context.Characters.AsQueryable();

                if (property != null)
                {
                    query = query.Where(e => e.Name == property);
                }
                characters = query.ToList();
            }

            return characters;
        }

        public Task<List<Character>> GetEpisodesBySearchCriteria(string character = null, string property = null)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Character entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
