using System.Data.SqlClient;

namespace SQLHydra.Interfaces
{
    public interface ICanGetSQLCommandObject
    {
        SqlCommand GetSQLCommandObject();
    }
}
