using Microsoft.AspNetCore.Mvc;
using ApiFacturas.Models.Repository;

namespace ApiFacturas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : Controller
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaController(IFacturaRepository facturaRepository) {
            _facturaRepository = facturaRepository;
        }

        [HttpGet("Facturas/GetFacturas")]
        public IActionResult GetFacturas()
        {
            try
            {
                var facturaList = _facturaRepository.ObtenerTodas();

                return Ok(facturaList);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("Facturas/GetTotalFacturas")]
        public IActionResult GetTotalFacturas()
        {
            try
            {
                var facturaList = _facturaRepository.ObtenerTotalFacturas();

                return Ok(facturaList);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("Comprador/GetPorRutComprador/{rut}")]
        public IActionResult GetPorRutComprador(double rut)
        {
            try
            {
                var facturaEncontrada = _facturaRepository.ObtenerPorRutComprador(rut);

                return Ok(facturaEncontrada);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("Comprador/GetRutMayorComprador")]
        public IActionResult GetRutMayorComprador()
        {
            try
            {
                var rutMayorComprador = _facturaRepository.ObtenerMayorComprador();

                return Ok(rutMayorComprador);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("Comprador/GetTotalComprasComprador")]
        public IActionResult GetTotalComprasComprador()
        {
            try
            {
                var comprasComprador = _facturaRepository.ObtenerTotalComprasComprador();

                return Ok(comprasComprador);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("Comuna/GetFacturasPorComunaComprador/{id}")]
        public IActionResult GetFacturasPorComunaComprador(double id)
        {
            try
            {
                var facturaEncontrada = _facturaRepository.ObtenerPorComunaComprador(id);

                return Ok(facturaEncontrada);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("Comuna/GetAgrupadasPorComuna")]
        public IActionResult GetAgrupadasPorComuna()
        {
            try
            {
                var facturaList = _facturaRepository.ObtenerAgrupadasPorComuna();

                return Ok(facturaList);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
