using ConsoleApp1;
using e_shop.Models;

namespace e_shop.Utils
{
    public class RatingService
    {
        private readonly MyContext _context;
        public RatingService(MyContext context)
        {
            _context = context;
        }

        public int GetAverageRating(Product product)
        {            
            List<Feedback> feedback = product.Feedback;

            if (feedback.Count == 0)
            {
                return 0;
            }

            int sum = feedback.Select(f => f.Stars)
                .Aggregate((a, b) => a + b);

            double res = (double)sum / (double)feedback.Count;

            return (int)Math.Ceiling(res);
        }
    }
}
