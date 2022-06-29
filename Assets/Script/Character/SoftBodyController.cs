using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SoftBodyController : MonoBehaviour
{
    //public static SoftBodyController Instance { get; private set; }


    #region Slime Variables
    [Header("RigidBodySlime")]
    [SerializeField] Rigidbody2D centerSlimeRb; //  Rigid Body del Hueso central del Mochi en el modo Slime
    [SerializeField] Rigidbody2D[] perimeterSlimeRb;    //  Rigid Body de los huesos perimetrales del Mochi en el modo Slime

    [Header("Movement")]
    [SerializeField] int speed;

    [Header("Raycast")] 
    public bool isGrounded;
    bool isGrounded1;
    bool isGrounded2;
    bool isGrounded3;
    [SerializeField] LayerMask groundLayer;

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
        //if (Instance != null && Instance != this) Destroy(this);    //  Si al inicio del script tiene no ningun valor o si ya lo tiene, lo destruye
        //else Instance = this;   //  Le a?ade el valor de este propio script

        //  Al aparecer le mete la inercia de la forma esfera a todos los RigidBody del slime
        centerSlimeRb.velocity = MochiManager.Instance.inertia;
        for (int i = 0; i < perimeterSlimeRb.Length; i++)
        {
            perimeterSlimeRb[i].velocity = MochiManager.Instance.inertia;
        }
    }

    void FixedUpdate()
    {
        MovementSlime();
        RaycastGroundCheck();
    }

    void MovementSlime()
    {

        if (MochiManager.Instance.disableMovement == true) return;
        movH = Input.GetAxis("Horizontal");
        if (movH!=0 /*|| centerSlimeRb.velocity.x >= 4*/)    //  Permite que al soltar el boton, la velocidad no vuelva a 0 sino que decaiga con el drag del Rigidbody
        {
            centerSlimeRb.velocity = new Vector2(movH * speed, centerSlimeRb.velocity.y);    //  Permite el movimiento lateral, si no se le indica el ejeY caera "flotando" y no con su peso real
        }

    }

    void RaycastGroundCheck()
    {

        for (int i = 0; i < perimeterSlimeRb.Length; i++)
        {

            //Illo, no pregunteis que tipo de magia gitana he invocado para que salgan varios raycast... no lo se ni yo.

            var right45 = (-perimeterSlimeRb[i].transform.right + perimeterSlimeRb[i].transform.up).normalized;
            var left45 = (-perimeterSlimeRb[i].transform.right - perimeterSlimeRb[i].transform.up).normalized;

            Debug.DrawRay(perimeterSlimeRb[i].transform.position, -perimeterSlimeRb[i].transform.right * .2f,Color.red);
            Debug.DrawRay(perimeterSlimeRb[i].transform.position, right45 * .2f,Color.blue);
            Debug.DrawRay(perimeterSlimeRb[i].transform.position, left45 * .2f,Color.blue);

            isGrounded1 = Physics2D.Raycast(perimeterSlimeRb[i].transform.position, -perimeterSlimeRb[i].transform.right, .2f, groundLayer);
            isGrounded2 = Physics2D.Raycast(perimeterSlimeRb[i].transform.position, right45, .2f, groundLayer);
            isGrounded3 = Physics2D.Raycast(perimeterSlimeRb[i].transform.position, left45, .2f, groundLayer);

            if (isGrounded1 || isGrounded2 || isGrounded3)  //  isGrounded se va sobrescribiendo por el valor del ultimo, por lo que si existen 3 solo cojera el valor del 3?
            {
                isGrounded = true;
                MochiManager.Instance.isGroundedSlime = isGrounded;
                return; //  Mismo pasa con las vueltas en el array, solo coje el valor del ultimo objeto, por lo que en cuanto uno sea true se tiene que parar ahi para que no continue
            }
            else 
            {
                isGrounded = false;
                MochiManager.Instance.isGroundedSlime = isGrounded;
            } 
        }
    }

    private void OnDestroy()
    {
        isGrounded = false;
        MochiManager.Instance.isGroundedSlime = isGrounded;
    }

    //void SendInertiaValue()
    //{
    //    Debug.Log("InertiaSlime");
    //    MochiManager.Instance.inertia = centerSlimeRb.velocity;
    //}

}
