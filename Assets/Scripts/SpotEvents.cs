using System;

public static class SpotEvents
{
    public static Action<InteractableSpot> OnClicked;

    public static void RaiseClicked(InteractableSpot spot)
    {
        OnClicked?.Invoke(spot);
    }
}
