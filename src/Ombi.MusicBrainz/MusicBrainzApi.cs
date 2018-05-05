using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Ombi.Api;
using Ombi.MusicBrainz.Json;
using System.Net.Http;

namespace Ombi.MusicBrainz
{
    public class MusicBrainzApi : IMusicBrainz
    {
        private const string userAgent = "test app ( topcat567@gmail.com )";
        private const string baseUri = "http://musicbrainz.org/ws/2";

        public IApi Api { get; }

        public MusicBrainzApi(IApi api)
        {
            this.Api = api;
        }

        public async Task<AlbumDto> GetAlbumInformation(string albumId)
        {
            Request request = new Request($"/release/{albumId}", baseUri, HttpMethod.Get);
            request.AddHeader("User-Agent", userAgent);
            request.FullUri = request.FullUri.AddQueryParameter("inc", "recordings");
            request.FullUri = request.FullUri.AddQueryParameter("fmt", "json");

            return await Api.Request<AlbumDto>(request);
        }

        public async Task<AlbumSearchResultDto> SearchAlbum(string query)
        {
            Request request = new Request($"/release", baseUri, HttpMethod.Get);
            request.AddHeader("User-Agent", userAgent);
            request.FullUri = request.FullUri.AddQueryParameter("query", query);
            request.FullUri = request.FullUri.AddQueryParameter("inc", "recordings");
            request.FullUri = request.FullUri.AddQueryParameter("fmt", "json");

            return await Api.Request<AlbumSearchResultDto>(request);
        }

        public async Task<ArtistSearchResultsDto> SearchArtists(string query)
        {
            Request request = new Request("/artist", baseUri, HttpMethod.Get);
            request.AddHeader("User-Agent", userAgent);
            request.FullUri = request.FullUri.AddQueryParameter("query", query);
            request.FullUri = request.FullUri.AddQueryParameter("fmt", "json");

            return await Api.Request<ArtistSearchResultsDto>(request);
        }

        public async Task<ArtistSearchResultsDto> AlbumsForArtist(string artistID)
        {
            Request request = new Request($"/release/{artistID}", baseUri, HttpMethod.Get);
            request.AddHeader("User-Agent", userAgent);
            request.FullUri = request.FullUri.AddQueryParameter("inc", "albums");
            request.FullUri = request.FullUri.AddQueryParameter("fmt", "json");

            return await Api.Request<ArtistSearchResultsDto>(request);
        }
    }
}