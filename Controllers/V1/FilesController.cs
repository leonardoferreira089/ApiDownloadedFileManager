using ApiDownloadedFileManager.Exceptions;
using ApiDownloadedFileManager.InputModel;
using ApiDownloadedFileManager.Services;
using ApiDownloadedFileManager.ViewModel;
using ApiDownloadedFileManager.ViewModel.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDownloadedFileManager.Controllers.V1
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Buscar todos os arquivos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os arquivos sem paginação
        /// </remarks>
        /// <param name="page">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="qty">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de arquivos</response>
        /// <response code="204">Caso não haja arquivos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileViewModel>>> GetFile([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int qty = 5)
        {
            var files = await _fileService.GetFile(page, qty);

            if (files.Count() == 0)
                return NoContent();

            return Ok(files);
        }

        /// <summary>
        /// Buscar um arquivo pelo seu Id
        /// </summary>
        /// <param name="idFile">Id do arquivo buscado</param>
        /// <response code="200">Retorna o arquivo filtrado</response>
        /// <response code="204">Caso não haja arquivo com este id</response>   
        [HttpGet("{idFile:guid}")]
        public async Task<ActionResult<FileViewModel>> GetFile([FromRoute] Guid idFile)
        {
            var file = await _fileService.GetFile(idFile);

            if (file == null)
                return NoContent();

            return Ok(file);
        }

        /// <summary>
        /// Inserir um arquivo no catálogo
        /// </summary>
        /// <param name="fileInputModel">Dados do Arquivo a ser inserido</param>
        /// <response code="200">Caso o Arquivo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um arquivo com mesmo nome para a mesma produtora</response> 
        [HttpPost]
        public async Task<ActionResult<FileViewModel>> InsertFile([FromBody] FileInputModel fileInputModel)
        {
            try
            {
                var file = await _fileService.Insert(fileInputModel);

                return Ok(file);
            }
            catch (FileAlreadyRegisteredException)
            {
                return UnprocessableEntity("Já existe um arquivo com este nome.");
            }
        }

        /// <summary>
        /// Atualizar um arquivo no catálogo
        /// </summary>
        /// /// <param name="idFile">Id do arquivo a ser atualizado</param>
        /// <param name="fileInputModel">Novos dados para atualizar o arquivo indicado</param>
        /// <response code="200">Caso o arquivo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um arquivo com este Id</response> 
        [HttpPut("{idFile:guid}")]
        public async Task<ActionResult> UpdateFile([FromRoute] Guid idFile, [FromBody] FileInputModel fileInputModel)
        {
            try
            {
                await _fileService.Refresh(idFile, fileInputModel);

                return Ok();
            }
            catch (FileAlreadyRegisteredException)
            {
                return NotFound("Não existe este arquivo");
            }
        }

        /// <summary>
        /// Atualizar o tipo de um arquivo
        /// </summary>
        /// /// <param name="idFile">Id do arquivo a ser atualizado</param>
        /// <param name="fileType">Novo tipo de arquivo do arquivo</param>
        /// <response code="200">Caso o tipo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um arquivo com este Id</response> 
        [HttpPatch("{idFile:guid}/fileType/{fileType:FileType}")]
        public async Task<ActionResult> RefreshFile([FromRoute] Guid idFile, [FromRoute] FileType fileType)
        {
            try
            {
                await _fileService.Refresh(idFile, fileType);

                return Ok();
            }
            catch (FileAlreadyRegisteredException)
            {
                return NotFound("Não existe este arquivo");
            }
        }

        /// <summary>
        /// Excluir um arquivo
        /// </summary>
        /// /// <param name="idFile">Id do arquivo a ser excluído</param>
        /// <response code="200">Caso o tipo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um arquivo com este Id</response>   
        [HttpDelete("{idFile:guid}")]
        public async Task<ActionResult> DeleteFile([FromRoute] Guid idFile)
        {
            try
            {
                await _fileService.Remove(idFile);

                return Ok();
            }
            catch (FileAlreadyRegisteredException)
            {
                return NotFound("Não existe este arquivo");
            }
        }

    }
}
