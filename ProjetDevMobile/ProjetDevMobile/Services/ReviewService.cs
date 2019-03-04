using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProjetDevMobile.Client;
using ProjetDevMobile.Model;
using static ProjetDevMobile.Model.Review;

namespace ProjetDevMobile.Services
{
    public class ReviewService : IReviewService
    {
        private List<Review> _reviews;
        private List<Review> _resultReviews;

        private ILiteDBClient _liteDBClient;
        private string _dbCollectionReview = "reviewsCollection";

        public ReviewService(ILiteDBClient liteDBClient)
        {
            _liteDBClient = liteDBClient;

            _reviews = new List<Review>();
            _resultReviews = new List<Review>();

            //Init();
            //TestInsert();
        }

        private void TestInsert()
        {
            _liteDBClient.CleanCollection(_dbCollectionReview);

            Review review = new Review("Restaurant", "C'est super bof", ReviewTypes.Food.ToString());
            _liteDBClient.InsertObjectInDB<Review>(review, _dbCollectionReview);

            _reviews.Add(review);
        }

        private void Init()
        {
            _reviews = _liteDBClient.GetCollectionFromDB<Review>(_dbCollectionReview);
        }

        public void AddReview(Review review)
        {
            _liteDBClient.InsertObjectInDB<Review>(review, _dbCollectionReview);
            _reviews.Add(review);
        }

        public void UpdateReview(Review review)
        {
            _liteDBClient.UpdateObjectInDB<Review>(review, _dbCollectionReview);
            _reviews[_reviews.FindIndex(rev => rev.Id == review.Id) ] = review;
        }

        public void DeleteReview(Review review)
        {
            _liteDBClient.RemoveObjectFromDB<Review>(review.Id, _dbCollectionReview);
            _reviews.Remove(review);
        }

        public void DeleteReview(int Id)
        {
            _liteDBClient.RemoveObjectFromDB<Review>(Id, _dbCollectionReview);
            Review ReviewToDelete = _reviews.Where(rev => rev.Id == Id).First();
            if (ReviewToDelete != null)
            {
                _reviews.Remove(ReviewToDelete);
            }
        }

        public Review GetReviewByIndex(int pos)
        {
            return _reviews[pos];
        }

        public Review GetReviewById(int Id)
        {
            return _reviews.Where(rev => rev.Id == Id).First() ;
        }

        public List<Review> GetReviews(bool food, bool drink, bool toSee)
        {
            _resultReviews.Clear();
            if (food)
            {
                _resultReviews.AddRange(_reviews.Where(review => review.Tag.Equals(ReviewTypes.Food.ToString())));
            }

            if (drink)
            {
                _resultReviews.AddRange(_reviews.Where(review => review.Tag.Equals(ReviewTypes.Drink.ToString())));
            }


            if (toSee)
            {
                _resultReviews.AddRange(_reviews.Where(review => review.Tag.Equals(ReviewTypes.ToSee.ToString())));
            }

            return _resultReviews;
        }
    }
}
