using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;

namespace ProAtividade.Data.Repositories
{
    public class AtividadeRepo : GeralRepo, IAtividadeRepo
    {
        private readonly DataContext _context;

        public AtividadeRepo(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Atividade> PegaPorIdAsync(int id)
        {
            IQueryable<Atividade> query = _context.Atividades;

            query = query.AsNoTracking()
                        .OrderByDescending(ativ => ativ.Id)
                        .Where(o => o.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Atividade> PegaPorTitulo(string titulo)
        {
            IQueryable<Atividade> query = _context.Atividades;

            query = query.AsNoTracking()
                        .OrderByDescending(ativ => ativ.Id);

            return await query.FirstOrDefaultAsync(o => o.Titulo == titulo);
        }

        public async Task<Atividade[]> PegarTodasAsync()
        {
             IQueryable<Atividade> query = _context.Atividades;

            query = query.AsNoTracking()
                        .OrderByDescending(ativ => ativ.Id);

            return await query.ToArrayAsync();
        }
    }
}