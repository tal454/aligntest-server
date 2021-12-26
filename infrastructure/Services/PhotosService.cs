

using infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace infrastructure.Services
{
    public class PhotosService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        public PhotosService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
        }


        public async Task<List<Photo>> GetPhotos()
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("Picsum");
                List<Photo> photos = JsonConvert.DeserializeObject<List<Photo>>(await httpClient.GetStringAsync("v2/list?page=1&limit=100")) ?? new List<Photo>();
                foreach (var photo in photos)
                {
                    var image = await httpClient.GetAsync(photo.download_url);
                    photo.image = await image.Content.ReadAsByteArrayAsync();
                }
                return photos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SavePhotosToCache(List<Photo> photos)
        {
            try
            {
                _memoryCache.Set("photos", photos);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Photo> GetPhotosFromCahce()
        {
            try
            {
                List<Photo> photos = _memoryCache.Get<List<Photo>>("photos");
                return photos;
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
