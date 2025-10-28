using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository
{
    internal class HouseNumberRepository : Repository<HouseNumber>, IHouseNumberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseNumberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(HouseNumber houseNumber)
        {
            _dbContext.Update(houseNumber);
        }
    }
}
