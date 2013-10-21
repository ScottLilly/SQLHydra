using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using SQLHydra.Interfaces;
using SQLHydra.Models;

namespace SQLHydra
{
    public class UpdateQuery : BaseQuery, ICanSetColumnValueToUpdateOrAddWhereCondition, ICanAddWhereConditionOrGetSQLCommandObject
    {
        private bool _allRowsSelected = false;

        private readonly List<AssignmentColumn> _assignmentColumns = new List<AssignmentColumn>();

        public UpdateQuery(Constants.DBTypes databaseType, string tableName)
            : base(databaseType, tableName)
        {
        }

        public ICanSetColumnValueToUpdateOrAddWhereCondition SetColumnToValue(string columnName, object value)
        {
            _assignmentColumns.Add(new AssignmentColumn(columnName, value));

            return this;
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

            query.Append("UPDATE ");

            query.AppendFormat(_formatter.TableName, _tableName);

            query.Append(" SET ");

            foreach(AssignmentColumn column in _assignmentColumns)
            {
                query.AppendFormat(_formatter.ColumnName, column.ColumnName);
                query.Append(" = ");
                query.Append(string.Format("@{0}", column.ColumnName));
                query.Append(column == _assignmentColumns[_assignmentColumns.Count - 1] ? "" : ", ");

                command.Parameters.Add(new SqlParameter(column.ColumnName.ForcePrefix("@"), column.Value));
            }

            if(!_allRowsSelected)
            {
                AppendWhereParameters(query, command);
            }

            command.CommandText = query.AsScrubbedString();

            return command;
        }

    }
}
