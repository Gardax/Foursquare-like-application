using ClassesData;
using ClassesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.Models;

namespace WebClient.Repositories
{
    public class CommentsRepository : BaseRepository
    {
        private const string ValidCommentTextChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890 -";
        private const int MinCommentTextChars = 4;
        private const int MaxCommentTextChars = 300;

        public static void AddComment(int userId, int placeId, CommentModel comment)
        {
            ValidateComment(comment);
            FoursquareContext context = new FoursquareContext();
            var place = context.Places.FirstOrDefault(p => p.Id == placeId);

            if (place == null)
            {
                throw new ServerErrorException("Place does not exist anymore.", "INV_NICK_LEN");
            }

            Comment newComment = new Comment();
            newComment.Text = comment.Text;
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ServerErrorException("User does not exist anymore.", "INV_NICK_LEN");
            }

            newComment.User = user;
            place.Comments.Add(newComment);
            context.SaveChanges();
        }

        private static void ValidateComment(CommentModel comment)
        {
            if (comment.Text == null || comment.Text.Length < MinCommentTextChars || comment.Text.Length > MaxCommentTextChars)
            {
                throw new ServerErrorException("Invalid Comment Text", "INV_NICK_LEN");
            }
        }
    }
}