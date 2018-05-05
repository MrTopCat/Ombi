using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace Ombi.MusicBrainz.Json
{
    public class AlbumSearchResultDto
    {
        [JsonProperty("release-groups")]
        public AlbumSearchResultEntity[] Albums { get; set; }

        public class AlbumSearchResultEntity
        {
            [JsonProperty("id")]
            public string AlbumId { get; set; }

            [JsonProperty("artist-credit")]
            public AlbumSearchResultArtist Artists { get; set; }
        }

        public class AlbumSearchResultArtist
        {
            [JsonProperty("id")]
            public string ArtistId { get; set; }            
        }
    }

}