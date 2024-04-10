using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script to child the menu element indicator. Should be placed on every button that should have it.
/// </summary>
public class MarkerButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject menuMarker; // The little icon that indicates a menu element is pointed at.
    private GameObject uiElement;  // The specific UI element that the user is pointing at

    private void Start()
    {
        menuMarker = GameObject.Find("MenuMarker"); // Name gets set on instantiation in MarkerScript.
        Debug.Log("MenuMarker: " + menuMarker);
    }

    /// <summary>
    /// Mouse Pointer event when the user hovers over a menu element.
    /// The menu marker gets bound to this element until the pointer leaves the area.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (menuMarker != null)
        {
            uiElement = eventData.pointerEnter.transform.parent.gameObject;
            menuMarker.transform.SetParent(uiElement.transform);
            menuMarker.transform.localPosition = new Vector3(0, 0, 0);
            menuMarker.SetActive(true);
        }
    }

    /// <summary>
    /// Mouse Pointer event when the user leaves the are of a menu element.
    /// The menu marker will disappear.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (menuMarker != null)
        {
            menuMarker.SetActive(false);
        }
    }
}

