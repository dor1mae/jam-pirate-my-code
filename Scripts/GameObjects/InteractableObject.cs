using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract void Interact(GridInventory inventory);
}
