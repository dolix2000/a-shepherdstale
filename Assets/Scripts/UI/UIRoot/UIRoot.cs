using UnityEngine;

/// <summary>
/// Base class for different UI-Pages
/// </summary>

public class UIRoot : MonoBehaviour
{
    /// <summary>
    /// Show UI
    /// </summary>
    public virtual void ShowRoot()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hide UI
    /// </summary>
    public virtual void HideRoot()
    {
        gameObject.SetActive(false);
    }
}
