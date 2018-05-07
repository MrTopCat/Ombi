using System;
using Newtonsoft.Json;

namespace Ombi.MusicBrainz.Json
{
        public class ArtistEntityDto
        {
            [JsonProperty("id")]
            public string ArtistID { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("disambiguation")]
            public string Disambiguation { get; set; }

            [JsonProperty("score")]
            public int Score { get; set; }

            [JsonProperty("release-groups")]
            public AlbumDto[] Albums { get; set; }
        }
}