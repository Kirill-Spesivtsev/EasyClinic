using EasyClinic.OfficesService.Application.Commands.CreateOffice;
using EasyClinic.OfficesService.Domain.Entities;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace EasyClinic.OfficesService.Application.Commands.UploadPhoto
{
    public record UploadPhotoCommand : IRequest<string>
    {
        public IFormFile File { get; set; } = default!;
    }

    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, string>
    {
        private readonly IRepository<Office> _officesRepository;

        public UploadPhotoCommandHandler(IRepository<Office> officesRepository)
        {
            _officesRepository = officesRepository;
        }

        public async Task<string> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition!)?.FileName?.Trim('"');
                if (fileName == null)
                {
                    throw new BadRequestException("No file name provided");
                }

                var fullPath = Path.Combine(pathToSave, fileName!);
                var storedPath = Path.Combine(folderName, fileName!);
                using var stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);
                
                return await Task.FromResult(storedPath);
            }
            else
            {
                throw new BadRequestException("Empty file provided");
            }
        }
    }
}
