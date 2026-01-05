using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto.AccuWeatherDTOs
{
    public class AccuLocationWeatherResultDTO
    {
        public string CityName { get; set; } = string.Empty;

        public string CityCode { get; set; } = string.Empty;

        public string EffectiveDate { get; set; } = string.Empty;

        public long EffectiveEpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Category {  get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public long EndEpochDate { get; set; }
        public string DailyForecastsDate { get; set; } = string.Empty;
        public int DailyForecastsEpochDate { get; set; }
        public double TempMinValue { get; set; }
        public string TempMinUnit { get; set; } = string.Empty;
        public int TempMinUnitType { get; set; }
        public double TempMaxValue { get; set; }
        public string TempMaxUnit { get; set; } = string.Empty;
        public int TempMaxUnitType { get; set; }

        public int DayIcon {  get; set; }
        public string DayIconPhrase {  get; set; } = string.Empty;
        public bool DayHasPrecipication { get; set; }
        public string DayPrecipicationType {  get; set; } = string.Empty;
        public string DayPrecipicationIntensity { get; set; } = string.Empty;

        public int NightIcon { get; set; }
        public string NightIconPhrase { get; set; } = string.Empty;
        public bool NightHasPrecipication { get; set; }
        public string NightPrecipicationType { get; set; } = string.Empty;
        public string NightPrecipicationIntensity { get; set; } = string.Empty;
        public string MobileLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;

    }
}
