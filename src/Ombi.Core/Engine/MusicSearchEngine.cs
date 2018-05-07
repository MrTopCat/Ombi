using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ombi.Core.Engine.Interfaces;
using Ombi.MusicBrainz;
using Ombi.MusicBrainz.Json;

public class MusicSearchEngine : IMusicSearchEngine
{
    public MusicSearchEngine(IMusicBrainzApi musicBrainzApi, IMapper mapper)
    {
        this.MusicBrainzApi = musicBrainzApi;
        this.Mapper = mapper;
    }

    private IMusicBrainzApi MusicBrainzApi { get; }
    private IMapper Mapper { get; }

    public async Task<IEnumerable<SearchMusicViewModel>> Search(string searchTerm)
    {
        var results = await MusicBrainzApi.SearchArtists(searchTerm);

        return await Transform(results, true);
    }

    public async Task<SearchMusicViewModel> GetArtistAlbums(string artistID)
    {
        return Mapper.Map<SearchMusicViewModel>(await MusicBrainzApi.GetAlbumInformation(artistID));
    }

    private async Task<IEnumerable<SearchMusicViewModel>> Transform(ArtistSearchResultsDto artistResults, bool includeAlbums = false)
    {
        List<SearchMusicViewModel> results = new List<SearchMusicViewModel>();
        var matching = Array.FindAll(artistResults.Artists, x => x.Score >= 90);

        Array.ForEach(matching, artist => results.Add(Mapper.Map<SearchMusicViewModel>(artist)));

        if (includeAlbums)
        {
            foreach (var model in results)
            {
                model.Albums = (await GetArtistAlbums(model.ArtistID)).Albums;
            }
        }

        return results;
    }
}