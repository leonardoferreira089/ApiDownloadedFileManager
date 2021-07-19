using System;

namespace ApiDownloadedFileManager.Exceptions
{
    public class FileAlreadyRegisteredException : Exception
    {
        public FileAlreadyRegisteredException()
            : base("Este arquivo já está cadastrado")
        { }
    }
}
