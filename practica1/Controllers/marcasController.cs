using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica1.Models;

namespace practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly EquiposContext _marcasContext;

        public marcasController(EquiposContext equiposContext)
        {
            _marcasContext = equiposContext;

        }
        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_TODO")]

        public IActionResult Get()
        {
            List<marcas> listmarcas = (from e in _marcasContext.marcas
                                                         select e).ToList();
            if (listmarcas.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(listmarcas);

            }

        }
        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult save_equipo([FromBody] marcas marcasnew)
        {
            try
            {
                _marcasContext.marcas.Add(marcasnew);
                _marcasContext.SaveChanges();
                return Ok(marcasnew);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult update_reg(int id, [FromBody] marcas marcasUpdate)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            marcas? marcasselection = (from e in _marcasContext.marcas
                                               where e.id_marca == id
                                               select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (marcasselection == null)
            {
                return NotFound();
            }
            else
            {
                marcasselection.nombre_marca = marcasUpdate.nombre_marca;
                marcasselection.estados = marcasUpdate.estados;



                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _marcasContext.Entry(marcasselection).State = EntityState.Modified;
                _marcasContext.SaveChanges();
                return Ok(marcasUpdate);

            }

        }


        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            marcas? marcasselection = (from e in _marcasContext.marcas
                                               where e.id_marca == id
                                               select e).FirstOrDefault();

            //Verificamos si existe
            if (marcasselection == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _marcasContext.marcas.Attach(marcasselection);
                _marcasContext.marcas.Remove(marcasselection);
                _marcasContext.SaveChanges();
                return Ok("Se a eliminado el registro \n" + marcasselection + "Nombre: " + marcasselection.nombre_marca + "\nEstado: " + marcasselection.estados);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            marcas? marcasselection = (from e in _marcasContext.marcas
                                               where e.id_marca == id
                                               select e).FirstOrDefault();


            //Verificar si existe
            if (marcasselection == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Nombre: " + marcasselection.nombre_marca + "\nEstado: " + marcasselection.estados);

            }
        }


    }
}
