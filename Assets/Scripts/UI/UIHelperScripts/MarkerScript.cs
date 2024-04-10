using UnityEngine;
using UnityEditor;

/// <summary>
/// Script to instantiate a little indicator for menu pointer events. Should be placed on RootControllers.
/// </summary>
public class MarkerScript : MonoBehaviour
{
    public static GameObject menuMarker;
    private GameObject menuMarkerPath;
    public static bool menuMarkerLoaded;

    /// <summary>
    /// Instantiate only once and reuse during active controller lifecycle.
    /// </summary>
    private void Awake()
    {
        menuMarkerPath = (GameObject)Resources.Load("UI/MenuMarker", typeof(GameObject));
    }

    private void Start()
    {
        if (!menuMarkerLoaded)
        {
            menuMarker = Instantiate(menuMarkerPath);
            DontDestroyOnLoad(menuMarker);
            menuMarker.name = "MenuMarker"; // Buttons use Find (by name) method to access this element.
            menuMarkerLoaded = true;
            Debug.Log("MenuMarker present");
        }
    }
}
