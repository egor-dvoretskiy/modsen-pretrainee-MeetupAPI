using MeetupAPI.Data.Repositories.Interfaces;
using MeetupAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Data.Repositories
{
    public class MeetupRepository : IMeetupRepository
    {
        private readonly MeetupAPIDbContext _context;

        public MeetupRepository(MeetupAPIDbContext context)
        {
            this._context = context;
        }

        public async Task Add(MeetupModel entity)
        {
            this._context.MeetupModels.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteById(int id)
        {
            if (!await this.IsExist(id))
                return false;

            var entity = await this.GetById(id);
            if (entity == null)
                return false;

            this._context.MeetupModels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MeetupModel>> GetAll()
        {
            return await this._context.MeetupModels
                .Include(x => x.Speakers)
                .ToListAsync();
        }

        public async Task<MeetupModel?> GetById(int id)
        {
            return await this._context.MeetupModels
                .Where(x => x.Id == id)
                .Include(x => x.Speakers)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await this._context.MeetupModels.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> Update(MeetupModel entity)
        {
            var model = await this.GetById(entity.Id);
            if (model == null)
                return false;

            //temporary decision -> problem with replacing nested list Speakers.
            this._context.MeetupModels.Remove(model);
            await this.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await this.IsExist(entity.Id))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
