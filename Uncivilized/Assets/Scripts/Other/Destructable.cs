using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public ItemObject item;
    public Transform itemsParent;
    public Vector3 dropOffset = Vector3.zero;
    public float hits = 2.0f;

    public void TakeDamage()
    {
        hits -= 1.0f;
        if(hits <= 0.0f)
        {
            Instantiate(item.prefab, transform.position + dropOffset, transform.rotation, itemsParent);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            other.enabled = false;
            TakeDamage();
        }
    }
}
