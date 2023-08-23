using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverDeath : MonoBehaviour
{
    [SerializeField] private AudioSource sound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("TOUCHED GROUND");
            sound.Play();
            GameManager.instance.GameOver();
            //sound.Stop();
        }
    }
}
