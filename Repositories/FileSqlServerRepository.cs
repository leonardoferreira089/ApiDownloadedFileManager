using ApiDownloadedFileManager.Entities;
using ApiDownloadedFileManager.ViewModel.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiDownloadedFileManager.Repositories
{
    public class FileSqlServerRepository : IFileRepository
    {
        private readonly SqlConnection sqlConnection;

        public FileSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<File>> GetFile(int page, int qty)
        {
            var files = new List<File>();

            var comando = $"select * from Files order by id offset {((page - 1) * qty)} rows fetch next {qty} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                files.Add(new File
                {
                    Id = (Guid)sqlDataReader["Id"],
                    FileName = (string)sqlDataReader["FileName"],
                    FileType = (FileType)sqlDataReader["FileType"],
                    GenreType = (string)sqlDataReader["GenreType"],
                    Purchased = (bool)sqlDataReader["Purchased"]
                });
            }

            await sqlConnection.CloseAsync();

            return files;
        }

        public async Task<File> GetFile(Guid id)
        {
            File file = null;

            var comando = $"select * from Files where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                file = new File
                {
                    Id = (Guid)sqlDataReader["Id"],
                    FileName = (string)sqlDataReader["FileName"],
                    FileType = (FileType)sqlDataReader["FileType"],
                    GenreType = (string)sqlDataReader["GenreType"],
                    Purchased = (bool)sqlDataReader["Purchased"]
                };
            }

            await sqlConnection.CloseAsync();

            return file;
        }

        public async Task<List<File>> GetFile(string fileName, int fileType)
        {
            var files = new List<File>();

            var comando = $"select * from Files where FileName = '{fileName}' and FileType = '{fileType}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                files.Add(new File
                {
                    Id = (Guid)sqlDataReader["Id"],
                    FileName = (string)sqlDataReader["FileName"],
                    FileType = (FileType)sqlDataReader["FileType"],
                    GenreType = (string)sqlDataReader["GenreType"],
                    Purchased = (bool)sqlDataReader["Purchased"]
                });
            }

            await sqlConnection.CloseAsync();

            return files;
        }

        public async Task Insert(File file)
        {
            var comando = $"insert Files (Id, FileName, FileType, GenreType, Purchased) values ('{file.Id}', '{file.FileName}', '{file.FileType}', '{file.GenreType}', '{file.Purchased}')";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Refresh(File file)
        {
            var comando = $"update Files set FileName = '{file.FileName}', FileType = '{file.FileType}', GenreType = {file.GenreType}, Purchased = {file.Purchased} where Id = '{file.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remove(Guid id)
        {
            var comando = $"delete from Files where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
