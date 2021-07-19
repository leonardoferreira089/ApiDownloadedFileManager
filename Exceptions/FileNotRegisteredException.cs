using System;

namespace ApiDownloadedFileManager.Exceptions
{
    public class FileNotRegisteredException : Exception
    {
        public FileNotRegisteredException()
            : base("Este arquivo não está cadastrado")
        { }
    }
}
