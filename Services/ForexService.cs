using Newtonsoft.Json;

namespace UsersApp.Services
{
    public class ForexService
    {
        private readonly HttpClient _httpClient;
        private static List<ForexRate> _cachedRates;
        private static DateTime _lastFetch;
        private static readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public ForexService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ForexRate>> GetExchangeRatesAsync()
        {
            if (_cachedRates != null && DateTime.Now - _lastFetch < _cacheDuration)
            {
                return _cachedRates;
            }

            string url = "https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var forexResponse = JsonConvert.DeserializeObject<ForexApiResponse>(jsonResponse);
                _cachedRates = forexResponse?.Data;
                _lastFetch = DateTime.Now;
                return _cachedRates;
            }
            throw new HttpRequestException("Unable to fetch exchange rates.");
        }

        public async Task<ForexRate> GetMYRRateAsync()
        {
            var rates = await GetExchangeRatesAsync();
            return rates?.FirstOrDefault(r => r.Currency == "MYR");
        }
    }
}