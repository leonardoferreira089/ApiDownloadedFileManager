using ApiDownloadedFileManager.ViewModel.Enum;
using System;

namespace ApiDownloadedFileManager.ViewModel
{
    public class FileViewModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int FileType { get; set; }
        public string GenreType { get; set; }
        public bool Purchased { get; set; }
    }
}
