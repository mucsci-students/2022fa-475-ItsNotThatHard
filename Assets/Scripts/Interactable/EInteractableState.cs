/// <summary>
/// Indicates the states that an interactable can be in
/// 
/// INTERACTABLE: The interactable can be interacted with
/// DISABLED: The interactable is disabled and cannot be interacted with
/// LOCKED: The interactable is locked. It can be interacted with, but it will not do what it is supposed to until unlocked
/// </summary>
public enum EInteractableState
{
    
    INTERACTABLE,
    DISABLED,
    LOCKED,

}
