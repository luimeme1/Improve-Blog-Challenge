using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlog.FrontEnd.Infrastructure 
{
    public interface ICommentsRepository 
    {
        Task<IList<T>> GetAll<T>(int postId);
        Task<IList<T>> GetN<T>(int postId, int count);
        Task<IList<T>> GetAll<T>();
    }
}