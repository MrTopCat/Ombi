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
        
        return await Transform(results);
    }

    public async Task<IEnumerable<SearchMusicViewModel>> Transform(ArtistSearchResultsDto artistResults, bool includeAlbums = false)
    {
        List<SearchMusicViewModel> results = new List<SearchMusicViewModel>();

        Array.ForEach(artistResults.Artists, artist => results.Add(Mapper.Map<SearchMusicViewModel>(artist)));        

        if (includeAlbums)
        {
            // TODO this shit
        }

        return results;
    }
}