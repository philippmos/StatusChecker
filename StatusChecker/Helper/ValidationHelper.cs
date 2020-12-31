using System.Collections.Generic;
using StatusChecker.Models.Database;

namespace StatusChecker.Helper
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Validate Gadget-Model and Return List with Message for invalid Fields
        /// </summary>
        /// <param name="gadget"></param>
        /// <returns></returns>
        public static List<string> CreateValidationErrorList(Gadget gadget)
        {
            var invalidFieldsList = new List<string>();

            if (gadget.Name.Length <= 3)
            {
                invalidFieldsList.Add("Länge des Gerätenamens");
            }

            if (string.IsNullOrEmpty(gadget.IpAddress))
            {
                invalidFieldsList.Add("IP Adresse ist erforderlich");
            }

            return invalidFieldsList;
        }
    }
}
