using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ObjectModels
{
    public class PostModel
    {
        private int id;
        private int tourId;
        private double rating;
        private string content;
        private List<string> images;
        private int totalLike;
        private int totalComment;
        private int authorId;
        private string createAt;
        private string deletedAt;
        private CommentModel featuredComment;
        private UserModel author;
        private TourModel tour;
        private bool isLike;

        public int Id { get => id; set => id = value; }
        public int TourId { get => tourId; set => tourId = value; }
        public double Rating { get => rating; set => rating = value; }
        public string Content { get => content; set => content = value; }
        public List<string> Images { get => images; set => images = value; }
        public int TotalLike { get => totalLike; set => totalLike = value; }
        public int TotalComment { get => totalComment; set => totalComment = value; }
        public int AuthorId { get => authorId; set => authorId = value; }
        public string CreateAt { get => createAt; set => createAt = value; }
        public string DeletedAt { get => deletedAt; set => deletedAt = value; }
        public CommentModel FeaturedComment { get => featuredComment; set => featuredComment = value; }
        public UserModel Author { get => author; set => author = value; }
        public TourModel Tour { get => tour; set => tour = value; }
        public bool IsLike { get => isLike; set => isLike = value; }
    }
}