using System.Collections;
using UnityEngine;

public class InteractableDoor : InteractableObject
{
    public override void Interact(GridInventory inventory)
    {
        var key = inventory.Items.Find(item => (item.GetType() == typeof(Key)));

        if (key != null)
        {
            key.Use();

            StartCoroutine(OnAnimation());
        }
        else Debug.Log("Нет ключа");
    }

    protected IEnumerator OnAnimation()
    {
        yield return new WaitForSeconds(1f);

        Debug.Log("Дверь открыта");
    }
}
