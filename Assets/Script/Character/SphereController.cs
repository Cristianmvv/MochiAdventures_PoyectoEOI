using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    //public static SphereController Instance { get; private set; }

    #region Sphere Variables
    [SerializeField] int speed;
    float movH;
    Rigidbody2D sphereRb;

    public bool isGrounded;

    public Sprite[] caras;
    public SpriteRenderer spriteCara;

    #endregion

    public float maxVelX;

    //  Intento de comunicar la inercia por eventos... fallo mucho
    //private void Awake()
    //{
    //    MochiManager.TakeInertia += SendInertiaValue;
    //}
    //private void OnDisable()
    //{
    //    MochiManager.TakeInertia -= SendInertiaValue;
    //}

    private void Awake()
    {
        //if (Instance != null && Instance != this) Destroy(this);    //  Si al inicio del script tiene no ningun valor o si ya lo tiene, lo destruye
        //else Instance = this;   //  Le a?ade el valor de este propio script

        sphereRb = GetComponent<Rigidbody2D>();
        sphereRb.velocity = MochiManager.Instance.inertia;
    }

    void Update()
    {
        MovementSphere();
    }

    void MovementSphere()
    {
        if (MochiManager.Instance.disableMovement == true) return;
        movH = Input.GetAxis("Horizontal");
        if (movH != 0)    //  Permite que al soltar el boton, la velocidad no vuelva a 0 sino que decaiga con el drag del Rigidbody
        {
            sphereRb.AddForce(Vector2.right * movH * speed * Time.deltaTime);   //  A?adiendo fuerzas se me hace mas divertido, la esfera es mas rapida pero se controla peor, y el slime lo contrario
            //sphereRb.velocity = new Vector2(movH * speed * Time.deltaTime, sphereRb.velocity.y);    //  Permite el movimiento lateral, si no se le indica el ejeY caera "flotando" y no con su peso real

            spriteCara.sprite = caras[0];

            if (sphereRb.velocity.x > maxVelX)
                sphereRb.velocity = new Vector2(maxVelX, sphereRb.velocity.y);
            else if (sphereRb.velocity.x < -maxVelX)
                sphereRb.velocity = new Vector2(-maxVelX, sphereRb.velocity.y);

            if (sphereRb.velocity.magnitude >= 7) spriteCara.sprite = caras[0];
            else spriteCara.sprite = caras[1];
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            MochiManager.Instance.isGroundedSphere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            MochiManager.Instance.isGroundedSphere = false;
        }
    }


    //void RaycastGroundCheck()     //  Testeando los raycast desde aqui que no tenia tantos rigidbodys
    //{
    //    var right45 = (sphereRb.transform.right + sphereRb.transform.up).normalized;
    //    var left45 = (sphereRb.transform.right -sphereRb.transform.up).normalized;



    //    isGrounded = Physics2D.Raycast(sphereRb.transform.position, sphereRb.transform.right, 100);
    //    Debug.DrawRay(sphereRb.transform.position, sphereRb.transform.right* 100, Color.red);
    //    Debug.DrawRay(sphereRb.transform.position, left45 * 100, Color.red);
    //    Debug.DrawRay(sphereRb.transform.position, right45 * 100, Color.red);

    //}

    private void OnDisable()
    {
        isGrounded = false;
        MochiManager.Instance.isGroundedSphere = isGrounded;
    }

    //void SendInertiaValue()
    //{
    //    Debug.Log("InertiaSphere");
    //    MochiManager.Instance.inertia = sphereRb.velocity;
    //}

}
