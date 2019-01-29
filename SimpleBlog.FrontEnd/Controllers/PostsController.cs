using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.FrontEnd.Infrastructure;
using SimpleBlog.FrontEnd.Models;
using SimpleBlog.FrontEnd.ViewModels;

namespace SimpleBlog.FrontEnd.Controllers
{
    public class PostsController : Controller
    {
        /**
         * TODO : should be in an interface dll (extension shared model)
         */
        const string STRING_SEPARATOR_TITLE_ID = "_n";

        protected readonly IPostsRepository postsRepo;
        protected readonly ICommentsRepository commentsRepo;

        public PostsController(IPostsRepository postsRepo, ICommentsRepository commentsRepo) 
        {
            this.postsRepo = postsRepo;
            this.commentsRepo = commentsRepo;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await postsRepo.GetAll<Post>();
            var comments = await commentsRepo.GetAll<Comment>();
            foreach (var post in posts)
            {
                post.NbComments = comments.Count(c => c.PostId == post.Id);
            }
            var vm = new PostsViewModel 
            {
                Posts = posts.ToList()
            };
            return View(vm);
        }

        public async Task<IActionResult> Details(string id)
        {
            var postId = GetIdFromSlug(id);
            var post = await postsRepo.Get<Post>(postId);
            var comments = await commentsRepo.GetAll<Comment>(postId);
            var vm = new PostDetailsViewModel 
            {
                Post = post,
                Comments = comments.ToList(),
            };
            return View(vm);
        }

        /// <summary>
        /// TODO : this treatment should not be here. but to go fast. 
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        private int GetIdFromSlug(string slug)
        {
            try
            {
                var id = slug.Split(STRING_SEPARATOR_TITLE_ID).Last();
                return int.Parse(id);
            }
            catch (Exception e)
            {
                // log and manage error
                throw e;
            }
        }
    }
}
