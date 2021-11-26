using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Toolbar : MonoBehaviour
{
    public List<GameObject> elements;

    public int page;

    public void SetPage(int pg)
    {
        elements[page].SetActive(false);
        page = pg;
        elements[page].SetActive(true);
    }

    //List<List<GameObject>> ListToListList(List<GameObject> listlistElements)
    //{
    //    List<List<GameObject>> lout = new List<List<GameObject>>();
    //    int indexDef = 0;
    //    int layer = 0;
    //    if (listlistElements[0] == null || listlistElements[0].name == "_Submodule")
    //        throw new System.Exception("I0003: List must begin with _Submodule");
    //    for (int i = 0; i < listlistElements.Count; i++)
    //    {
    //        if (listlistElements[i] == null) continue;
    //        if (listlistElements[i].name == "_Submodule" && layer == 0)
    //        {
    //            layer++;
    //        }
    //        else if (listlistElements[i].name == "_SubEnd" && layer == 1)
    //        {
    //            layer--;
    //            indexDef++;
    //        }
    //        else if (listlistElements[i].name == "_Submodule" && layer == 1)
    //        {
    //            throw new System.Exception("I0001: Cannot define Submodule in Submodule");
    //        }
    //        else if (listlistElements[i].name == "_SubEnd" && layer == 0)
    //        {
    //            throw new System.Exception("I0002: No Submodule to close");
    //        }
    //        else if (layer == 1)
    //        {
    //            lout[layer].Add(listlistElements[i]);
    //        }
    //        else
    //        {
    //            throw new System.Exception("I0004: Cannot have objects on root");
    //        }
    //    }
    //    return lout;
    //}
}