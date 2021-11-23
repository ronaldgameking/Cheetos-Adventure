using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Alias using
using UnityUtils;

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
    public LockConstrains NoOffset;

    private bool offsetXDone = false;
    private bool offsetYDone = false;
    private bool offsetZDone = false;

    private Vector3 newPos = Vector3.zero;
    private IntegerBoolean offsetAppliedLock = new IntegerBoolean();

    private void Awake()
    {
        //Don't judge me, I like pointers...
        #region pointers
#if UNITY_EDITOR
        //Make a struct (pass by value) act like a class (pass by reference)
        unsafe
        {
            //Make a new instance of struct
            IntegerBoolean ib = new IntegerBoolean();
            //Get the memory address of the struct
            IntegerBoolean* _ibptr = &ib;
            //Assign some number
            _ibptr->getMeViaPointer = 298364;
            //Make another var point to that address
            IntegerBoolean* ibsec = _ibptr;
            //Output current value of _ibptr's getMeViaPointer
            Debug.Log(_ibptr->getMeViaPointer);
            //Change the value of _ibptr's variable
            _ibptr->getMeViaPointer = 298000;
            //Show that the value has changed for both
            Debug.Log(_ibptr->getMeViaPointer);
            Debug.Log(ibsec->getMeViaPointer);
        }
#endif
        #endregion
    }

    void Update()
    {
        //Some debugging stuff, ignore
        //Show signed 32 bit integer as binary (base 2)
        Debug.Log(Convert.ToString((int)Position, 2));
        LockConstrains _con = LockConstrains.YZ;
        Debug.Log(Convert.ToString((int)_con, 2));
        newPos = transform.position;
        try
        {
            //Check if X shouldn't be changed by extracting the bits.
            //We need the binary AND operator then comparing it against binary 0
            //if lock enabled comparison evaluates to false
            if (((int)Position & 0b1000) == 0b0000)
            {
                newPos.x = followThis.position.x + offset.x;
            }
            //Check if X should still have an offset applied
            else if (((int)NoOffset & 0b1000) == 0b0000 && !offsetXDone)
            {
                newPos.x = transform.position.x + offset.x;
                offsetXDone = true;
                //offsetAppliedLock = new IntegerBoolean(true,  offsetAppliedLock.Evaluate(1), offsetAppliedLock.Evaluate(2), false);
            }

            //Same as X but with Y
            if (((int)Position & 0b0100) == 0b0000)
            {
                newPos.y = followThis.position.y + offset.y;
            }
            else if (((int)NoOffset & 0b0010) == 0b0000 && !offsetYDone)
            {
                newPos.y = transform.position.y + offset.y;
                offsetYDone = true;
                //offsetAppliedLock = new IntegerBoolean(offsetAppliedLock.Evaluate(0), true, offsetAppliedLock.Evaluate(2), false);
            }

            //Same as X and Y but with Z
            if (((int)Position & 0b0010) == 0b0000)
            {
                newPos.z = followThis.position.z + offset.z;
            }
            else if (((int)NoOffset & 0b0010) == 0b0000 && !offsetZDone)
            {
                //SetZ();
                newPos.z = transform.position.z + offset.z;
                offsetZDone = true;
                //offsetAppliedLock = new IntegerBoolean(offsetAppliedLock.Evaluate(0), offsetAppliedLock.Evaluate(1), true,  false);
            }

            transform.position = newPos;
            newPos = Vector3.zero;
        }
        catch (Exception e)
        {
            //Debug.LogError(e);
            enabled = false;
        }
    }

    private void SetZ()
    {
        Vector3 pos = transform.position;
        pos.z = transform.position.z + offset.z;
        Debug.Log(transform.position.z + "|" + pos.z + "|" + offset.z);
        transform.position = pos;
    }
}
