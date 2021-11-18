using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public enum LockConstrains
    {
        None = 0b0000,
        OnlyX = 0b1000,
        OnlyY = 0b0100,
        OnlyZ = 0b0010,
        XY = 0b1100,
        XZ = 0b1010,
        YZ = 0b0110
    }

    public Transform followThis;
    public Vector3 offset;

    [Header("Constrains")]
    public LockConstrains Position;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(System.Convert.ToString((int)Position, 2));
        LockConstrains _con = LockConstrains.OnlyX | LockConstrains.OnlyY | LockConstrains.OnlyZ;
        Debug.Log(System.Convert.ToString((int)_con, 2));

        try
        {
            if (((int)Position & 0b1110) == 0b0000)
            {
                transform.position = followThis.position + offset;
            }

            else
            {
                Vector3 curpos = transform.position;
                curpos.y = followThis.position.y + offset.y;
                curpos.z = followThis.position.z + offset.z;
                transform.position = curpos;

            }

            switch ((int)Position & 0b1110)
            {
                default:
                    break;
            }
        }
        catch
        {
            enabled = false;
        }
    }
}
