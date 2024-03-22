using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EasyClinic.OfficesService.Application.Commands.CreateOffice;
using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace EasyClinic.OfficesService.Application.Commands.UploadPhoto
{
    public record UploadPhotoCommand : IRequest<string>
    {
        public IFormFile File { get; set; } = default!;
    }

    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, string>
    {
        private readonly IConfiguration _configuration;

        public UploadPhotoCommandHandler(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
