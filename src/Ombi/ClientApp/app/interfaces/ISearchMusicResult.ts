export interface ISearchMusicResult {
    artistID: string;
    name: string;
    image: string;
    description: string;
    disambiguation: string;
    albums: IAlbumResult[];

    backgroundImage: any;
}

export interface IAlbumResult {
    albumID: string;
    title: string;
    description: string;
    albumArt: string;
}

export interface ISearchMusicResultContainer {
    artists: ISearchMusicResult[];
}