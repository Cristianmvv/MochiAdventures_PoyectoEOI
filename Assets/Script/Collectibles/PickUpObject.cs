using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickups/Fruit"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.ScoreFruit();
            //Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Pickups/FruitX5"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.ScoreFruit(5);
            //Destroy(collision.gameObject);
        }
    }
}
