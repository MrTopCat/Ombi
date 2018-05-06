using System;
using Newtonsoft.Json;

namespace Ombi.MusicBrainz.Json
{
    public class AlbumDto
    {
        [JsonProperty("id")]
        public string AlbumId { get; set; }

        [JsonProperty("first-release-date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}