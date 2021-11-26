using System;

/// <summary>
/// Functions made to shoot yourself in the foot
/// </summary>
public static unsafe class C
{
    private static void* available;
    public static void* malloc(Int32 m)
    {
        Int32 getAdr = new Int32();
        void* adr = &getAdr;
        available = (Int32*)available + (sizeof(Int32));
        return adr;
    }

    public static void free(ref object a)
    {
        a = null;
        GC.Collect();
    }
    
    //private static void free<T>(ref T a) where T : class
    //{
    //    a = null;
    //    GC.Collect();
    //}
}
