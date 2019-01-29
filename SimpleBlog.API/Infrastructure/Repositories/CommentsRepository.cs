using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleBlog.API.Models;
using static SimpleBlog.API.Infrastructure.WebClient;

namespace SimpleBlog.API.Infrastructure 
{
    public class CommentsRepository : ICommentsRepository 
    {
        public static IWebClient _client;

        public CommentsRepository(IWebClient client)
        {
            _client = client;
        }

        public async Task<IList<Comment>> GetN<Comment>(int postId, int count)
        {
            var comments = await GetAll<Comment>(postId);
            return comments.Take(count).ToList();
        }

        public async Task<IList<Comment>> GetAll<Comment>(int postId)
        {
            _client.EntityType = EnumEntityType.comments;
            var json = await _client.GetData(postId + "");
            return JsonConvert.DeserializeObject<List<Comment>>(json);
        }

        public async Task<IList<Comment>> GetAll<Comment>()
        {
            _client.EntityType = EnumEntityType.comments;
            var json = await _client.GetData();
            return JsonConvert.DeserializeObject<List<Comment>>(json);
        }
    }
}