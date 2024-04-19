using Business.Interface;
using Business.Repository;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using Web.Response;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RickAndMortyController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IRepository<Episode> _episodeRepository;
        private readonly IRepository<Character> _chacterRepository;

        public RickAndMortyController(IHttpClientFactory clientFactory, IRepository<Episode> episodeRepository, IRepository<Character> characterRepository)
        {
            _clientFactory = clientFactory;
            _episodeRepository = episodeRepository;
            _chacterRepository = characterRepository;
        }

        [HttpGet("episodes")]
        [ApiKey]
        public async Task<IActionResult> GetEpisodes()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://rickandmortyapi.com/api/episode");

            if (response.IsSuccessStatusCode)
            {
                var episodesData = await response.Content.ReadAsStringAsync();

                var episodes = JsonConvert.DeserializeObject<EpisodesResponse>(episodesData);

                var epidoseList = MapToEpisodes(episodes!.Results);

                // Burada episodes listesini veritabanına kaydet
                foreach (var episode in epidoseList)
                {
                   await _episodeRepository.AddAsync(episode);
                }
                return Ok(episodes);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpGet("characters")]
        [ApiKey]
        public async Task<IActionResult> GetCharacters()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://rickandmortyapi.com/api/character");

            if (response.IsSuccessStatusCode)
            {
                var charactersData = await response.Content.ReadAsStringAsync();
                var characters = JsonConvert.DeserializeObject<List<Character>>(charactersData);

                // Burada episodes listesini veritabanına kaydet
                foreach (var character in characters)
                {
                   await _chacterRepository.AddAsync(character);
                }
                return Ok(characters);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }


        [HttpGet("episodes/{character?}/{property?}")]
        [ApiKey]
        public async Task<IActionResult> GetSearchEpisodes([FromRoute] string character = null, [FromRoute] string property = null)
        {
            var cachedEpisodes =await _episodeRepository.GetEpisodesBySearchCriteria(character, property);

            if (cachedEpisodes.Any())
            {
                return Ok(cachedEpisodes);
            }
            return Ok("Kayıt bulunamadı.");
        }

        [HttpGet("characters/{character?}/{property?}")]
        [ApiKey]
        public async Task<IActionResult> GetCharacters([FromRoute] string character = null, [FromRoute] string property = null)
        {
            var cachedCharacter = await _chacterRepository.GetCharactersBySearchCriteria(character, property);

            if (cachedCharacter.Any())
            {
                return Ok(cachedCharacter);
            }
            return Ok("Kayıt bulunamadı.");
        }



        public static List<Episode> MapToEpisodes(List<EpisodeResponse> responses)
        {
            if (responses == null)
            {
                return new List<Episode>();
            }

            return responses.Select(response => new Episode(
                id: response.Id,
                name: response.Name,
                airDate: response.AirDate, 
                episodeCode: response.Episode,
                characters: (List<Uri>)response.Characters.Select(url => new Uri(url)),
                url: new Uri(response.Url),
                created: response.Created
            )).ToList();
        }
    }
}

