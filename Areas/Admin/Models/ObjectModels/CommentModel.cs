using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ObjectModels
{
    public class CommentModel
    {
        private int id;
        private string content;
        private int userId;
        private int postId;
        private string createAt;
        private string deletedAt;
        private UserModel author;

        public int Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public int UserId { get => userId; set => userId = value; }
        public int PostId { get => postId; set => postId = value; }
        public string CreateAt { get => createAt; set => createAt = value; }
        public string DeletedAt { get => deletedAt; set => deletedAt = value; }
        public UserModel Author { get => author; set => author = value; }
    }
}