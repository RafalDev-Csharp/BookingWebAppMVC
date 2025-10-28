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
    public class HouseRepository : Repository<House>, IHouseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(House house)
        {            
            _dbContext.Update(house);
        }
    }
}
