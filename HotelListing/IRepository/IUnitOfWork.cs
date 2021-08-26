using HotelListing.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;

namespace HotelListing.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> CountryRepository { get; }

        IGenericRepository<Hotel> HotelRepository { get; }

        Task Save();
    }
}
