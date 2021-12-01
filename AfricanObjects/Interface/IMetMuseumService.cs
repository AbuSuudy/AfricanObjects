using AfricanObjects.Models;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface IMetMuseumService
    {

        public  Task<int> GetObjectId();

        public Task<TweetObject> GetMuseumObject();
    }
}
