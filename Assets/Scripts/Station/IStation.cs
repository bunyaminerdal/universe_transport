
public interface IStation
{
    StationTypes StationType { get; set; }
    string StationName { get; set; }
}
public enum StationTypes
{
    Cargo,
    Repair,
    Shipyard,
    IntermediateProduct,
    FinalProduct
}
