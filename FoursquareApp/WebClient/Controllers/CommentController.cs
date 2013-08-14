using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebClient.Models;
using WebClient.Repositories;

namespace WebClient.Controllers
{
    public class CommentController : BaseApiController
    {
        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage AddComment(int placeId, string sessionKey, [FromBody]CommentModel comment)
        {
            var response = this.PerformOperation(() =>
            {
                var userId = UsersRepository.LoginUser(sessionKey);
                CommentsRepository.AddComment(userId, placeId, comment);
            });
            return response;
        }
    }
}