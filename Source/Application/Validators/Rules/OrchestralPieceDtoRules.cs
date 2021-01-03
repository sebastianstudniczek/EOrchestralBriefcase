using System.Text.RegularExpressions;
using FluentValidation;

namespace EOrchestralBriefcase.Application.Validators.Rules
{
    public static class OrchestralPieceDtoRules
    {
        public static IRuleBuilderOptions<T, string> TitleValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }

        public static IRuleBuilderOptions<T, int> NumberInBriefcaseValidation<T>(this IRuleBuilder<T, int> rule)
        {
            return rule
                .NotNull()
                .GreaterThan(0);
        }

        public static IRuleBuilderOptions<T, int?> TempoValidation<T>(this IRuleBuilder<T, int?> rule)
        {
            return rule
                .GreaterThan(0);
        }

        public static IRuleBuilderOptions<T, string> SongLinkValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .Must(BeYoutubeUrl)
                    .WithMessage("Invalid song link.");
        }

        private static bool BeYoutubeUrl(object value)
        {
            var regex =
                new Regex(@"^(?:https:\/\/)?(?:www\.)?(?:youtube\.com|youtu\.be)\/watch\?v=([a-zA-Z0-9_]+)");

            if (value == null || regex.IsMatch(value.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}
