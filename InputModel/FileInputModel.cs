using ApiDownloadedFileManager.ViewModel.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApiDownloadedFileManager.InputModel
{
    public class FileInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do arquivo deve conter entre 3 e 100 caracteres")]
        public string FileName { get; set; }
        [Required]
        public int FileType{ get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do gênero do arquivo deve conter entre 3 e 100 caracteres")]
        public string GenreType { get; set; }
        public bool Purchased { get; set; }
    }
}
