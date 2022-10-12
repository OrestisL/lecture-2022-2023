using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent onClick = new UnityEvent();
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicks {eventData.clickCount}");
        onClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Entered {name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Exited {name}");
    }
}
