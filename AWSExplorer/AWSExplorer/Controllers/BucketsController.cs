using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWSExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;

        public BucketsController(IAmazonS3 amazonS3)
        {
                _s3Client = amazonS3;
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateBucket(string bucketName)
        //{
        //    var bucketexists = await _s3Client.DoesS3BucketExistAsync(bucketName);
        //    if(bucketexists)
        //    {
        //        return BadRequest($"Bucket {bucketName} already exists.");
        //    }
        //    await _s3Client.PutBucketAsync(bucketName);
        //    return Ok
        //}
    }
}
