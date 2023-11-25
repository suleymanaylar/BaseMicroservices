using FluentValidation;
using FreeCourse.Web.Models.Discounts;

namespace FreeCourse.Web.Validatiors
{
    public class DiscountApplyInputValidatior:AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidatior()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim Kupon Alanı Boş olamaz.");
        }
    }
}
