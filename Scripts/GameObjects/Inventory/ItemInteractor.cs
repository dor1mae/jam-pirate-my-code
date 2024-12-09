using System.Linq.Expressions;
using UnityEngine;


public abstract class ItemInteractor : MonoBehaviour
{
    [SerializeField] protected AbstractScriptableItem _itemForSpawn;

    public void Use(out AbstractItem newItem)
    {
        //Debug.Log(_itemDictionary.GetType());
        newItem = CreateInstance();
    }

    protected abstract AbstractItem CreateInstance();
}
