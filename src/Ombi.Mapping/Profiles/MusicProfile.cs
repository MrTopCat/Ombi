using AutoMapper;
using Ombi.Core.Models.Search;
using Ombi.MusicBrainz.Json;
using static SearchMusicViewModel;

namespace Ombi.Mapping.Profiles
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<ArtistEntityDto, SearchMusicViewModel>();
            CreateMap<AlbumDto, AlbumViewModel>();
        }
    }
}