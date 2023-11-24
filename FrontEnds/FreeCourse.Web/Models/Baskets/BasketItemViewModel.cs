using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FreeCourse.Web.Models.Baskets
{
    public class BasketItemViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        private decimal? DiscountAppliedPrice { get; set; }

        public decimal GetCurrentPrice
        {
            get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
        }

        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
    }
}
