using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Ombi.Api;
using System.Net.Http;

namespace Ombi.MusicBrainz
{
    public class MusicBrainzApi : IMusicBrainz
    {
        private static readonly string baseUri = "http://musicbrainz.org/ws/2";

        public IApi Api { get; }

        public MusicBrainzApi(IApi api)
        {
            this.Api = api;
        }

        public async Task<Album> GetAlbumInformation(string albumId)
        {
            throw new NotImplementedException();
        }

        public async Task<AlbumSearchResult> SearchAlbum(string query)
        {
            Request request = new Request($"/release", baseUri, HttpMethod.Get);
            request.FullUri = request.FullUri.AddQueryParameter("query", query);
            request.FullUri = request.FullUri.AddQueryParameter("recordings", query);
            request.FullUri = request.FullUri.AddQueryParameter("fmt", query);

            return await Api.Request<AlbumSearchResult>(request);
        }
    }
}