using System;
using System.Collections.Generic;
using System.Linq;
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

        var matching = Array.FindAll(results.Artists, x => x.Score >= 90)
            .OrderByDescending(x => x.Score)
            .Take(10) // each item will require an api call, limit to 10
            .ToArray(); 

        return await Transform(matching, true);
    }

    public async Task<SearchMusicViewModel> GetArtistAlbums(string artistID)
    {
        return Mapper.Map<SearchMusicViewModel>(await MusicBrainzApi.AlbumsForArtist(artistID));
    }

    private async Task<IEnumerable<SearchMusicViewModel>> Transform(ArtistEntityDto[] artists, bool includeAlbums = false)
    {
        List<SearchMusicViewModel> results = new List<SearchMusicViewModel>();

        Array.ForEach(artists, artist => results.Add(Mapper.Map<SearchMusicViewModel>(artist)));

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