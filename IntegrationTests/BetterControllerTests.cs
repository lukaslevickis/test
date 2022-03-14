using FluentAssertions;
using HorseRacingBackend.Dtos;
using IntegrationTests.Factories;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class BetterControllerTests
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _httpClient;

        public BetterControllerTests()
        {
            _factory = new CustomWebApplicationFactory();
            _httpClient = _factory.CreateClient();
        }

        [Test]
        public async Task Get_SimpleGetCall_ReturnsAllResults()
        {
            var result = await _httpClient.GetAsync("/api/better");
            var dtos = await result.Content.ReadAsAsync<List<BetterDto>>();

            dtos.Count().Should().Be(0);
        }

        [Test]
        public async Task Create_givenDto_CreateTheResult()
        {
            var dto = new BetterDto
            {
                Bet = 4,
                Name = "Testing",
                Surname = "is fun"
            };

            var json = JsonConvert.SerializeObject(dto);

            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync("/api/better", stringContent);
            var created = await result.Content.ReadAsAsync<BetterDto>();

            created.Name.Should().Be(dto.Name);
            created.Surname.Should().Be(dto.Surname);

            await _httpClient.DeleteAsync($"/api/better/{created.Id}");
        }
    }
}
