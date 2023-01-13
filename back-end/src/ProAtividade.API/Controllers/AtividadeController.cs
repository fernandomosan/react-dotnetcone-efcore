using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProAtividade.API.Model;

namespace ProAtividade.API.Controllers
{
    [Route("api/[controller]")]
    public class AtividadeController : Controller
    {
        public IEnumerable<Atividade> listAtividade = new List<Atividade>(){
            new Atividade(1),
            new Atividade(2),
            new Atividade(3)
        };

        [HttpGet]
        public IEnumerable<Atividade> Get(){
            return listAtividade;
        }

        [HttpGet("{id}")]
        public Atividade Get(int id){
            return listAtividade.FirstOrDefault(item => item.Id == id);
        }

        [HttpPost()]
        public IEnumerable<Atividade> Post(Atividade atividade){
            
            return listAtividade.Append<Atividade>(atividade);
        }

        [HttpPut("{id}")]
        public Atividade Put(int id, Atividade atividade){
            atividade.Id += 1;

            return atividade;
        }

        [HttpDelete("{id}")]
        public string Delelete(int id){
            return $"Meu primeiro metodo delete com paramentro {id}";
        }
        
    }
}