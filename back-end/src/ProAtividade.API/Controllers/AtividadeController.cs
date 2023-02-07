using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.API.Controllers
{
    [Route("api/[controller]")]
    public class AtividadeController : Controller
    {
        private readonly IAtividadeService _atividadeService;

        public AtividadeController(IAtividadeService atividadeService)
        {
            _atividadeService = atividadeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(){
            try
             {
                var atividades = await _atividadeService.PegarTodasAtividadesAsync();
                if(atividades == null) return NoContent();

                return Ok(atividades);
             }
             catch (System.Exception ex)
             {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar atividade. Erro>: {ex.Message}");
             }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id){
            try
             {
                var atividade = await _atividadeService.PegarAtividadePorId(id);
                if(atividade == null) return NoContent();

                return Ok(atividade);
             }
             catch (System.Exception ex)
             {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar recuperar atividade com id: {id}. Erro>: {ex.Message}");
             }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Atividade atividade)
        {
            try
             {
                var atividadeAdd = await _atividadeService.AdicionarAtividade(atividade);
                if(atividadeAdd == null) return NoContent();

                return Ok(atividadeAdd);
             }
             catch (System.Exception ex)
             {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar adicionar atividade. Erro>: {ex.Message}");
             }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Atividade atividade){

           try
             {
                if(atividade.Id !=  id) 
                    this.StatusCode(StatusCodes.Status409Conflict, 
                    $"Você está tendo atualizar a tividade errada");

                var atividadeAtualizada = await _atividadeService.AtualizarAtividade(atividade);
                if(atividadeAtualizada == null) return NoContent();

                return Ok(atividadeAtualizada);
             }
             catch (System.Exception ex)
             {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Erro ao tentar atualizar atividade id:{id}. Erro: {ex.Message}");
             }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delelete(int id){
            try
            {
                if(await _atividadeService.DeletarAtividade(id)){
                    return Ok(new {message = "Deletado"});
                }
                else{
                    return BadRequest("Ocorreu um problema não especifico ao tentar deletar atividade.");
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Erro ao deletar atividade id: {id} Erro: {ex.Message}");
            }
            
        }
        
    }
}