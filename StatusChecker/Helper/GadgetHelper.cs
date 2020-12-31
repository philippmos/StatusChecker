using StatusChecker.Models;
using StatusChecker.Models.Enums;

namespace StatusChecker.Helper
{
    public static class GadgetHelper
    {

        /// <summary>
        /// Returns the StatusIndicationColor for the current GadgetStatus
        /// </summary>
        /// <param name="gadgetStatus"></param>
        /// <returns></returns>
        public static StatusIndicatorColors GetStatusIndicatorColor(GadgetStatus gadgetStatus)
        {
            if (gadgetStatus == null) return StatusIndicatorColors.Black;

            if (gadgetStatus.overtemperature == false && gadgetStatus.temperature <= 90.00 && gadgetStatus.voltage <= 250.00)
            {
                return StatusIndicatorColors.Green;
            }

            return StatusIndicatorColors.Red;

        }
    }
}
