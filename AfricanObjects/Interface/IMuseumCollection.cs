using AfricanObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface IMuseumCollection
    {
        Task<MuseumObject> GetMuseumObjectFromCollection();
    }
}
