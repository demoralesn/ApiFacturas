using Newtonsoft.Json;
using System.Text.Json.Nodes;
using ApiFacturas.DAL;

namespace ApiFacturas.Models.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IDBConnection _dbConnection;
        
        public FacturaRepository(IDBConnection dbConnection) { 
            _dbConnection = dbConnection;
        }

        public IEnumerable<Factura> ObtenerTodas()
        {
            IEnumerable<Factura>? facturaList = _dbConnection.GetConnection();

            return facturaList;
        }

        public Factura ObtenerPorRutComprador(double rut)
        {
            IEnumerable<Factura>? facturaList = _dbConnection.GetConnection();

            Factura? facturaEncontrada = facturaList.FirstOrDefault(x => x.RUTComprador == rut);

            return facturaEncontrada;
        }

        public IEnumerable<Factura> ObtenerPorComunaComprador(double id)
        {
            IEnumerable<Factura>? facturaList = _dbConnection.GetConnection();

            return facturaList.Where(x => x.ComunaComprador == id);
        }

        public IEnumerable<Factura> ObtenerAgrupadasPorComuna()
        {
            IEnumerable<Factura>? facturaList = _dbConnection.GetConnection();

            return facturaList.GroupBy(x => x.ComunaComprador).SelectMany(grp => grp).ToList();
        }

        public double ObtenerMayorComprador()
        {
            IEnumerable<Factura>? facturaList = _dbConnection.GetConnection();

            var rutMayorComprador = facturaList
            .GroupBy(x => x.RUTComprador)
            .OrderByDescending(grp => grp.Count())
            .Select(grp => grp.Key)
            .FirstOrDefault();

            return rutMayorComprador;
        }

        public IEnumerable<Comprador> ObtenerTotalComprasComprador()
        {
            IEnumerable<Factura>? facturaList = _dbConnection.GetConnection();

            List<Comprador> compradorVentas = new List<Comprador>();

            double totalCompras = 0;

            var facturasAgrupadas = facturaList.GroupBy(x => x.RUTComprador)
                      .ToDictionary(g => g.Key, g => g.ToList());

            foreach (KeyValuePair<double, List<Factura>> fact in facturasAgrupadas)
            {
                foreach (Factura factura in fact.Value)
                {
                    foreach (DetalleFactura facturaDet in factura.DetalleFactura)
                    {
                        totalCompras += facturaDet.TotalProducto;
                    }
                }

                compradorVentas.Add(new Comprador { RUTComprador = fact.Key, TotalCompras = totalCompras });

                totalCompras = 0;
            }

            return compradorVentas;
        }

        public IEnumerable<FacturaSimple> ObtenerTotalFacturas()
        {
            IEnumerable<Factura>? facturaList = _dbConnection.GetConnection();

            List<FacturaSimple> totalFacturas = new List<FacturaSimple>();

            double totalCompras = 0;


            foreach (Factura factura in facturaList)
            {
                foreach (DetalleFactura facturaDet in factura.DetalleFactura)
                {
                    totalCompras += facturaDet.TotalProducto;
                }

                totalFacturas.Add(new FacturaSimple { NumeroDocumento = factura.NumeroDocumento, TotalFactura = totalCompras });

                totalCompras = 0;
            }

            return totalFacturas;
        }
    }
}