using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace Ombi.MusicBrainz
{
    public class AlbumSearchResult
    {
        [JsonProperty("releases")]
        public AlbumSearchResultEntity[] Albums { get; set; }

        public class AlbumSearchResultEntity
        {
            [JsonProperty("id")]
            public string AlbumId { get; set; }
        }

    }

}