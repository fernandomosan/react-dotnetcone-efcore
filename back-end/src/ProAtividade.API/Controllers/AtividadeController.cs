using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAtividade.API.Data;
using ProAtividade.API.Model;

namespace ProAtividade.API.Controllers
{
    [Route("api/[controller]")]
    public class AtividadeController : Controller
    {
        private readonly DataContext _context; 
        public AtividadeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Atividade> Get(){
            return _context.Atividades.ToList();
        }

        [HttpGet("{id}")]
        public Atividade Get(int id){
            return _context.Atividades.FirstOrDefault(item => item.Id == id);
        }

        [HttpPost]
        public Atividade Post([FromBody]Atividade atividade)
        {
            _context.Atividades.Add(atividade);
            if (_context.SaveChanges() > 0)
                return _context.Atividades.FirstOrDefault(ati => ati.Id == atividade.Id);
            else
                throw new Exception("Você não conseguiu adicionar uma atividade");
        }

        [HttpPut("{id}")]
        public Atividade Put(int id, [FromBody]Atividade atividade){

            if(atividade.Id != id) throw new Exception("Você está tentando atualizar a atividade errada");

            _context.Update(atividade);
            if(_context.SaveChanges() > 0){
                return _context.Atividades.FirstOrDefault(x => x.Id == id);
            }
            else{
                return new Atividade();
            }
        }

        [HttpDelete("{id}")]
        public bool Delelete(int id){

            var atividade = _context.Atividades.FirstOrDefault(x => x.Id == id);

            if(atividade == null){
                throw new Exception("Você está tentando deletar uma atividade que não existe!");
            }

            _context.Remove(atividade);

            return _context.SaveChanges() > 0;
        }
        
    }
}