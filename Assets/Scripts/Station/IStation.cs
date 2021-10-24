
public interface IStation
{
    StationTypes StationType { get; set; }
}
public enum StationTypes
{
    cargo,
    repair,
    Shipyard,
    intermediateProduct,
    finalProduct
}
