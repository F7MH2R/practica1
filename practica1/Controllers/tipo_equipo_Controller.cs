using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica1.Models;

namespace practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_equipo_Controller : ControllerBase
    {
        private readonly EquiposContext _tipo_equipContext;

        public tipo_equipo_Controller(EquiposContext equiposContext)
        {
            _tipo_equipContext = equiposContext;

        }
        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_TODO")]

        public IActionResult Get()
        {
            List<tipo_equipo> listatipoEquip = (from e in _tipo_equipContext.tipo_equipo
                                       select e).ToList();
            if (listatipoEquip.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(listatipoEquip);

            }

        }
        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult save_equipo([FromBody] tipo_equipo newtipoEquip)
        {
            try
            {
                _tipo_equipContext.tipo_equipo.Add(newtipoEquip);
                _tipo_equipContext.SaveChanges();
                return Ok(newtipoEquip);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult update_reg(int id, [FromBody] tipo_equipo tipoUpdate)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            tipo_equipo? tiposelect = (from e in _tipo_equipContext.tipo_equipo
                                       where e.id_tipo_equipo == id
                                       select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (tiposelect == null)
            {
                return NotFound();
            }
            else
            {
                tiposelect.descripcion = tipoUpdate.descripcion;
                tiposelect.estado = tipoUpdate.estado;



                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _tipo_equipContext.Entry(tiposelect).State = EntityState.Modified;
                _tipo_equipContext.SaveChanges();
                return Ok(tipoUpdate);

            }

        }


        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            tipo_equipo? tiposelect = (from e in _tipo_equipContext.tipo_equipo
                                       where e.id_tipo_equipo == id
                                       select e).FirstOrDefault();

            //Verificamos si existe
            if (tiposelect == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _tipo_equipContext.tipo_equipo.Attach(tiposelect);
                _tipo_equipContext.tipo_equipo.Remove(tiposelect);
                _tipo_equipContext.SaveChanges();
                return Ok("Se a eliminado el registro \n" + tiposelect + "Nombre: " + tiposelect.descripcion + "\nEstado: " + tiposelect.estado);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            tipo_equipo? tiposelect = (from e in _tipo_equipContext.tipo_equipo
                                       where e.id_tipo_equipo == id
                                       select e).FirstOrDefault();


            //Verificar si existe
            if (tiposelect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Nombre: " + tiposelect.descripcion + "\nEstado: " + tiposelect.estado);

            }
        }


    }
}
