using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image img;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Fade(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Fade(false));
    }

    IEnumerator Fade(bool hide)
    {
        for (int i = 0; i < 5; i++)
        {
            Color color = img.color;
            if (hide)
                color.a -= .1f;
            else
                color.a += .1f;
            img.color = color;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
