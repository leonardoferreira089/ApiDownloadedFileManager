using ApiDownloadedFileManager.InputModel;
using ApiDownloadedFileManager.ViewModel;
using ApiDownloadedFileManager.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDownloadedFileManager.Services
{
    public interface IFileService : IDisposable
    {
        Task<List<FileViewModel>> GetFile(int page, int qty);
        Task<FileViewModel> GetFile(Guid id);
        Task<FileViewModel> Insert(FileInputModel file);
        Task Refresh(Guid id, FileInputModel file);
        Task Refresh(Guid id, FileType fileType);
        Task Remove(Guid id);
    }
}
