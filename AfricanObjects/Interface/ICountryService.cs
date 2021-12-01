using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface ICountryService
    {
        public  Task<List<String>> GetAllAfricanCountries();
    }
}
