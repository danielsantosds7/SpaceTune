using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivadorDeInimigos : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Inimigos>().AtivarInimigo();
        }
    }
}
