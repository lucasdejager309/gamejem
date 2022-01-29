using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class lootItem {
    public GameObject prefab;
    public float dropchancePercentage;
}

public class DropLoot : MonoBehaviour
{
    public lootItem[] items;

    public void Drop() {
        
        foreach(lootItem item in items) {
            float chance = Random.Range(0, 100);
            if (chance < item.dropchancePercentage) {
                GameObject droppedItem = Instantiate(item.prefab, transform.position, Quaternion.identity);
                droppedItem.transform.rotation = Quaternion.EulerRotation(droppedItem.transform.rotation.x, droppedItem.transform.rotation.y, Random.RandomRange(0, 360));
            }
        }
    }
}
