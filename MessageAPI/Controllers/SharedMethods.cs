using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace MessagingService.Controllers
{
    public static class SharedMethods
    {
        public static bool EmailMatchesPattern(string email)
        {
            var regex = new Regex("^\\S+@\\S+\\.\\S+$");
            return regex.Matches(email).Count() == 1;
        }
    }
}
