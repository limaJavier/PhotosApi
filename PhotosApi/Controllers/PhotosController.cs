using Microsoft.AspNetCore.Mvc;
using PhotosApi.Models.PhotoModel;
using PhotosApi.Models.PhotoFile;
using Microsoft.AspNetCore.Authorization;

namespace PhotosApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IPhotoModelRepository _photoModelRepository { get; set; }
        private IPhotoFileRepository _photoFileRepository { get; set; }
        public PhotosController(IPhotoModelRepository photoModelRepository, IPhotoFileRepository photoFileRepository)
        {
            _photoModelRepository = photoModelRepository;
            _photoFileRepository = photoFileRepository;
        }

        [AllowAnonymous]
        // GET: api/Photos
        [HttpGet(Name = "GetAllPhotos")]
        public ActionResult<IEnumerable<Photo>> GetAllPhotos() => Ok(_photoModelRepository.GetAllPhotos());

        [AllowAnonymous]
        // GET api/Photos/GetPhoto/5
        [HttpGet("{id:int}", Name = "GetPhoto")]
        public ActionResult<Photo> GetPhoto(int id)
        {
            var result = _photoModelRepository.GetPhoto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [AllowAnonymous]
        //GET api/Photos/Download/5
        [HttpGet("Download/{id:int}", Name = "DownloadPhoto")]
        public IActionResult DownloadPhoto(int id)
        {
            var photo = _photoModelRepository.GetPhoto(id);
            if (photo == null)
                return NotFound();

            if (!_photoFileRepository.DownloadPhoto(photo.Url, out byte[] bytes))
                return StatusCode(StatusCodes.Status500InternalServerError);

            return File(bytes, photo.ContentType);
        }

        // POST api/Photos
        [HttpPost(Name = "StorePhoto")]
        public IActionResult StorePhoto([FromForm] PhotoFile photoFile)
        {
            if (!_photoFileRepository.UploadPhoto(photoFile, out string url))
                return StatusCode(StatusCodes.Status500InternalServerError);
            if (!_photoModelRepository.CreatePhoto(new Photo() { Description = photoFile.Description, Title = photoFile.Title, Album = photoFile.Album, Url = url, ContentType = photoFile.FormFile.ContentType }))
                return StatusCode(StatusCodes.Status500InternalServerError);
            return NoContent();
        }

        // PUT api/Photos/5
        [HttpPut("{id:int}", Name = "ChangePhoto")]
        public IActionResult ChangePhoto(int id, [FromForm] PhotoFile photoFile)
        {
            if (_photoModelRepository.GetPhoto(id) == null)
                return NotFound();
            if (!_photoModelRepository.UpdatePhoto(id, new Photo { Description = photoFile.Description, Title = photoFile.Title, Album = photoFile.Album }))
                return StatusCode(StatusCodes.Status500InternalServerError);
            return NoContent();
        }

        // DELETE api/Photos/5
        [HttpDelete("{id:int}", Name = "DeletePhoto")]
        public IActionResult DeletePhoto(int id)
        {
            if (!_photoModelRepository.DeletePhoto(id, out string url))
                return NotFound();
            if (!_photoFileRepository.DeletePhoto(url))
                return StatusCode(StatusCodes.Status500InternalServerError);
            return NoContent();
        }
    }
}
