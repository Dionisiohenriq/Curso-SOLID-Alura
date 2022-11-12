using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Alura.LeilaoOnline.WebApp.Dados.Interfaces;

namespace Alura.LeilaoOnline.WebApp.Dados.EFCore
{
    public class LeilaoDaoComEFCore : ILeilaoDao
    {
        AppDbContext _context;
        public LeilaoDaoComEFCore()
        {
            _context = new AppDbContext();
        }
        public IEnumerable<Categoria> GetAllCategorias()
        {
            return _context.Categorias.AsEnumerable();
        }

        public Leilao FindLeilaoById(int idLeilao)
        {
            return _context.Leiloes.Find(idLeilao);
        }

        public bool Insert(Leilao leilao)
        {
            _context.Add(leilao);

            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Leilao> GetAllLeiloes()
        {
            return _context.Leiloes
                .Include(l => l.Categoria).AsEnumerable();
        }

        public bool Update(Leilao leilao)
        {
            _context.Update(leilao);
            
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Leilao> SearchLeiloes(string termo)
        {
            return _context.Leiloes
                .Include(l => l.Categoria)
                .Where(l => string.IsNullOrWhiteSpace(termo) ||
                    l.Titulo.ToUpper().Contains(termo.ToUpper()) ||
                    l.Descricao.ToUpper().Contains(termo.ToUpper()) ||
                    l.Categoria.Descricao.ToUpper().Contains(termo.ToUpper())
                ).AsEnumerable();
        }

        public IEnumerable<Leilao> GetAllLeilaoCategorias()
        {
            return _context.Leiloes
                .Include(l => l.Categoria).AsEnumerable();
        }
        
        public bool Remove(int id)
        {
            _context.Remove(id);

            return _context.SaveChanges() > 0;
        }

    }
}
