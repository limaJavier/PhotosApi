using Microsoft.AspNetCore.Mvc;
using PhotosApi.Models.PhotoModel;
using PhotosApi.Models.PhotoFile;

namespace PhotosApi.Controllers
{
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

        // GET: api/Photos
        [HttpGet(Name = "GetAllPhotos")]
        public ActionResult<IEnumerable<Photo>> GetAllPhotos() => Ok(_photoModelRepository.GetAllPhotos());

        // GET api/Photos/GetPhoto/5
        [HttpGet("{id:int}", Name = "GetPhoto")]
        public ActionResult<Photo> GetPhoto(int id)
        {
            var result = _photoModelRepository.GetPhoto(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        //GET api/Photos/Download/5
        [HttpGet("Download/{id:int}", Name = "DownloadPhoto")]
        public string DownloadPhoto(int id)
        {
            return "value";
        }

        // POST api/Photos
        [HttpPost(Name = "CreatePhoto")]
        public IActionResult StorePhoto([FromForm] PhotoFile photoFile)
        {
            if (!_photoFileRepository.UploadPhoto(photoFile, out string url))
                return StatusCode(StatusCodes.Status500InternalServerError);
            if (!_photoModelRepository.CreatePhoto(new Photo() { Description = photoFile.Description, Title = photoFile.Title, Album = photoFile.Album }, url))
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
