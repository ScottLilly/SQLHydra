using System.ComponentModel;

namespace SQLHydra.QueryFormatter
{
    public static class FormatterFactory
    {
        public static IFormatter GetFormatterFor(Constants.DBTypes databaseType)
        {
            switch(databaseType)
            {
                case Constants.DBTypes.MSSQL2008:
                    return new MSSQL2008();
                case Constants.DBTypes.StandardSQL:
                    return new StandardSQL();
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
