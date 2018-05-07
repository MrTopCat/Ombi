using System.Collections.Generic;
using Newtonsoft.Json;
using Ombi.Api.FanartTv.Models;

public class ArtistResult
{
    [JsonProperty("mbid_id")]
    public string MusicBrainzID { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("albums")]
    public IDictionary<string, AlbumResult> Albums { get; set; }

    [JsonProperty("hdmusiclogo")]
    public Content[] HighDefenitionMusicLogos { get; set; }

    [JsonProperty("artistthumb")]
    public Content[] ArtistThumbnails { get; set; }

    [JsonProperty("musiclogo")]
    public Content[] MusicLogos { get; set; }

    [JsonProperty("musicbanner")]
    public Content[] MusicBanners { get; set; }

    [JsonProperty("artistbackground")]
    public Content[] ArtistBackgrounds { get; set; }
}