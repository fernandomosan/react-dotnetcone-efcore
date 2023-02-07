using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.Domain.Services
{
    public class AtividadeService : IAtividadeService
    {
        private readonly IAtividadeRepo _atividadeRepo;

        public AtividadeService(IAtividadeRepo atividadeRepo)
        {
            _atividadeRepo = atividadeRepo;
        }

        public async Task<Atividade> AdicionarAtividade(Atividade model)
        {
            if(await _atividadeRepo.PegaPorTitulo(model.Titulo) != null)
            throw new Exception("Já existe uma atividade com este titulo");
        
            if(await _atividadeRepo.PegaPorIdAsync(model.Id) == null){
                _atividadeRepo.Adicionar(model);
                if(await _atividadeRepo.SalvarMudancasAsync()){
                    return model;
                }
            }

            return null;
        }

        public async Task<Atividade> AtualizarAtividade(Atividade model)
        {
            if(model.DataConclusao != null)
                throw new Exception("Não se pode atualizar uma atividade já concluida");
            
            if(await _atividadeRepo.PegaPorIdAsync(model.Id) != null){
                _atividadeRepo.Atualizar(model);
                if(await _atividadeRepo.SalvarMudancasAsync()){
                    return model;
                }
            }

            return null;
        }

        public async Task<bool> ConcluirAtividade(Atividade model)
        {

            if(model != null){
            model.Concluir();
            _atividadeRepo.Atualizar(model);
            return await _atividadeRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> DeletarAtividade(int atividadeId)
        {
            var atividade = await _atividadeRepo.PegaPorIdAsync(atividadeId);
            if(atividade == null) throw new Exception("Atividade que tentou deletar não existe");

            _atividadeRepo.Deletar(atividade);
            return await _atividadeRepo.SalvarMudancasAsync();
        }

        public async Task<Atividade> PegarAtividadePorId(int atividadeId)
        {            
            try
            {
                var atividade = await _atividadeRepo.PegaPorIdAsync(atividadeId);
                if(atividade == null) return null;
                
                return atividade;
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Atividade[]> PegarTodasAtividadesAsync()
        {
            try
            {
                var atividade = await _atividadeRepo.PegarTodasAsync();
                if(atividade == null) return null;
                
                return atividade;
            }
            catch (System.Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}