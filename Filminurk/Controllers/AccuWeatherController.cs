using System.Xml;
using Filminurk.Core.Dto.AccuWeatherDTOs;
using Filminurk.Core.ServiceInterface;
using Filminurk.Models.AccuWeather;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class AccuWeatherController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;
        public AccuWeatherController(IWeatherForecastServices weatherForecastServices)
        {
            _weatherForecastServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindWeather(AccuWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "AccuWeather", new { city = model.CityName });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult City(string city)
        {
            AccuLocationWeatherResultDTO dto = new();
            dto.CityName = city;
            _weatherForecastServices.AccuWeatherResult(dto);
            AccuWeatherViewModel vm = new();
            vm.EffectiveDate = dto.EffectiveDate;
            vm.EffectiveEpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.Text = dto.Text;
            vm.Category = dto.Category;
            vm.EndEpochDate = dto.EndEpochDate;
            vm.EndDate = dto.EndDate;
            vm.DailyForecastsEpochDate = dto.DailyForecastsEpochDate;
            vm.DailyForecastsDate = dto.DailyForecastsDate;
            vm.TempMinValue = dto.TempMinValue;
            vm.TempMinUnit = dto.TempMinUnit;
            vm.TempMinUnitType = dto.TempMinUnitType;
            vm.TempMaxValue = dto.TempMaxValue;
            vm.TempMaxUnit = dto.TempMaxUnit;
            vm.TempMaxUnitType = dto.TempMaxUnitType;
            vm.DayIcon = dto.DayIcon;
            vm.DayIconPhrase = dto.DayIconPhrase;
            vm.DayHasPrecipication = dto.DayHasPrecipication;
            vm.DayPrecipicationType = dto.DayPrecipicationType;
            vm.DayPrecipicationIntensity = dto.DayPrecipicationIntensity;
            vm.NightIcon = dto.NightIcon;
            vm.NightIconPhrase = dto.NightIconPhrase;
            vm.NightHasPrecipication = dto.NightHasPrecipication;
            vm.NightPrecipicationType = dto.NightPrecipicationType;
            vm.NightPrecipicationIntensity = dto.NightPrecipicationIntensity;

            vm.MobileLink = dto.MobileLink;
            vm.Link = dto.Link;

            return View(vm);
        }
    }
}
