using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Infrastructure.Data;
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

        public IEnumerable<House> Get(Expression<Func<House, bool>> filter, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<House> GetAll(Expression<Func<House, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(House house)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(House house)
        {
            throw new NotImplementedException();
        }
    }
}
