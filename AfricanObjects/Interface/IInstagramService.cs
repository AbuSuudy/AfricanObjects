using AfricanObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AfricanObjects.Interface
{
    public interface IInstagramService
    {
        Task<bool> PostImage(string imageURL, string caption, CancellationToken token);
        Task<bool> CreatPost(string imageContentId, CancellationToken token);
        Task<bool> StartGramming(CancellationToken token);
        Task<bool> LongLivedToken();
    }
}
