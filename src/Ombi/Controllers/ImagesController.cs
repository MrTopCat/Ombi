using Microsoft.AspNetCore.Mvc;
using Ombi.Api.FanartTv;
using Ombi.Store.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Ombi.Api.TheMovieDb;
using Ombi.Config;
using Ombi.Helpers;
using CSharpx;
using static SearchMusicViewModel;

namespace Ombi.Controllers
{
    [ApiV1]
    [Produces("application/json")]
    public class ImagesController : Controller
    {
        public ImagesController(IFanartTvApi fanartTvApi, IApplicationConfigRepository config,
            IOptions<LandingPageBackground> options, ICacheService c)
        {
            FanartTvApi = fanartTvApi;
            Config = config;
            Options = options.Value;
            _cache = c;
        }

        private IFanartTvApi FanartTvApi { get; }
        private IApplicationConfigRepository Config { get; }
        private LandingPageBackground Options { get; }
        private readonly ICacheService _cache;

        [HttpGet("images/artist/{mbid}")]
        public async Task<SearchMusicViewModel> GetArtistImages(string mbid)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetArtistImages(mbid, key.Value);

            var viewModel = new SearchMusicViewModel();

            if (images == null)
            {
                return null;
            }

            if (images.ArtistThumbnails != null && images.ArtistThumbnails.Length > 0)
            {
                viewModel.Image = images.ArtistThumbnails.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
            }

            if (images.ArtistBackgrounds != null && images.ArtistBackgrounds.Length > 0)
            {
                viewModel.BackgroundImage = images.ArtistBackgrounds.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
            }

            if (images.Albums.Values.Count > 0)
            {
                var albums = new List<AlbumViewModel>();

                foreach (var kvp in images.Albums.Where(x => x.Value.AlbumCovers != null))
                {
                    albums.Add(new AlbumViewModel()
                    {
                        AlbumID = kvp.Key,
                        AlbumArt = kvp.Value.AlbumCovers.OrderByDescending(x => x.Likes).Select(x => x.Url).FirstOrDefault(),
                    });
                }

                viewModel.Albums = albums.ToArray();
            }

            return viewModel;
        }

        [HttpGet("poster/artist/{mbid}")]
        public async Task<string> GetArtistPoster(string mbid)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetArtistImages(mbid, key.Value);

            if (images == null)
            {
                return string.Empty;
            }

            if (images.ArtistThumbnails != null && images.ArtistThumbnails.Length > 0)
            {
                var image = images.ArtistThumbnails.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                return image == null ? string.Empty : image;
            }

            return string.Empty;
        }

        [HttpGet("background/artist/{mbid}")]
        public async Task<string> GetArtistBackground(string mbid)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetArtistImages(mbid, key.Value);

            if (images == null)
            {
                return string.Empty;
            }

            if (images.ArtistBackgrounds != null && images.ArtistBackgrounds.Length > 0)
            {
                var image = images.ArtistBackgrounds.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                return image == null ? string.Empty : image;
            }

            return string.Empty;
        }

        [HttpGet("tv/{tvdbid}")]
        public async Task<string> GetTvBanner(int tvdbid)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetTvImages(tvdbid, key.Value);
            if (images == null)
            {
                return string.Empty;
            }
            if (images.tvbanner != null)
            {
                var enImage = images.tvbanner.Where(x => x.lang == "en").OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                if (enImage == null)
                {
                    return images.tvbanner.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                }
            }
            if (images.seasonposter != null)
            {
                return images.seasonposter.FirstOrDefault()?.url ?? string.Empty;
            }
            return string.Empty;
        }

        [HttpGet("poster/movie/{movieDbId}")]
        public async Task<string> GetMoviePoster(string movieDbId)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetMovieImages(movieDbId, key.Value);

            if (images == null)
            {
                return string.Empty;
            }

            if (images.movieposter?.Any() ?? false)
            {
                var enImage = images.movieposter.Where(x => x.lang == "en").OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                if (enImage == null)
                {
                    return images.movieposter.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                }
                return enImage;
            }

            if (images.moviethumb?.Any() ?? false)
            {
                return images.moviethumb.OrderBy(x => x.likes).Select(x => x.url).FirstOrDefault();
            }

            return string.Empty;
        }

        [HttpGet("poster/tv/{tvdbid}")]
        public async Task<string> GetTvPoster(int tvdbid)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetTvImages(tvdbid, key.Value);

            if (images == null)
            {
                return string.Empty;
            }

            if (images.tvposter?.Any() ?? false)
            {
                var enImage = images.tvposter.Where(x => x.lang == "en").OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                if (enImage == null)
                {
                    return images.tvposter.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                }
                return enImage;
            }

            if (images.tvthumb?.Any() ?? false)
            {
                return images.tvthumb.OrderBy(x => x.likes).Select(x => x.url).FirstOrDefault();
            }

            return string.Empty;
        }

        [HttpGet("background/movie/{movieDbId}")]
        public async Task<string> GetMovieBackground(string movieDbId)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetMovieImages(movieDbId, key.Value);

            if (images == null)
            {
                return string.Empty;
            }

            if (images.moviebackground?.Any() ?? false)
            {
                var enImage = images.moviebackground.Where(x => x.lang == "en").OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                if (enImage == null)
                {
                    return images.moviebackground.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                }
                return enImage;
            }

            return string.Empty;
        }

        [HttpGet("background/tv/{tvdbid}")]
        public async Task<string> GetTvBackground(int tvdbid)
        {
            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            var images = await FanartTvApi.GetTvImages(tvdbid, key.Value);

            if (images == null)
            {
                return string.Empty;
            }

            if (images.showbackground?.Any() ?? false)
            {
                var enImage = images.showbackground.Where(x => x.lang == "en").OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                if (enImage == null)
                {
                    return images.showbackground.OrderByDescending(x => x.likes).Select(x => x.url).FirstOrDefault();
                }
                return enImage;
            }

            return string.Empty;
        }

        [HttpGet("background")]
        public async Task<object> GetBackgroundImage()
        {
            var moviesArray = Options.Movies;
            var tvArray = Options.TvShows;

            var rand = new Random();
            var movieUrl = string.Empty;
            var tvUrl = string.Empty;

            var key = await _cache.GetOrAdd(CacheKeys.FanartTv, async () => await Config.Get(Store.Entities.ConfigurationTypes.FanartTv), DateTime.Now.AddDays(1));

            if (moviesArray.Any())
            {
                var item = rand.Next(moviesArray.Length);
                var result = await FanartTvApi.GetMovieImages(moviesArray[item].ToString(), key.Value);

                while (!result.moviebackground.Any())
                {
                    result = await FanartTvApi.GetMovieImages(moviesArray[item].ToString(), key.Value);
                }

                movieUrl = result.moviebackground[0].url;
            }
            if (tvArray.Any())
            {
                var item = rand.Next(tvArray.Length);
                var result = await FanartTvApi.GetTvImages(tvArray[item], key.Value);

                while (!result.showbackground.Any())
                {
                    result = await FanartTvApi.GetTvImages(tvArray[item], key.Value);
                }

                tvUrl = result.showbackground[0].url;
            }

            if (!string.IsNullOrEmpty(movieUrl) && !string.IsNullOrEmpty(tvUrl))
            {
                var result = rand.Next(2);
                if (result == 0) return new { url = movieUrl };
                if (result == 1) return new { url = tvUrl };
            }

            if (!string.IsNullOrEmpty(movieUrl))
            {
                return new { url = movieUrl };
            }
            return new { url = tvUrl };
        }
    }
}
