using System;
using Newtonsoft.Json;

namespace Ombi.MusicBrainz.Json
{
    public class Track
    {
        [JsonProperty("id")]
        public string TrackId { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("position")]
        public string position { get; set; }
    }
}