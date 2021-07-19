using ApiDownloadedFileManager.Entities;
using ApiDownloadedFileManager.Exceptions;
using ApiDownloadedFileManager.InputModel;
using ApiDownloadedFileManager.Repositories;
using ApiDownloadedFileManager.ViewModel;
using ApiDownloadedFileManager.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDownloadedFileManager.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<List<FileViewModel>> GetFile(int page, int qty)
        {
            var files = await _fileRepository.GetFile(page, qty);

            return files.Select(file => new FileViewModel
            {
                Id = file.Id,
                FileName = file.FileName,
                FileType = (int)file.FileType,
                GenreType = file.GenreType,
                Purchased = file.Purchased
            })
                               .ToList();
        }

        public async Task<FileViewModel> GetFile(Guid id)
        {
            var file = await _fileRepository.GetFile(id);

            if (file == null)
                return null;

            return new FileViewModel
            {
                Id = file.Id,
                FileName = file.FileName,
                FileType = (int)file.FileType,
                GenreType = file.GenreType,
                Purchased = file.Purchased
            };
        }

        public async Task<FileViewModel> Insert(FileInputModel file)
        {
            var entityFile = await _fileRepository.GetFile(file.FileName, file.FileType);

            if (entityFile.Count > 0)
                throw new FileAlreadyRegisteredException();

            var fileInsert = new File
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                FileType = (FileType)file.FileType,
                GenreType = file.GenreType,
                Purchased = file.Purchased
            };

            await _fileRepository.Insert(fileInsert);

            return new FileViewModel
            {
                Id = fileInsert.Id,
                FileName = file.FileName,
                FileType = (int)file.FileType,
                GenreType = file.GenreType,
                Purchased = file.Purchased
            };
        }
        public async Task Refresh(Guid id, FileInputModel file)
        {
            var entityFile = await _fileRepository.GetFile(id);

            if (entityFile == null)
                throw new FileNotRegisteredException();

            entityFile.FileName = file.FileName;
            entityFile.FileType = (FileType)file.FileType;
            entityFile.GenreType = file.GenreType;
            entityFile.Purchased = file.Purchased;

            await _fileRepository.Refresh(entityFile);
        }

        public async Task Refresh(Guid id, FileType fileType)
        {
            var entityFile = await _fileRepository.GetFile(id);

            if (entityFile == null)
                throw new FileNotRegisteredException();

            entityFile.FileType = fileType;

            await _fileRepository.Refresh(entityFile);
        }

        public async Task Remove(Guid id)
        {
            var file = await _fileRepository.GetFile(id);

            if (file == null)
                throw new FileAlreadyRegisteredException();

            await _fileRepository.Remove(id);
        }

        public void Dispose()
        {
            _fileRepository?.Dispose();
        }
    }
}
