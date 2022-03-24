using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField] public bool IsLocked { get; protected set; }
    [field: SerializeField] public bool IsBroken { get; protected set; }
    

    public abstract void Lock(Vector3 position);

    public abstract void Unlock(Vector3 position);

    public abstract void Interact(Vector3 position);

    public abstract string GetDescription();
    
    public abstract bool GetState();

    public void ToggleLock() => IsLocked = !IsLocked;

    public void ToggleBreak() => IsBroken = !IsBroken;

    public void Repare() 
    {
        if(IsBroken) ToggleBreak();
    } 
}
