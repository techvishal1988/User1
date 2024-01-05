using System.Text.RegularExpressions;

namespace User.Validators
{
    public static class Validation
    {
        public static bool IsValidEmail(this string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(email))
                return false;

            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }

        public static bool IsValid(this object param)
        {
            if (param == null) return true;

            if (Convert.ToInt32(param) <= 0)
                return false;
            else
                return true;
        }
    }
}
