using AfricanObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface IInstagramService
    {
        Task<bool> PostImage(string imageURL, string caption);
        Task<bool> CreatPost(string imageContentId);
        Task<bool> StartGramming();
    }
}
