using Newtonsoft.Json;
using Ombi.Api.FanartTv.Models;

public class AlbumResult
{
    [JsonProperty("albumcover")]
    public AlbumCoverResult[] AlbumCovers { get; set; }

    public class AlbumCoverResult
    {
        [JsonProperty("id")]
        public string AlbumCoverID { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("likes")]
        public int Likes { get; set; }
    }

    public class CdArtResult
    {
    }
}