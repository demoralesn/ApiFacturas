using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace ApiFacturas.Models.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        public FacturaRepository() { }

        public IEnumerable<Factura> ObtenerTodas()
        {
            var jsonString = File.ReadAllText("JsonEjemplo.json");
            IEnumerable<Factura>? facturaList = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

            return facturaList;
        }

        public Factura ObtenerPorRutComprador(double rut)
        {
            var jsonString = File.ReadAllText("JsonEjemplo.json");
            IEnumerable<Factura>? facturaList = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

            Factura? facturaEncontrada = facturaList.FirstOrDefault(x => x.RUTComprador == rut);

            return facturaEncontrada;
        }

        public Factura ObtenerPorComunaComprador(double id)
        {
            var jsonString = File.ReadAllText("JsonEjemplo.json");
            IEnumerable<Factura>? facturaList = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

            Factura? facturaEncontrada = facturaList.FirstOrDefault(x => x.ComunaComprador == id);

            return facturaEncontrada;
        }

        public IEnumerable<Factura> ObtenerAgrupadasPorComuna()
        {
            var jsonString = File.ReadAllText("JsonEjemplo.json");
            IEnumerable<Factura>? facturaList = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

            return facturaList.GroupBy(x => x.ComunaComprador).SelectMany(grp => grp).ToList();
        }

        public double ObtenerMayorComprador()
        {
            var jsonString = File.ReadAllText("JsonEjemplo.json");
            IEnumerable<Factura>? facturaList = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

            var rutMayorComprador = facturaList
            .GroupBy(x => x.RUTComprador)
            .OrderByDescending(grp => grp.Count())
            .Select(grp => grp.Key)
            .FirstOrDefault();

            return rutMayorComprador;
        }

        public IEnumerable<Comprador> ObtenerTotalComprasComprador()
        {
            var jsonString = File.ReadAllText("JsonEjemplo.json");
            IEnumerable<Factura>? facturaList = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

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
            var jsonString = File.ReadAllText("JsonEjemplo.json");
            IEnumerable<Factura>? facturaList = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

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