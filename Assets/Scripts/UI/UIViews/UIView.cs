using UnityEngine;

/// <summary>
/// Base class for different UI elements and views
/// </summary>

public class UIView : MonoBehaviour
{
    /// <summary>
    /// Show UI elements
    /// </summary>
    public virtual void ShowView()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide UI elements
    /// </summary>
    public virtual void HideView()
    {
        gameObject.SetActive(false);
    }
}
