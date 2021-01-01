using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.Models.Enums;
using StatusChecker.ViewModels.Gadgets;
using StatusChecker.Services.Interfaces;


namespace StatusChecker.Helper
{
    public static class GadgetHelper
    {
        /// <summary>
        /// Round the Temperature Value with 2 decimalplaces
        /// </summary>
        /// <param name="temperature"></param>
        /// <returns></returns>
        public static double RoundTemperature(double temperature)
        {
            return Math.Round(temperature, 2);
        }

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

        /// <summary>
        /// Order the Gadgets by SystemSetting
        /// </summary>
        /// <param name="unsortedGadgetList"></param>
        /// <returns></returns>
        public async static Task<List<GadgetViewModel>> SortGadgetListBySettingAsync(List<GadgetViewModel> unsortedGadgetList)
        {
            var settingService = DependencyService.Get<ISettingService>();
            var sortingSetting = await settingService.GetSettingValueAsync(SettingKeys.GadgetSortingType);
            Enum.TryParse(typeof(GadgetSortingTypes), sortingSetting, true, out var gadgetSortingType);

            switch (gadgetSortingType)
            {
                case GadgetSortingTypes.ByNameAsc:
                    return unsortedGadgetList.OrderBy(x => x.Name).ToList();

                case GadgetSortingTypes.ByNameDesc:
                    return unsortedGadgetList.OrderByDescending(x => x.Name).ToList();


                case GadgetSortingTypes.ByLocationAsc:
                    return unsortedGadgetList.OrderBy(x => x.Location).ToList();

                case GadgetSortingTypes.ByLocationDesc:
                    return unsortedGadgetList.OrderByDescending(x => x.Location).ToList();


                case GadgetSortingTypes.ByTemperatureAsc:
                    return unsortedGadgetList.OrderBy(x => x.Temperature).ToList();

                case GadgetSortingTypes.ByTemperatureDesc:
                    return unsortedGadgetList.OrderByDescending(x => x.Temperature).ToList();


                case GadgetSortingTypes.ByCreationDesc:
                    return unsortedGadgetList.OrderByDescending(x => x.Id).ToList();

                case GadgetSortingTypes.ByCreationAsc:
                default:
                    return unsortedGadgetList.OrderBy(x => x.Id).ToList();
            }
        }
    }
}
