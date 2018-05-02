using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Ombi.MusicBrainz
{
    public class MusicBrainzApi : IMusicBrainz
    {
        public Task<Album> GetAlbumInformation(string albumId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AlbumSearchResult>> SearchAlbum(string query)
        {
            throw new NotImplementedException();
        }
    }
}