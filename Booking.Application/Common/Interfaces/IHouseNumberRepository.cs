using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Interfaces
{
    public interface IHouseNumberRepository : IRepository<HouseNumber>
    {
        void Update(HouseNumber houseNumber);
    }
}
