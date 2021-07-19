using ApiDownloadedFileManager.Entities;
using ApiDownloadedFileManager.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiDownloadedFileManager.Repositories
{
    public interface IFileRepository : IDisposable
    {
        Task<List<File>> GetFile(int page, int qty);
        Task<File> GetFile(Guid id);
        Task<List<File>> GetFile(string FileName, int fileType);
        Task Insert(File file);
        Task Refresh(File file);
        Task Remove(Guid id);
    }
}
