using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Interfaces
{
    public interface IHouseRepository : IRepository<House>
    {
        void Update(House house);
    }
}
