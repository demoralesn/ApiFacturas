using ApiFacturas.Models;
using ApiFacturas.Models.Repository;
using Newtonsoft.Json;

namespace ApiFacturas.DAL
{
    public class DBConnection : IDBConnection
    {
        public DBConnection()
        {

        }

        public IEnumerable<Factura> GetConnection()
        {
            try
            {
                var jsonString = File.ReadAllText("JsonEjemplo.json");
                IEnumerable<Factura>? data = JsonConvert.DeserializeObject<List<Factura>>(jsonString);

                return data;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Factura>();
            }
        }
    }
}
