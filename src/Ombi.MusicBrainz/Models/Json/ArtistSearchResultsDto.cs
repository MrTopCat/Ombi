using System;
using Newtonsoft.Json;

namespace Ombi.MusicBrainz.Json
{
    public class ArtistSearchResultsDto
    {
        [JsonProperty("artists")]
        public ArtistEntityDto[] Artists { get; set; }
    }
}