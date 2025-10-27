using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Interfaces
{
    public interface IHouseRepository
    {
        IEnumerable<House> GetAll(Expression<Func<House, bool>>? filter = null, string? includeProperties = null);
        House Get(Expression<Func<House, bool>> filter, string? includeProperties = null);
        void Add(House house);
        void Update(House house);
        void Remove(House house);
        Task Save();
    }
}
