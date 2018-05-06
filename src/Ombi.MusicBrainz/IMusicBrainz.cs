using System.Collections.Generic;
using System.Threading.Tasks;
using Ombi.MusicBrainz.Json;
using System;

namespace Ombi.MusicBrainz
{
    public interface IMusicBrainz
    {
        Task<ArtistSearchResultsDto> SearchArtists(string query);

        Task<ArtistEntityDto> AlbumsForArtist(string artistID);

        Task<AlbumDto> GetAlbumInformation(string albumId);

        Task<AlbumSearchResultDto> SearchAlbum(string query);
    }
}