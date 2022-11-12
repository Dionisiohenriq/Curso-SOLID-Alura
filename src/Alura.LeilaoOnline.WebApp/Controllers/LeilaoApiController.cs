using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados.Interfaces;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [ApiController]
    [Route("/api/leiloes")]
    public class LeilaoApiController : ControllerBase
    {
        ILeilaoDao _dao;
        public LeilaoApiController(ILeilaoDao leilaoDao)
        {
            _dao = leilaoDao;
        }

        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            return Ok(_dao.GetAllLeilaoCategorias());
        }

        [HttpGet("{id}")]
        public IActionResult EndpointGetLeilaoById(int id)
        {
            var leilao = _dao.FindLeilaoById(id);
            
            if (leilao == null)
            {
                return NotFound();
            }

            return Ok(leilao);
        }

        [HttpPost]
        public IActionResult EndpointPostLeilao(Leilao leilao)
        {
            return Ok(_dao.Insert(leilao));
        }

        [HttpPut]
        public IActionResult EndpointPutLeilao(Leilao leilao)
        {
            return Ok(_dao.Update(leilao));
        }

        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteLeilao(int id)
        {
            var leilao = _dao.FindLeilaoById(id);

            if (leilao == null) return NotFound();
            if (leilao.Situacao == SituacaoLeilao.Pregao) return StatusCode(405);

            _dao.Remove(id);

            return NoContent();
        }


    }
}
