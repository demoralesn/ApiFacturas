namespace ApiFacturas.Models.Repository
{
    public interface IDBConnection
    {
        IEnumerable<Factura> GetConnection();
    }
}
