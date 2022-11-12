using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Dados.Interfaces
{
    public interface ICategoriaDao
    {
        Categoria GetCategoriaById(int id);
        IEnumerable<Categoria> GetAllCategorias();
    }
}
