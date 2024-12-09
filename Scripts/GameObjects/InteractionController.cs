using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actionAsset;
    [SerializeField] private GridInventory _inventory;
    [SerializeField] private float _distance;

    private LayerMask _layerMask = 1 << 9;

    private InputAction _inputAction;
    private readonly Vector2 Center = new Vector2(Screen.width / 2, Screen.height / 2);

    private void Start()
    {
        _inputAction = _actionAsset.FindAction("Use");
    }

    private IEnumerator OnInputActivation()
    {
        _inputAction.Disable();

        yield return new WaitForSeconds(0.1f);

        _inputAction.Enable();
    }

    public void OnTriggerStay(Collider other)
    {
        if (_inputAction.IsPressed())
        {
            Debug.Log("Выполнено действие подбора");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Center);

            if (Physics.Raycast(ray, out hit, _distance))
            {
                Debug.Log($"Луч запущен, и получил объект с тегом {hit.collider.tag} и именем {hit.collider.gameObject.name}");
                var item = hit.collider.gameObject;
                if (item.tag == "Item")
                {
                    Debug.Log("Попытка собрать предмет");
                    item.GetComponent<ItemInteractor>().Use(out var newItem);
                    GridInventory.SendItem.Invoke(newItem);

                    Debug.Log($"{gameObject.name}: предмет подобран");

                    StartCoroutine(OnInputActivation());

                    Destroy(hit.collider.gameObject);
                }
                else if (item.tag == "Interactable")
                {
                    Debug.Log("Попытка воспользоваться дверью");
                    var obj = item.GetComponent<InteractableObject>();
                    obj.Interact(_inventory);
                }
            }
        }
    }
}
