using UnityEngine.Events;

public static class PlayerManagerEventHandler
{
    public static UnityEvent<bool> MapChangeEvent = new UnityEvent<bool>();
    public static UnityEvent<float, float> BoundaryCreateEvent = new UnityEvent<float, float>();
    public static UnityEvent<bool> BoundaryChangeEvent = new UnityEvent<bool>();
    public static UnityEvent<float> MovementModifier = new UnityEvent<float>();
    public static UnityEvent RotationBillboard = new UnityEvent();

    public static UnityEvent InteractionEvent = new UnityEvent();
    public static UnityEvent Interaction2Event = new UnityEvent();

    public static UnityEvent<SolarSystem> SolarSelection = new UnityEvent<SolarSystem>();
}
