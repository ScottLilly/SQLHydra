using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using SQLHydra.Interfaces;
using SQLHydra.Models;

namespace SQLHydra
{
    public class InsertQuery : BaseQuery, ICanSetColumnValueToInsertOrGetSQLCommandObject
    {
        private readonly List<AssignmentColumn> _assignmentColumns = new List<AssignmentColumn>();

        public InsertQuery(Constants.DBTypes databaseType, string tableName)
            : base(databaseType, tableName)
        {
        }

        public ICanSetColumnValueToInsertOrGetSQLCommandObject SetColumnToValue(string columnName, object value)
        {
            _assignmentColumns.Add(new AssignmentColumn(columnName, value));

            return this;
        }

        public SqlCommand GetSQLCommandObject()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO ");

            query.AppendFormat(_formatter.TableName, _tableName);

            query.Append(" (");

            foreach(AssignmentColumn column in _assignmentColumns)
            {
                query.AppendFormat(_formatter.ColumnName, column.ColumnName);
                query.Append(column == _assignmentColumns[_assignmentColumns.Count - 1] ? "" : ", ");
            }

            query.Append(") VALUES (");

            foreach(AssignmentColumn column in _assignmentColumns)
            {
                query.AppendFormat("@{0}", column.ColumnName);
                query.Append(column == _assignmentColumns[_assignmentColumns.Count - 1] ? "" : ", ");

                command.Parameters.Add(new SqlParameter(column.ColumnName.ForcePrefix("@"), column.Value));
            }

            query.Append(")");

            command.CommandText = query.AsScrubbedString();

            return command;
        }
    }
}
