using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ombi.Core.Engine.Interfaces
{
    public interface IMusicSearchEngine
    {
        Task<IEnumerable<SearchMusicViewModel>> Search(string searchTerm);
    }
}