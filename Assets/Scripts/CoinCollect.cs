using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField]
    GameObject effect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                string coinTag = gameObject.tag; // Get the tag of the coin that was collided with

                switch (coinTag)
                {
                    case "coinOne":
                        Debug.Log("coinOne");
                        CoinController.instance.calculateScore(5);
                        break;
                    case "coinTwo":
                        Debug.Log("coinTwo");
                        CoinController.instance.calculateScore(25);
                        break;
                    case "coinThree":
                        Debug.Log("coinThree");
                        CoinController.instance.calculateScore(100);
                        break;
                    case "coinFour":
                        Debug.Log("coinFour");
                        CoinController.instance.calculateScore(500);
                        break;
                }

                Destroy(gameObject);
                Instantiate(effect, transform.position, Quaternion.identity);
            }
        }


       
    }
}
