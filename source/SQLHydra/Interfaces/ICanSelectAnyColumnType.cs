namespace SQLHydra.Interfaces
{
    public interface ICanSelectAnyColumnType : ICanSelectIndividualColumn
    {
        ICanSelectIndividualColumn SelectAllColumns();
    }
}
