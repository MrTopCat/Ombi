using System;
using Ombi.Core.Models.Search;
using Ombi.Store.Entities;

public class SearchMusicViewModel : SearchViewModel
{
    public string ArtistID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; } = "Placeholder for artist description";

    public string Image { get; set; }

    public string BackgroundImage { get; set; }

    public string Disambiguation { get; set; }

    public AlbumViewModel[] Albums { get; set; }

    public override RequestType Type => RequestType.Music;

    public class AlbumViewModel
    {
        public string AlbumID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AlbumArt { get; set; }

    }
}