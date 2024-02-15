namespace ApiFacturas.Models.Repository
{
    public interface IFacturaRepository
    {
        IEnumerable<Factura> ObtenerTodas();
        Factura ObtenerPorRutComprador(double rut);

        IEnumerable<Factura> ObtenerPorComunaComprador(double id);
        IEnumerable<Factura> ObtenerAgrupadasPorComuna();

        double ObtenerMayorComprador();

        IEnumerable<Comprador> ObtenerTotalComprasComprador();

        IEnumerable<FacturaSimple> ObtenerTotalFacturas();
    }
}
