using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    #region Sphere Variables
    [SerializeField] int speed;
    float movH;
    Rigidbody2D sphereRb;

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

    void Start()
    {
        sphereRb = GetComponent<Rigidbody2D>();
        //sphereRb.velocity = MochiManager.Instance.inertia;    //  Si intento darle la inercia desde aqui no salta... ni idea de porque no.
    }

    // Update is called once per frame
    void Update()
    {
        MovementSphere();
    }

    void MovementSphere()
    {
        movH = Input.GetAxis("Horizontal");
        if (movH != 0)    //  Permite que al soltar el boton, la velocidad no vuelva a 0 sino que decaiga con el drag del Rigidbody
        {
            sphereRb.AddForce(Vector2.right * movH * speed * Time.deltaTime);   //  A?adiendo fuerzas se me hace mas divertido, la esfera es mas rapida pero se controla peor, y el slime lo contrario
            //sphereRb.velocity = new Vector2(movH * speed * Time.deltaTime, sphereRb.velocity.y);    //  Permite el movimiento lateral, si no se le indica el ejeY caera "flotando" y no con su peso real
        }
    }

    //void SendInertiaValue()
    //{
    //    Debug.Log("InertiaSphere");
    //    MochiManager.Instance.inertia = sphereRb.velocity;
    //}

}
