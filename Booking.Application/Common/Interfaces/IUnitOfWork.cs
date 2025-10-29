using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IHouseRepository House { get; }
        IHouseNumberRepository HouseNumber { get; }
        IAmenityRepository Amenity { get; }
        Task SaveAsync();
    }
}
