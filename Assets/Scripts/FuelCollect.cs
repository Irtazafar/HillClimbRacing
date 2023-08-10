using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollect : MonoBehaviour
{
    

    [SerializeField]
    GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FuelController.instance.FillFuel();
            Destroy(gameObject);
            Instantiate(effect, transform.position, Quaternion.identity);
        }
    }
}
