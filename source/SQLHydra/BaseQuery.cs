using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;

using SQLHydra.Models;
using SQLHydra.QueryFormatter;

namespace SQLHydra
{
    public abstract class BaseQuery
    {
        protected readonly string _tableName;
        protected readonly IFormatter _formatter;

        private readonly List<WhereCondition> _whereConditions = new List<WhereCondition>();

        protected BaseQuery(Constants.DBTypes databaseType, string tableName)
        {
            _tableName = tableName;
            _formatter = FormatterFactory.GetFormatterFor(databaseType);
        }

        protected void AddWhereCondition(string columnName, Constants.Comparators comparator, object value)
        {
            _whereConditions.Add(new WhereCondition(columnName, comparator, value));
        }

        protected void AppendWhereParameters(StringBuilder query, SqlCommand command)
        {
            if(_whereConditions.Count == 0)
            {
                return;
            }

            query.Append(" WHERE ");

            foreach(WhereCondition whereCondition in _whereConditions)
            {
                query.AppendFormat(_formatter.ColumnName, whereCondition.ColumnName);

                query.Append(GetComparatorString(whereCondition.Comparator));

                query.AppendFormat("@WHERE_{0}_{1}", whereCondition.ColumnName, _whereConditions.IndexOf(whereCondition));

                AddSqlParameter(command, whereCondition);

                query.Append(whereCondition == _whereConditions[_whereConditions.Count - 1] ? "" : " AND ");
            }

            query.Append(" ");
        }

        protected string GetComparatorString(Constants.Comparators comparator)
        {
            switch(comparator)
            {
                case Constants.Comparators.EqualTo:
                    return _formatter.EqualTo;
                case Constants.Comparators.NotEqualTo:
                    return _formatter.NotEqualTo;
                case Constants.Comparators.GreaterThan:
                    return _formatter.GreaterThan;
                case Constants.Comparators.GreaterThanOrEqualTo:
                    return _formatter.GreaterThanOrEqualTo;
                case Constants.Comparators.LessThan:
                    return _formatter.LessThan;
                case Constants.Comparators.LessThanOrEqualTo:
                    return _formatter.LessThanOrEqualTo;
                case Constants.Comparators.StartsWith:
                    return _formatter.StartWith;
                case Constants.Comparators.EndsWith:
                    return _formatter.EndsWith;
                case Constants.Comparators.Contains:
                    return _formatter.Contains;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private void AddSqlParameter(SqlCommand command, WhereCondition whereCondition)
        {
            switch(whereCondition.Comparator)
            {
                case Constants.Comparators.StartsWith:
                    command.Parameters
                           .Add(new SqlParameter(
                                    string.Format("@WHERE_{0}_{1}", whereCondition.ColumnName,
                                                  _whereConditions.IndexOf(whereCondition)),
                                    whereCondition.Value.ToString().ForceSuffix(_formatter.WildCardCharacter)));
                    break;
                case Constants.Comparators.EndsWith:
                    command.Parameters
                           .Add(new SqlParameter(
                                    string.Format("@WHERE_{0}_{1}", whereCondition.ColumnName,
                                                  _whereConditions.IndexOf(whereCondition)),
                                    whereCondition.Value.ToString().ForcePrefix(_formatter.WildCardCharacter)));
                    break;
                case Constants.Comparators.Contains:
                    command.Parameters
                           .Add(new SqlParameter(
                                    string.Format("@WHERE_{0}_{1}", whereCondition.ColumnName,
                                                  _whereConditions.IndexOf(whereCondition)),
                                    whereCondition.Value.ToString()
                                                  .ForcePrefix(_formatter.WildCardCharacter)
                                                  .ForceSuffix(_formatter.WildCardCharacter)));
                    break;
                default:
                    command.Parameters
                           .Add(new SqlParameter(
                                    string.Format("@WHERE_{0}_{1}", whereCondition.ColumnName,
                                                  _whereConditions.IndexOf(whereCondition)),
                                    whereCondition.Value));
                    break;
            }
        }
    }
}
