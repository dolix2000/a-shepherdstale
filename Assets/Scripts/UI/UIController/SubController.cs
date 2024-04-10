using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Abstract base class for SubControllers and implemented reference to RootController
/// </summary>

public abstract class SubController : MonoBehaviour
{
    [HideInInspector] // Make variable public, but hide in Unity's Editor to prevent wrong assignment
    public RootController root;

    public static List<SubController> controllerInstances = new List<SubController>(10);

    public void Awake()
    {
        controllerInstances.Add(this);
    }

    /// <summary>
    /// Public method to use specific controller. 
    /// Virtual method can be overridden by a deriving class (eg. specific Subcontroller)
    /// </summary>

    public virtual void UseController()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Public method to release specific controller from duty.
    /// </summary>
    public virtual void ReleaseController()
    {
        gameObject.SetActive(false);
    }
}
/// <summary>
/// Extension of SubController class to generic UI Root reference.
/// </summary>
/// 
public abstract class SubController<T> : SubController where T : UIRoot
{
    [SerializeField]
    protected T ui;
    public T UI
    {
        get => ui;
        set => ui = value;
    }

    public override void UseController()
    {
        base.UseController();

        ui.ShowRoot();
    }

    public override void ReleaseController()
    {
        base.ReleaseController();

        ui.HideRoot();
    }
}

