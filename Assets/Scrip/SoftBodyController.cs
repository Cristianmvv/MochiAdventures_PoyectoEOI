using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SoftBodyController : MonoBehaviour
{
    Animator anim;

    #region Slime Variables
    [SerializeField] Rigidbody2D centerSlimeRb; //  Hueso central del Mochi en el modo Slime
    [SerializeField] int speed;
    float movH;
    #endregion

    void Update()
    {
        MovementSlime();
    }

    void MovementSlime()
    {
        movH = Input.GetAxis("Horizontal");
        centerSlimeRb.velocity = new Vector2(movH * speed * Time.deltaTime, centerSlimeRb.velocity.y);    //  Permite el movimiento lateral, si no se le indica el ejeY caera "flotando" y no con su peso real
    }

}
