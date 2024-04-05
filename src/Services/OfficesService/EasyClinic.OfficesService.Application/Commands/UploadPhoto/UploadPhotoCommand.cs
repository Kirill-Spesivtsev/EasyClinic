using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EasyClinic.OfficesService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EasyClinic.OfficesService.Application.Commands
{
    /// <summary>
    /// Command to upload photo to the server
    /// </summary>
    public record UploadPhotoCommand : IRequest<string>
    {
        /// <summary>
        /// File to be uploaded
        /// </summary>
        public IFormFile File { get; set; } = default!;
    }

    /// <summary>
    /// Handler for <see cref="UploadPhotoCommand"/>
    /// </summary>
    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, string>
    {
        private readonly IConfiguration _configuration;

        public UploadPhotoCommandHandler(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Uploads photo to Azure Blob Storage.
        /// </summary>
        /// <remarks>
        /// Creates Azure blob storage container if it doesn't exist.
        /// Uploads photo to Azure Blob Storage and returns its full path.
        /// If no file is provided, throws <see cref="BadRequestException"/>.
        /// </remarks>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Image path string</returns>
        /// <exception cref="BadRequestException">
        /// Thrown if no file was provided
        /// </exception>
        public async Task<string> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var blobContainerClient = new BlobContainerClient(
                _configuration["ConnectionStrings-OfficesServiceAzureStorageConnection"],
                "api-image-storage");

            var createResponse = await blobContainerClient.CreateIfNotExistsAsync();

            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            if (request.File.Length > 0)
            {
                var azureResponse = new List<Azure.Response<BlobContentInfo>>();

                string fileName = request.File.FileName;
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new BadRequestException("No file provided");
                }
                string filePath = Path.Combine("Images", "OfficesService", "ProfilePictures", fileName);

                using var memoryStream = new MemoryStream();
                
                request.File.CopyTo(memoryStream);
                memoryStream.Position = 0;
                var client = await blobContainerClient.UploadBlobAsync(filePath, memoryStream, default);
                azureResponse.Add(client);
                
                return filePath;
            }
            else
            {
                throw new BadRequestException("Empty file provided");
            }
        }
    }
}
