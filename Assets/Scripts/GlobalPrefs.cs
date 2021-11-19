public class GlobalPrefs
{
    public enum CallbackType
    {
        Delegates,
        UnityEvent
    }

    //Middle finger to compiler
    public static CallbackType CallbackMode = CallbackType.Delegates;

    
}
