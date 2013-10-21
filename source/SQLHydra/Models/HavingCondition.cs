namespace SQLHydra.Models
{
    public class HavingCondition
    {
        public ComputedColumn AggregateFunction { get; private set; }
        public Constants.Comparators Comparator { get; private set; }
        public double Value { get; private set; }

        public HavingCondition(ComputedColumn aggregateFunction, Constants.Comparators comparator, double value)
        {
            AggregateFunction = aggregateFunction;
            Comparator = comparator;
            Value = value;
        }
    }
}
