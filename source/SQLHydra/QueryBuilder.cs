using SQLHydra.Interfaces;

namespace SQLHydra
{
    public class QueryBuilder
    {
        private readonly Constants.DBTypes _databaseType;

        internal QueryBuilder(Constants.DBTypes databaseType)
        {
            _databaseType = databaseType;
        }

        public ICanSelectAnyColumnType BuildSelectQueryForTable(string tableName)
        {
            return new SelectQuery(_databaseType, tableName);
        }

        public ICanSetColumnValueToInsert BuildInsertQueryForTable(string tableName)
        {
            return new InsertQuery(_databaseType, tableName);
        }

        public ICanSetColumnValueToUpdateOrAddWhereCondition BuildUpdateQueryForTable(string tableName)
        {
            return new UpdateQuery(_databaseType, tableName);
        }

        public ICanApplyToAllRowsOrAddWhereCondition BuildDeleteQueryForTable(string tableName)
        {
            return new DeleteQuery(_databaseType, tableName);
        }
    }
}
