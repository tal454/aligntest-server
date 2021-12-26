

using infrastructure.Models;

using infrastructure.Services;
using Microsoft.AspNetCore.Mvc;


namespace align_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private readonly PhotosService _photosService;
        private readonly ILogger<PhotosController> _logger;

        public PhotosController(PhotosService photosService, 
            ILogger<PhotosController> logger)
        {
            _photosService = photosService;
            _logger = logger;

        }

        [HttpGet]
        public ActionResult<Photo> GetRandomPhotos()
        {
            try
            {
                List<Photo> photos = _photosService.GetPhotosFromCahce();
                if (photos.Count == 0)
                    return NotFound();
                Random rnd = new Random();
                List<Photo> returnList = new List<Photo>();
                for (int i = 0; i < 5; i++)
                {
                    int index = rnd.Next(photos.Count);
                    returnList.Add(photos[index]);
                    photos.RemoveAt(index);
                }
                _photosService.SavePhotosToCache(photos);
                return Ok(returnList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + "/n" + ex.StackTrace);
                return BadRequest("Internal Error: " );
            }

        }
    }
}
