using Filminurk.Core.Dto.OMDbAPIDTOs;
using Filminurk.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    public class OMDbServices : IOMDbAPIServices
    {
        public async Task<OMDbAPIMovieSearchRootDTO> OMDbSearchResult(string Title)
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return null;
            }

            string apiKey = Data.Environment.omdbapikey;
            string requestUrl = $"http://www.omdbapi.com/?t={Uri.EscapeDataString(Title)}&apikey={Uri.EscapeDataString(apiKey)}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(requestUrl).ConfigureAwait(false);
            }
            catch
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(responseJson))
            {
                return null;
            }

            // Quick check for OMDb "Response":"False" (when movie not found or error)
            try
            {
                using var doc = JsonDocument.Parse(responseJson);
                if (doc.RootElement.TryGetProperty("Response", out var respProp) &&
                    respProp.GetString()?.Equals("False", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return null;
                }
            }
            catch
            {
                // fall through to attempt deserialization
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            try
            {
                var dto = JsonSerializer.Deserialize<OMDbAPIMovieSearchRootDTO>(responseJson, options);
                return dto;
            }
            catch
            {
                return null;
            }
        }
    }
}
