using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SoftBodyController : MonoBehaviour
{
    Animator anim;

    #region Slime Variables
    [SerializeField] Rigidbody2D centerSlimeRb; //  Rigid Body del Hueso central del Mochi en el modo Slime
    [SerializeField] Rigidbody2D[] PerimeterSlimeRb;    //  Rigid Body de los huesos perimetrales del Mochi en el modo Slime
    [SerializeField] int speed;
    float movH;
    #endregion

    //  Intento de comunicar la inercia por eventos... fallo mucho
    //private void Awake()
    //{
    //    MochiManager.TakeInertia += SendInertiaValue;
    //}
    //private void OnDisable()
    //{
    //    MochiManager.TakeInertia -= SendInertiaValue;
    //} 

    void Awake()
    {
        //  Al aparecer le mete la inercia de la forma esfera a todos los RigidBody del slime
        centerSlimeRb.velocity = MochiManager.Instance.inertia;
        for (int i = 0; i < PerimeterSlimeRb.Length; i++)
        {
            PerimeterSlimeRb[i].velocity = MochiManager.Instance.inertia;
        }
    }

    void FixedUpdate()
    {
        MovementSlime();
    }

    void MovementSlime()
    {
        movH = Input.GetAxis("Horizontal");
        if (movH!=0)    //  Permite que al soltar el boton, la velocidad no vuelva a 0 sino que decaiga con el drag del Rigidbody
        {
            centerSlimeRb.velocity = new Vector2(movH * speed * Time.deltaTime, centerSlimeRb.velocity.y);    //  Permite el movimiento lateral, si no se le indica el ejeY caera "flotando" y no con su peso real
        }
    }

    //void SendInertiaValue()
    //{
    //    Debug.Log("InertiaSlime");
    //    MochiManager.Instance.inertia = centerSlimeRb.velocity;
    //}

}
