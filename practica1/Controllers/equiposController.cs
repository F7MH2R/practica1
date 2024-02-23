using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using practica1.Models;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace practica1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly EquiposContext _equiposContext;

        public equiposController(EquiposContext equiposContext)
        {
            _equiposContext = equiposContext;
            
        }



        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_TODO")]

        public IActionResult Get()
        {
            List<equipos> listadoequipo = (from e in _equiposContext.equipos
                                          select e).ToList();
            if (listadoequipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoequipo);

        }




        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult save_equipo([FromBody]equipos equipo) {
            try
            {
                _equiposContext.equipos.Add(equipo);
                _equiposContext.SaveChanges();
                return Ok(equipo);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);


            }

        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult update_reg(int id, [FromBody] equipos equipoUpdates) {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            equipos? equiposelection = (from e in _equiposContext.equipos
                                        where e.id_equipos == id
                                        select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (equiposelection == null)
            {
                return NotFound();
            }
            else
            {
                equiposelection.nombre = equipoUpdates.nombre;
                equiposelection.descripcion = equipoUpdates.descripcion;
                equiposelection.tipo_equipo_id = equipoUpdates.tipo_equipo_id;
                equiposelection.marca_id = equipoUpdates.marca_id;
                equiposelection.modelo = equipoUpdates.modelo;
                equiposelection.anio_compra = equipoUpdates.anio_compra;
                equiposelection.costo = equipoUpdates.costo;
                equiposelection.vida_util = equipoUpdates.vida_util;
                equiposelection.estado_equipo_id = equipoUpdates.estado_equipo_id;
                equiposelection.estado = equipoUpdates.estado;



                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _equiposContext.Entry(equiposelection).State = EntityState.Modified;
                _equiposContext.SaveChanges();
                return Ok(equipoUpdates);

            }

        }
        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            equipos? equiposelection = (from e in _equiposContext.equipos
                                        where e.id_equipos == id
                                        select e).FirstOrDefault();

            //Verificamos si existe
            if (equiposelection == null)
            {
                return NotFound();
            }
            else { 
                //si existe ejecutamos la accion de eliminar
                _equiposContext.equipos.Attach(equiposelection);
                _equiposContext.equipos.Remove(equiposelection);
                _equiposContext.SaveChanges();
                return Ok("Se a eliminado el registro \n"+equiposelection + "Nombre: "+equiposelection.nombre+"\nCosto: "+equiposelection.costo);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id ) {

            //Buscar el registro con la consulta
            equipos? equiposelection = (from e in _equiposContext.equipos
                                        where e.id_equipos == id
                                        select e).FirstOrDefault();


            //Verificar si existe
            if(equiposelection == null)
            {
                return NotFound();
            }
            else { 
                return Ok("Busqueda realizada con exito\n "+ "Nombre"+equiposelection.nombre+"\nCosto"+equiposelection.costo);
            
            }
        }
    }
}
