export interface ISearchMusicResult {
    artistID: string;
    name: string;
    image: string;
}

export interface IAlbumResult {

}

export interface ISearchMusicResultContainer {
    artists: ISearchMusicResult[];
}