using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pickups/Fruit"))
        {
            if (MochiManager.Instance.inForceZone) return;
            print("PickUpNormal");

            collision.gameObject.SetActive(false);
            GameManager.Instance.ScoreFruit();
            //Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Pickups/FruitX5"))
        {
            if (MochiManager.Instance.inForceZone) return;
            collision.gameObject.SetActive(false);
            GameManager.Instance.ScoreFruit(5);
            //Destroy(collision.gameObject);
        }
    }
}
