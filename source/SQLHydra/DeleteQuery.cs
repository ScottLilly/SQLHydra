using System.Data;
using System.Data.SqlClient;
using System.Text;

using SQLHydra.Interfaces;

namespace SQLHydra
{
    public class DeleteQuery : BaseQuery, ICanApplyToAllRowsOrAddWhereCondition, ICanAddWhereConditionOrGetSQLCommandObject
    {
        private bool _allRowsSelected = false;

        public DeleteQuery(Constants.DBTypes databaseType, string tableName)
            : base(databaseType, tableName)
        {
        }

        public ICanGetSQLCommandObject ApplyToAllRows()
        {
            _allRowsSelected = true;

            return this;
        }

        public ICanAddWhereConditionOrGetSQLCommandObject Where(string columnName, Constants.Comparators comparator, object value)
        {
            AddWhereCondition(columnName, comparator, value);

            return this;
        }

        public SqlCommand GetSQLCommandObject()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            StringBuilder query = new StringBuilder();

            query.Append("DELETE FROM ");

            query.AppendFormat(_formatter.TableName, _tableName);

            if(!_allRowsSelected)
            {
                AppendWhereParameters(query, command);
            }

            command.CommandText = query.AsScrubbedString();

            return command;
        }
    }
}
