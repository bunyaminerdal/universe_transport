using UnityEngine.Events;

public static class PlayerManagerEventHandler
{
    public static UnityEvent<bool> MapChangeEvent = new UnityEvent<bool>();
    public static UnityEvent<float, float> BoundryCreateEvent = new UnityEvent<float, float>();
    public static UnityEvent<bool> BoundryChangeEvent = new UnityEvent<bool>();
    public static UnityEvent<float> MovementModifier = new UnityEvent<float>();
}
