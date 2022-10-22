using System;
using UnityEngine;

/// <summary>
/// Represents a generic thing that can be interacted with
/// </summary>
public interface IInteractable
{

    /// <summary>
    /// The current state of the interactable
    /// </summary>
    /// <returns>The current state of the interactable</returns>
    EInteractableState GetInteractableState();

    /// <summary>
    /// Interacts with this interactable
    /// </summary>
    /// <param name="interactor">The person interacting with this interactable</param>
    public void Interact(GameObject interactor);

    /// <summary>
    /// Event that is fired when this interactable is interacted with
    /// </summary>
    public event EventHandler OnInteracted;

}
