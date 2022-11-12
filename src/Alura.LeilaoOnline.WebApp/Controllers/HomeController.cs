using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Alura.LeilaoOnline.WebApp.Dados.Interfaces;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class HomeController : Controller
    {
        ILeilaoDao _dao;
        ICategoriaDao _categoriaDao;
        public HomeController(ILeilaoDao leilaoDao, ICategoriaDao categoriaDao)
        {
            _dao = leilaoDao;
            _categoriaDao = categoriaDao;
        }

        public IActionResult Index()
        {
            return View(_categoriaDao.GetAllCategorias());
        }

        [Route("[controller]/StatusCode/{statusCode}")]
        public IActionResult StatusCodeError(int statusCode)
        {
            if (statusCode == 404) return View("404");
            return View(statusCode);
        }

        [Route("[controller]/Categoria/{categoria}")]
        public IActionResult Categoria(int categoria)
        {
            
            return View(_categoriaDao.GetCategoriaById(categoria));
        }

        [HttpPost]
        [Route("[controller]/Busca")]
        public IActionResult Busca(string termo)
        {
            ViewData["termo"] = termo;

            return View(_dao.SearchLeiloes(termo));
        }
    }
}
