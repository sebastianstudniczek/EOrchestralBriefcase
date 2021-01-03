using System.Text.RegularExpressions;

namespace EOrchestralBriefcase.Application.Validators
{
    public static class FluentValidationHelper
    {
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
