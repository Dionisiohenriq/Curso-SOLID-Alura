using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Dados.Interfaces
{
    public interface ILeilaoDao
    {
        IEnumerable<Categoria> GetAllCategorias();
        Leilao FindLeilaoById(int idLeilao);
        bool Insert(Leilao leilao);
        IEnumerable<Leilao> GetAllLeiloes();
        bool Update(Leilao leilao);
        IEnumerable<Leilao> SearchLeiloes(string termo);
        IEnumerable<Leilao> GetAllLeilaoCategorias();
        bool Remove(int id);

    }
}
