using infrastructure.interfaces;
using infrastructure.Models;


namespace infrastructure.Services
{
    public class StartupService: IStartupService
    {
        private readonly PhotosService _photosService;

        public StartupService(PhotosService photosService)
        {
            _photosService = photosService;
        }

        public async Task Initialize()
        {
            List<Photo> photos = await _photosService.GetPhotos();
            _photosService.SavePhotosToCache(photos);
        }
    }
}
