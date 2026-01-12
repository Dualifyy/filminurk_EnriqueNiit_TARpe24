using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Filminurk.Core.Dto.AccuWeatherDTOs;
using Filminurk.Core.ServiceInterface;

namespace Filminurk.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDTO> AccuWeatherResult(AccuLocationWeatherResultDTO dto)
        {
            string apikey = Filminurk.Data.Environment.accuweatherkey; //key tuleb environemtnist, ega pole hardcodetud
            var baseUrl = "https://dataservice.accuweather.com/forecasts/v1/daily/1day/";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://dataservice.accuweather.com/locations/v1/cities/search");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                    );
                var response =  httpClient.GetAsync($"?apikey={apikey}&q={dto.CityName}").GetAwaiter().GetResult(); // get awaiter get result remove await
                var jsonResponse = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;
                    dto.CityCode = root[0].GetProperty("Key").ToString();
                }

                    
            }



            string weatherResponse = baseUrl + $"{dto.CityCode}?apikey={apikey}&metric=true";

            using (var clientWeather = new HttpClient())
            {
                var httpResponseWeather = clientWeather.GetAsync(weatherResponse).GetAwaiter().GetResult();
                string jsonWeather = await httpResponseWeather.Content.ReadAsStringAsync();

                AccuLocationRootDTO weatherRootDTO = JsonSerializer.Deserialize<AccuLocationRootDTO>(jsonWeather);

                dto.EffectiveDate = weatherRootDTO.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDTO.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDTO.Headline.Severity;
                dto.Text = weatherRootDTO.Headline.Text;
                dto.Category = weatherRootDTO.Headline.Category;
                dto.EndDate = weatherRootDTO.Headline.EndDate;
                dto.EndEpochDate = weatherRootDTO.Headline.EndEpochDate;

                dto.MobileLink = weatherRootDTO.Headline.MobileLink;
                dto.Link = weatherRootDTO.Headline.Link;

                dto.DailyForecastsDate = weatherRootDTO.DailyForecasts[0].Date;
                dto.DailyForecastsEpochDate = weatherRootDTO.DailyForecasts[0].EpochDate;

                dto.TempMinValue = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.Unit;
                dto.TempMinUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Minimum.UnitType;

                dto.TempMaxValue = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Value;
                dto.TempMaxUnit = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.Unit;
                dto.TempMaxUnitType = weatherRootDTO.DailyForecasts[0].Temperature.Maximum.UnitType;

                dto.DayIcon = weatherRootDTO.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherRootDTO.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipication = weatherRootDTO.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayPrecipicationType = weatherRootDTO.DailyForecasts[0].Day.PrecipitationType;
                dto.DayPrecipicationIntensity = weatherRootDTO.DailyForecasts[0].Day.PrecipitationIntensity;

                dto.NightIcon = weatherRootDTO.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherRootDTO.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipication = weatherRootDTO.DailyForecasts[0].Night.HasPrecipitation;
                dto.NightPrecipicationType = weatherRootDTO.DailyForecasts[0].Night.PrecipitationType;
                dto.NightPrecipicationIntensity = weatherRootDTO.DailyForecasts[0].Night.PrecipitationIntensity;


            }
            return dto;

        }
    }
}
