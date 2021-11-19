using System;
using UnityEngine;

namespace UnityUtils
{
    public struct IntegerBoolean
    {
#if UNITY_EDITOR
        /// <summary>
        /// Do what it says, or else...
        /// </summary>
        public int getMeViaPointer;
#endif
        public int Buffer { get { return m_buffer; } }

        private int m_buffer;
        private int size;

        /// <summary>
        /// Create an IntegerBoolean that holds 4 Booleans
        /// </summary>
        /// <param name="booleanA"></param>
        /// <param name="booleanB"></param>
        /// <param name="booleanC"></param>
        /// <param name="booleanD"></param>
        public IntegerBoolean(bool booleanA, bool booleanB, bool booleanC, bool booleanD )
        {
            m_buffer = int.Parse("0b" + Convert.ToInt32(booleanA) + Convert.ToInt32(booleanB) + Convert.ToInt32(booleanC) + Convert.ToInt32(booleanD));
            size = 3;
#if UNITY_EDITOR
            getMeViaPointer = 100;
#endif
            return;
        }

        public IntegerBoolean(int buffer)
        {
            m_buffer = buffer;
            size = 4;
#if UNITY_EDITOR
            getMeViaPointer = 0;
#endif
        }
        public void SetBool(int index)
        {
            //m_buffer 
        }
        /// <summary>
        /// Output the value of a given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Evaluate(int index)
        {
            string _parse = "0b" + new string('0', index) + 1 + new string('0', size - index);
            if (!int.TryParse(_parse, out int _bitwise))
            {
                Debug.LogWarning("Failed parse: " + _parse);
                return false;
            }
            Debug.LogWarning(Convert.ToString(_bitwise, 2));
            return (m_buffer & _bitwise) != 0b0000;
        }
#if UNITY_EDITOR
        public unsafe IntegerBoolean* GetMemLoc()
        {
            fixed(IntegerBoolean* IntegerBool = &this)
            {
                return IntegerBool;
            }
        }
#endif
    }
}
