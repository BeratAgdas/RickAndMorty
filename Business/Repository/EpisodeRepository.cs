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
    public class EpisodeRepository : IRepository<Episode>
    {
        private readonly RickAndMortyDbContext _dbContext;

        public EpisodeRepository(RickAndMortyDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Episode>> GetEpisodesBySearchCriteria(string character = null, string property = null)
        {
            var episodes = new List<Episode>();

            using (var context = _dbContext)
            {
                var query = context.Episodes.AsQueryable(); 

                if (character != null)
                {
                    query = query.Where(e => e.Characters.Any(c => c.AbsolutePath.Contains(character)));
                }
                if (property != null)
                {
                    query = query.Where(e => e.Name == property); 
                }
                episodes =  query.ToList();
            }

            return episodes;
        }



        public async Task<List<Episode>> GetAllAsync()
        {
            return await _dbContext.Episodes.ToListAsync();
        }

        public async Task<Episode> GetByIdAsync(int id)
        {
            return await _dbContext.Episodes.FindAsync(id);
        }

        public async Task AddAsync(Episode entity)
        {
            _dbContext.Episodes.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Episode entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Episode entity)
        {
            _dbContext.Episodes.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<Episode>> GetCharactersBySearchCriteria(string character = null, string property = null)
        {
            throw new NotImplementedException();
        }
    }
}
