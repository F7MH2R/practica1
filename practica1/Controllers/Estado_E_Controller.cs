using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using practica1.Models;

namespace practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estado_E_Controller : ControllerBase
    {
        private readonly EquiposContext _equiposEContext;

        public Estado_E_Controller(EquiposContext equiposContext)
        {
            _equiposEContext = equiposContext;

        }
        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_TODO")]

        public IActionResult Get()
        {
            List<estados_equipo> listadoEquiposEstado = (from e in _equiposEContext.estados_equipo
                                                         select e).ToList();
            if (listadoEquiposEstado.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(listadoEquiposEstado);

            }

        }
        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult save_equipo([FromBody] estados_equipo estadosEquipoNew)
        {
            try
            {
                _equiposEContext.estados_equipo.Add(estadosEquipoNew);
                _equiposEContext.SaveChanges();
                return Ok(estadosEquipoNew);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult update_reg(int id, [FromBody] estados_equipo esatdoEUpdate)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            estados_equipo? equiposelection = (from e in _equiposEContext.estados_equipo
                                        where e.id_estados_equipo == id
                                        select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (equiposelection == null)
            {
                return NotFound();
            }
            else
            {
                equiposelection.descripcion = esatdoEUpdate.descripcion;
                equiposelection.estado = esatdoEUpdate.estado;



                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _equiposEContext.Entry(equiposelection).State = EntityState.Modified;
                _equiposEContext.SaveChanges();
                return Ok(esatdoEUpdate);

            }

        }


        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            estados_equipo? equiposelection = (from e in _equiposEContext.estados_equipo
                                        where e.id_estados_equipo == id
                                        select e).FirstOrDefault();

            //Verificamos si existe
            if (equiposelection == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _equiposEContext.estados_equipo.Attach(equiposelection);
                _equiposEContext.estados_equipo.Remove(equiposelection);
                _equiposEContext.SaveChanges();
                return Ok("Se a eliminado el registro \n" + equiposelection + "Descripción: " + equiposelection.descripcion + "\nEstado: " + equiposelection.estado);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            estados_equipo? equiposelection = (from e in _equiposEContext.estados_equipo
                                        where e.id_estados_equipo == id
                                        select e).FirstOrDefault();


            //Verificar si existe
            if (equiposelection == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Descripción: " + equiposelection.descripcion + "\nEstado: " + equiposelection.estado);

            }
        }



    }
}
