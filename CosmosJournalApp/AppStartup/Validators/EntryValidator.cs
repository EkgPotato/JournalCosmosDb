using FluentValidation;
using WebApi.Models.DTOs;
namespace WebApi.AppStartup.Validators;
public class EntryValidator : AbstractValidator<EntryDTO>
{
    public EntryValidator()
    {
        RuleFor(entry => entry.Title).NotNull().NotEmpty();
    }
}
