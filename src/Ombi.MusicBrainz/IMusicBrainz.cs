using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Ombi.MusicBrainz
{
    public interface IMusicBrainz
    {
        Task<Album> GetAlbumInformation(string albumId);

        Task<List<AlbumSearchResult>> SearchAlbum(string query);
    }
}