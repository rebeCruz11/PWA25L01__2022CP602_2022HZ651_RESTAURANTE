using L01_2022CP602_2022HZ651.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022CP602_2022HZ651.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly RestauranteContext _RestauranteContext;

        public pedidosController(RestauranteContext restauranteContext)
        {
            _RestauranteContext = restauranteContext;
        }
        // EndPoint que retorna el listado de todos los pedidos existentes

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listadoMotorista = (from e in _RestauranteContext.pedidos
                                                 select e).ToList();

            if (listadoMotorista.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoMotorista);
        }



        // EndPoint que retorna los registros de una tabla filtrados por cliente

        [HttpGet]
        [Route("getById/{filtro}")]
        public IActionResult Get(string filtro)
        {
            motoristas? motorista = (from m in _RestauranteContext.motoristas
                                     where m.nombreMotorista.Contains(filtro)
                                     select m).FirstOrDefault();

            if (motorista == null)
            {
                return NotFound();
            }

            return Ok(motorista);
        }

        // EndPoint que retorna los registros de una tabla filtrados por motorista

        [HttpGet]
        [Route("getById/{filtro}")]
        public IActionResult Get(string filtro)
        {
            motoristas? motorista = (from m in _RestauranteContext.motoristas
                                     where m.nombreMotorista.Contains(filtro)
                                     select m).FirstOrDefault();

            if (motorista == null)
            {
                return NotFound();
            }

            return Ok(motorista);
        }

        // EndPoint que agrega nuevos motorista a la tabla

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarMotorista([FromBody] motoristas motorista)
        {
            try
            {
                _RestauranteContext.motoristas.Add(motorista);
                _RestauranteContext.SaveChanges();
                return Ok(motorista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // EndPoint que actualiza los datos del motorista 
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarMotorista(int id, [FromBody] motoristas motoristaModificar)
        {
            motoristas? motoristaActual = (from m in _RestauranteContext.motoristas
                                           where m.motoristaId == id
                                           select m).FirstOrDefault();

            if (motoristaActual == null)
            { return NotFound(); }
            motoristaActual.nombreMotorista = motoristaModificar.nombreMotorista;

            _RestauranteContext.Entry(motoristaActual).State = EntityState.Modified;
            _RestauranteContext.SaveChanges();

            return Ok(motoristaModificar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            motoristas? motorista = (from m in _RestauranteContext.motoristas
                                     where m.motoristaId == id
                                     select m).FirstOrDefault();

            if (motorista == null)
                return NotFound();

            _RestauranteContext.motoristas.Attach(motorista);
            _RestauranteContext.motoristas.Remove(motorista);
            _RestauranteContext.SaveChanges();

            return Ok(motorista);
        }
    }

}
}
