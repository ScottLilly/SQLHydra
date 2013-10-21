using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using SQLHydra.Interfaces;
using SQLHydra.Models;

namespace SQLHydra
{
    public class SelectQuery : BaseQuery, ICanSelectAnyColumnType, ICanSelectIndividualColumnOrHavingClause
    {
        private bool _selectAllColumns = false;

        private readonly List<Column> _columns = new List<Column>();
        private readonly List<Column> _groupByColumns = new List<Column>();
        private readonly List<ComputedColumn> _computedColumns = new List<ComputedColumn>();
        private readonly List<HavingCondition> _havingConditions = new List<HavingCondition>();
        private readonly List<Join> _innerJoins = new List<Join>();
        private readonly List<Join> _leftJoins = new List<Join>();
        private readonly SortedList<string, Constants.OrderByDirections> _sortOrders = new SortedList<string, Constants.OrderByDirections>();

        public SelectQuery(Constants.DBTypes databaseType, string tableName)
            : base(databaseType, tableName)
        {
        }

        public ICanSelectIndividualColumn SelectAllColumns()
        {
            _selectAllColumns = true;

            return this;
        }

        public ICanSelectIndividualColumn SelectColumn(string name)
        {
            return SelectColumn(name, null);
        }

        public ICanSelectIndividualColumn SelectColumn(string name, string alias)
        {
            _columns.Add(new Column(name, alias));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause SelectGroupByColumn(string name)
        {
            _columns.Add(new Column(name));
            _groupByColumns.Add(new Column(name));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause SelectGroupByColumn(string name, string alias)
        {
            _columns.Add(new Column(name, alias));
            _groupByColumns.Add(new Column(name, alias));

            return this;
        }

        #region Select column of aggregate function

        public ICanSelectIndividualColumn SelectMinValueForColumn(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.Min, columnName, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectMaxValueForColumn(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.Max, columnName, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectCountOfAllRows(string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.CountOfAllRows, null, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectCountOfRowsWithNonNullColumnValues(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.CountOfRowsWithNonNullColumnValues, columnName, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectCountOfRowsWithDistinctColumnValues(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.CountOfRowsWithDistinctColumnValues, columnName, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectSumOfAllRows(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.SumOfAllRows, columnName, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectSumOfRowsWithDistinctColumnValues(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.SumOfRowsWithDistinctColumnValues, columnName, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectAverageOfAllRows(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.AverageOfAllRows, columnName, alias));

            return this;
        }

        public ICanSelectIndividualColumn SelectAverageOfRowsWithDistinctColumnValues(string columnName, string alias)
        {
            _computedColumns.Add(new ComputedColumn(Constants.ComputationTypes.AverageOfRowsWithDistinctColumnValues, columnName, alias));

            return this;
        }

        #endregion

        #region Select column of aggregate function for HAVING clause

        public ICanSelectIndividualColumnOrHavingClause HavingMinValueForColumn(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.Min, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingMaxValueForColumn(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.Max, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingCountOfAllRows(Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.CountOfAllRows, null, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingCountOfRowsWithNonNullColumnValues(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.CountOfRowsWithNonNullColumnValues, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingCountOfRowsWithDistinctColumnValues(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.CountOfRowsWithDistinctColumnValues, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingSumOfColumnForAllRows(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.SumOfAllRows, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingSumOfColumnsWithDistinctValues(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.SumOfRowsWithDistinctColumnValues, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingAverageOfColumnForAllRows(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.AverageOfAllRows, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        public ICanSelectIndividualColumnOrHavingClause HavingAverageOfColumnsWithDistinctValues(string columnName, Constants.Comparators comparator, double value, string alias)
        {
            ComputedColumn computedColumn = new ComputedColumn(Constants.ComputationTypes.AverageOfRowsWithDistinctColumnValues, columnName, alias);

            _computedColumns.Add(computedColumn);
            _havingConditions.Add(new HavingCondition(computedColumn, comparator, value));

            return this;
        }

        #endregion

        public ICanAddJoin InnerJoin(string tableName, string baseTableColumnName, string joinedTableColumnName)
        {
            _innerJoins.Add(new Join(tableName, baseTableColumnName, joinedTableColumnName));

            return this;
        }

        public ICanAddJoin LeftJoin(string tableName, string baseTableColumnName, string joinedTableColumnName)
        {
            _leftJoins.Add(new Join(tableName, baseTableColumnName, joinedTableColumnName));

            return this;
        }

        public ICanAddWhereCondition Where(string columnName, Constants.Comparators comparator, object value)
        {
            AddWhereCondition(columnName, comparator, value);

            return this;
        }

        public ICanAddOrderByClause OrderBy(string columnName, Constants.OrderByDirections sortOrder)
        {
            if(!_sortOrders.ContainsKey(columnName))
            {
                _sortOrders.Add(columnName, sortOrder);
            }

            return this;
        }

        public SqlCommand GetSQLCommandObject()
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;

            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");

            if(_selectAllColumns)
            {
                query.AppendFormat("{0} ", _formatter.AllColumns);
            }
            else
            {
                foreach(Column column in _columns)
                {
                    query.AppendFormat(_formatter.ColumnName, column.Name);

                    if(column.Alias != null)
                    {
                        query.AppendFormat(" AS {0}", column.Alias);
                    }

                    if(_computedColumns.Count == 0)
                    {
                        query.Append(column == _columns[_columns.Count - 1] ? "" : ", ");
                    }
                    else
                    {
                        query.Append(", ");
                    }
                }

                foreach(ComputedColumn computedColumn in _computedColumns)
                {
                    AppendAggregateFunction(query, computedColumn);

                    query.AppendFormat(" AS {0}", computedColumn.Alias);

                    query.Append(computedColumn == _computedColumns[_computedColumns.Count - 1] ? "" : ", ");
                }
            }

            query.Append(" FROM ");

            query.AppendFormat(_formatter.TableName, _tableName);

            foreach(Join innerJoin in _innerJoins)
            {
                query.AppendFormat(" INNER JOIN {0} ON {1}.{2} = {0}.{3} ",
                                   string.Format(_formatter.TableName, innerJoin.TableName),
                                   string.Format(_formatter.TableName, _tableName),
                                   string.Format(_formatter.TableName, innerJoin.BaseTableColumnName),
                                   string.Format(_formatter.TableName, innerJoin.JoinedTableColumnName));
            }

            foreach(Join leftJoin in _leftJoins)
            {
                query.AppendFormat(" LEFT JOIN {0} ON {1}.{2} = {0}.{3} ",
                                   string.Format(_formatter.TableName, leftJoin.TableName),
                                   string.Format(_formatter.TableName, _tableName),
                                   string.Format(_formatter.TableName, leftJoin.BaseTableColumnName),
                                   string.Format(_formatter.TableName, leftJoin.JoinedTableColumnName));
            }

            AppendWhereParameters(query, command);

            if(_groupByColumns.Count > 0)
            {
                query.Append(" GROUP BY ");

                foreach(Column column in _groupByColumns)
                {
                    query.AppendFormat(_formatter.ColumnName, column.Name);

                    query.Append(column == _groupByColumns[_groupByColumns.Count - 1] ? "" : ", ");
                }
            }

            if(_havingConditions.Count > 0)
            {
                query.Append(" HAVING ");

                foreach(HavingCondition havingCondition in _havingConditions)
                {
                    AppendAggregateFunction(query, havingCondition.AggregateFunction);
                    query.Append(" ");
                    query.Append(GetComparatorString(havingCondition.Comparator));
                    query.Append(" ");

                    query.AppendFormat("@HAVING_{0}", havingCondition.AggregateFunction.Alias.ToUpper());

                    command.Parameters.Add(new SqlParameter(string.Format("@HAVING_{0}", havingCondition.AggregateFunction.Alias.ToUpper()), havingCondition.Value));
                }
            }

            if(_sortOrders.Count > 0)
            {
                query.Append(" ORDER BY ");

                foreach(KeyValuePair<string, Constants.OrderByDirections> sortOrder in _sortOrders)
                {
                    query.AppendFormat(" {0} {1}", string.Format(_formatter.ColumnName, sortOrder.Key), sortOrder.Value == Constants.OrderByDirections.Ascending ? "ASC" : "DESC");

                    query.Append((_sortOrders.IndexOfKey(sortOrder.Key) == (_sortOrders.Count - 1)) ? "" : ", ");
                }
            }

            command.CommandText = query.AsScrubbedString();

            return command;
        }

        private void AppendAggregateFunction(StringBuilder query, ComputedColumn computedColumn)
        {
            switch(computedColumn.ComputationType)
            {
                case Constants.ComputationTypes.Min:
                    query.AppendFormat("MIN({0})", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
                case Constants.ComputationTypes.Max:
                    query.AppendFormat("MAX({0})", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
                case Constants.ComputationTypes.CountOfAllRows:
                    query.AppendFormat("COUNT(*)");
                    break;
                case Constants.ComputationTypes.CountOfRowsWithNonNullColumnValues:
                    query.AppendFormat("COUNT({0})", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
                case Constants.ComputationTypes.CountOfRowsWithDistinctColumnValues:
                    query.AppendFormat("COUNT(DISTINCT({0}))", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
                case Constants.ComputationTypes.SumOfAllRows:
                    query.AppendFormat("SUM({0})", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
                case Constants.ComputationTypes.SumOfRowsWithDistinctColumnValues:
                    query.AppendFormat("SUM(DISTINCT({0}))", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
                case Constants.ComputationTypes.AverageOfAllRows:
                    query.AppendFormat("AVG({0})", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
                case Constants.ComputationTypes.AverageOfRowsWithDistinctColumnValues:
                    query.AppendFormat("AVG(DISTINCT({0}))", string.Format(_formatter.ColumnName, computedColumn.Name));
                    break;
            }
        }
    }
}
