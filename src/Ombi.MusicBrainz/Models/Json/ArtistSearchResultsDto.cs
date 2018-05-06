using System;
using Newtonsoft.Json;

namespace Ombi.MusicBrainz.Json
{
    public class ArtistSearchResultsDto
    {
        [JsonProperty("artists")]
        public ArtistEntityDto[] Artists { get; set; }

        public ArtistEntityDto this[int key]
        {
            get
            {
                return this.Artists[key];
            }
            set
            {
                this.Artists[key] = value;
            }
        }
    }
}