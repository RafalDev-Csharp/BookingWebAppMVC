using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository
{
    public class HouseRepository : IHouseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(House house)
        {
            _dbContext.Add<House>(house);
        }

        public House Get(Expression<Func<House, bool>> filter, string? includeProperties = null)
        {
            IQueryable<House> query = _dbContext.Set<House>();
            if (filter is not null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                //  House_Number,House,.. xase sensitive
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<House> GetAll(Expression<Func<House, bool>>? filter = null, string? includeProperties = null)
        {
                                    //_dbContext.Houses..       -- the same
            IQueryable<House> query = _dbContext.Set<House>();
            if (filter is not null)
            {
                query = query.Where(filter); 
            }
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                //  House_Number,House,.. xase sensitive
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public void Remove(House house)
        {
            _dbContext.Remove<House>(house);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(House house)
        {
            _dbContext.Update<House>(house);
        }
    }
}
