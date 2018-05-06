using AutoMapper;
using Ombi.Core.Models.Search;
using Ombi.MusicBrainz.Json;

namespace Ombi.Mapping.Profiles
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<ArtistEntityDto, SearchMusicViewModel>();
        }
    }
}