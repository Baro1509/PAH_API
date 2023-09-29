using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase {
        private readonly IConfiguration _configuration;
        public ImageController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file) {
            if (file == null) {
                return BadRequest();
            }

            // Get any Stream - it can be FileStream, MemoryStream or any other type of Stream
            var stream = file.OpenReadStream();

            //authentication
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_configuration["Firebase:ApiKey"]));
            var a = await auth.SignInWithEmailAndPasswordAsync(_configuration["Firebase:AuthEmail"], _configuration["Firebase:AuthPassword"]);

            // Constructr FirebaseStorage, path to where you want to upload the file and Put it there
            var task = new FirebaseStorage(
                _configuration["Firebase:Bucket"],

                 new FirebaseStorageOptions {
                     AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                     ThrowOnCancel = true,
                 })
                .Child("img")
                .Child("avt")
                .Child(file.FileName)
                .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // await the task to wait until upload completes and get the download url
            var downloadUrl = await task;
            return Ok(new { downloadUrl });
        }
    }
}
