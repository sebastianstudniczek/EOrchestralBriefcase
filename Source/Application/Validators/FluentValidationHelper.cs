using System.Text.RegularExpressions;

namespace EOrchestralBriefcase.Application.Validators
{
    public static class FluentValidationHelper
    {
        public static bool BeYoutubeUrl(object value)
        {
            Regex regex =
                new Regex(@"^(?:https:\/\/)?(?:www\.)?(?:youtube\.com|youtu\.be)\/watch\?v=([a-zA-Z0-9_]+)");

            if (value == null || regex.IsMatch(value.ToString()))
            {
                return true;
            }

            return false;
        }

        public static bool BeInteger(int value)
        {
            Regex regex = new Regex(@"^[1-9]+[0-9]*$");

            if (regex.IsMatch(value.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}
