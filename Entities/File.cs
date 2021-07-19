using ApiDownloadedFileManager.ViewModel.Enum;
using System;

namespace ApiDownloadedFileManager.Entities
{
    public class File
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public string GenreType { get; set; }
        public bool Purchased { get; set; }
    }
}
