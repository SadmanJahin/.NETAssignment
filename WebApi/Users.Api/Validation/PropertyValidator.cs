using Azure.Core;
using FluentValidation;
using Users.Core.Entities;
using WebApi.Shared.Models;

namespace Users.Api.Validation
{
    public class PropertyValidator : AbstractValidator<PageRequest>
    {
        public PropertyValidator()
        {
            RuleForEach(x => x.Filters)
                .ChildRules(filters =>
                {
                    filters.RuleFor(f => f.PropertyName)
                        .Must(BeAValidPropertyName)
                        .WithMessage(f => $"Invalid column name: {f.PropertyName}");
                });

            RuleForEach(x => x.Sorts)
                .ChildRules(sorts =>
                {
                    sorts.RuleFor(f => f.PropertyName)
                        .Must(BeAValidPropertyName)
                        .WithMessage(f => $"Invalid column name: {f.PropertyName}");
                });
        }

        private bool BeAValidPropertyName(string propName)
        {
            var validProperties = typeof(User).GetProperties().Select(p => p.Name).ToList();

            return validProperties.Contains(propName, StringComparer.OrdinalIgnoreCase);
        }
    }
}
