using AfricanObjects.Models;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface IMuseumService
    {
        public  Task GetMaxRange();

        public Task<MuseumObject> GetMuseumObject();

       
    }
}
