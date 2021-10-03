using UnityEngine.Events;
using System.Collections.Generic;

public static class PlayerManagerEventHandler
{
    public static UnityEvent<bool> MapChangeEvent = new UnityEvent<bool>();
    public static UnityEvent<float, float> BoundaryCreateEvent = new UnityEvent<float, float>();
    public static UnityEvent<bool> BoundaryChangeEvent = new UnityEvent<bool>();
    public static UnityEvent<float> MovementModifierEvent = new UnityEvent<float>();
    public static UnityEvent RotationBillboardEvent = new UnityEvent();
    public static UnityEvent InteractionEvent = new UnityEvent();
    public static UnityEvent RouteCreateInteractionEvent = new UnityEvent();
    public static UnityEvent<SolarSystem> SolarSelectionEvent = new UnityEvent<SolarSystem>();
    public static UnityEvent<List<RoutePart>> CreateRouteEvent = new UnityEvent<List<RoutePart>>();
    public static UnityEvent<SolarClusterStruct[]> SolarClustersReadyEvent = new UnityEvent<SolarClusterStruct[]>();
    public static UnityEvent<SolarSystem> RoutePartInstantiateEvent = new UnityEvent<SolarSystem>();
}
