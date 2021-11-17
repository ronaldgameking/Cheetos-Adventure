using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtils.delegates;


public class Health : MonoBehaviour
{
    public int MaxHp { get; private set; }

    public int Hitpoints;
    public bool UseCallback = false;
    public bool DontDestroy = false;

    public SimpleCallback D_onDeathCallback;
    public SimpleCallback D_onDamageCallback;
    
    public UnityEngine.Events.UnityEvent UE_onDeathCallback;
    public UnityEngine.Events.UnityEvent UE_onDamageCallback;

    private void Awake()
    {
        MaxHp = Hitpoints;
    }

    public void ModifyHealth(int _hp)
    {
        Hitpoints += _hp;
        if (Hitpoints <= 0)
        {
            //.
            if (UseCallback)
            {
                if (GlobalPrefs.CallbackMode == GlobalPrefs.CallbackType.Delegates)
                    D_onDeathCallback?.Invoke();
                else
                    UE_onDeathCallback?.Invoke();
            }
            if (!DontDestroy)
            {
                Destroy(gameObject);
            }
        }
        if (GlobalPrefs.CallbackMode == GlobalPrefs.CallbackType.Delegates)
            D_onDamageCallback?.Invoke();
        else
            UE_onDamageCallback?.Invoke();
    }
}
