using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Parties
{
    public interface IPartyRepository
    {
        Task<bool> PayerExistsAsync(long payerId);
        Task<bool> ManufacturerExistsAsync(long manufacturerId);
    }
}
