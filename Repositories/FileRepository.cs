using ApiDownloadedFileManager.Entities;
using ApiDownloadedFileManager.ViewModel.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiDownloadedFileManager.Repositories
{
    public class FileRepository : IFileRepository
    {
        private static Dictionary<Guid, File> files = new Dictionary<Guid, File>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new File{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), FileName = "Two and a half man", FileType = (FileType)2, GenreType = "Comédia", Purchased = true} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new File{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), FileName = "Mais esperto que o Diabo", FileType = (FileType)6, GenreType = "Auto-Ajuda", Purchased = false} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new File{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), FileName = "Red Hot Chili Peppers DVD", FileType = (FileType)3, GenreType = "Rock and Roll", Purchased = true} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new File{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), FileName = "The Roundup", FileType = (FileType)1, GenreType = "Ação e guerra", Purchased = true} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new File{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), FileName = "The Simpsons", FileType = (FileType)4, GenreType = "Comédia", Purchased = false} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new File{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), FileName = "Curso Asp.Net Core", FileType = (FileType)5, GenreType = "Tecnologia da informação", Purchased = false} }
        };

        public Task<List<File>> GetFile(int page, int qty)
        {
            return Task.FromResult(files.Values.Skip((page - 1) * qty).Take(qty).ToList());
        }

        public Task<File> GetFile(Guid id)
        {
            if (!files.ContainsKey(id))
                return Task.FromResult<File>(null);

            return Task.FromResult(files[id]);
        }

        public Task<List<File>> GetFile(string fileName, int fileType)
        {
            return Task.FromResult(files.Values.Where(file => file.FileName.Equals(fileName) && file.FileType.Equals(fileType)).ToList());
        }

        public Task<List<File>> GetFileWithoutLambda(string fileName, FileType fileType)
        {
            var retorna = new List<File>();

            foreach (var file in files.Values)
            {
                if (file.FileName.Equals(fileName) && file.FileType.Equals(fileType))
                    retorna.Add(file);
            }

            return Task.FromResult(retorna);
        }

        public Task Insert(File file)
        {
            files.Add(file.Id, file);
            return Task.CompletedTask;
        }

        public Task Refresh(File file)
        {
            files[file.Id] = file;
            return Task.CompletedTask;
        }

        public Task Remove(Guid id)
        {
            files.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fecha conexão com o banco
        }
    }
}
