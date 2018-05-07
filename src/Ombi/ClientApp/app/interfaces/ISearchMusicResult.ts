export interface ISearchMusicResult {
    artistID: string;
    name: string;
    image: string;
    backgroundImage: string;
    description: string;
    disambiguation: string;
}

export interface IAlbumResult {

}

export interface ISearchMusicResultContainer {
    artists: ISearchMusicResult[];
}