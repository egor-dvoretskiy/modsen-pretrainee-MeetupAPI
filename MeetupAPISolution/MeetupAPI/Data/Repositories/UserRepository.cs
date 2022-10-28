using MeetupAPI.Data.Repositories.Interfaces;
using MeetupAPI.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            this._context = context;
        }

        public async Task Add(UserModel entity)
        {
            this._context.UserModels.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteById(int id)
        {
            if (!await this.IsExist(id))
                return false;

            var entity = await this.GetById(id);
            if (entity == null)
                return false;

            this._context.UserModels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return await this._context.UserModels.ToListAsync();
        }

        public async Task<UserModel?> GetById(int id)
        {
            return await this._context.UserModels
                .Where(x => x.UserId == id)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await this._context.UserModels.AnyAsync(e => e.UserId == id);
        }

        public async Task<bool> Update(UserModel entity)
        {
            var model = await this.GetById(entity.UserId);
            if (model == null)
                return false;

            //temporary decision -> problem with replacing nested list Speakers.
            this._context.UserModels.Remove(model);
            await this.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await this.IsExist(entity.UserId))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
