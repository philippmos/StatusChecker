using System.Collections.Generic;
using StatusChecker.I18N;
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
                invalidFieldsList.Add(AppTranslations.Page_NewGadget_Validation_Alert_GadgetName_Length);
            }

            if (string.IsNullOrEmpty(gadget.IpAddress))
            {
                invalidFieldsList.Add(AppTranslations.Page_NewGadget_Validation_Alert_GadgetIpAddress_Required);
            }

            return invalidFieldsList;
        }

        /// <summary>
        /// Checks if Setting is valid
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool IsSettingValid(Setting setting)
        {
            return setting != null && !string.IsNullOrEmpty(setting.Value);
        }
    }
}
