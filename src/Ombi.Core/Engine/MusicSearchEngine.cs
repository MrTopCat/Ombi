using System.Collections.Generic;
using System.Threading.Tasks;
using Ombi.Core.Engine.Interfaces;

public class MusicSearchEngine : IMusicSearchEngine
{
    public Task<IEnumerable<SearchMusicViewModel>> Search(string searchTerm)
    {
        throw new System.NotImplementedException();
    }
}