using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiManager : MonoBehaviour
{
    public static MochiManager Instance { get; private set; }

    #region Eventos
    //public delegate void TakeInertiaValue();
    //public static event TakeInertiaValue TakeInertia;
    #endregion

    #region Variables
    [Header("Formas Slime")]
    [SerializeField] GameObject mochiSlimePf;     //Prefabs para instanciar las formas
    [SerializeField] GameObject mochiSpherePf;
    public GameObject slimeCenter; //  Hueso central del modo slime para poder instanciar el modo esfera en este punto, necesario porque de base se instanciara donde este el MochiController
    GameObject mochiSlime, mochiSphere;  //  GameObjects donde se instanciaran las formas
    public bool IsSphere;  //  Permite indicar en que estado se encuentra el slime, si en esfera o slime
    public bool disableMovement;
    public bool disableTransform;

    [Header("Salto")]
    public float jumpForce;
    float jumpTime;
    [SerializeField] bool isGrounded;
    public bool isGroundedSphere;
    public bool isGroundedSlime;

    [Header("Animacion")]
    private Animator anim;

    public Vector2 inertia;

    AudioSource audioS;
    public AudioClip audioJump, audioForm;

    public bool inForceZone;

    public bool caraMochi;
    #endregion


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);    //  Si al inicio del script tiene no ningun valor o si ya lo tiene, lo destruye
        else Instance = this;   //  Le a?ade el valor de este propio script
        audioS = GetComponent<AudioSource>();
    }

    void Start()
    {
        FirstInstantiateMochi();
        //anim.GetComponent<Animator>();
    }

    void Update()
    {
        //if (SphereController.Instance == null) isGrounded = CheckGrounded();
        if (IsSphere) isGrounded = CheckGrounded();
        else isGrounded = CheckGrounded(isGroundedSphere);
        //anim.SetBool("isGrounded", isGrounded);
        if (!disableTransform) ChangeForms();
    }

    void ChangeForms()
    {
        if (disableTransform) return;

        if (Input.GetKey(KeyCode.W))    //  Cuando se mantenga pulsado el boton
        {
            if (!IsSphere) caraMochi = true;
            jumpTime += Time.deltaTime; //  Empezara un contador
            if (jumpTime >= 0.2f && IsSphere)   //  Si se pulsa por mas de 0.2seg Y ES ESFERA cambiara a forma de slime
            {
                InstantiateMochiSlime();
                caraMochi = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.W))  //  Al levantar el boton
        {
            if (jumpTime <= 0.2f)   //  Si se a levantado antes de 0.2seg (un pulso rapido) cambiara de estado
            {
                if (!IsSphere)
                {
                    InstantiateMochiSphere();
                }
                else
                {
                    InstantiateMochiSlime();
                }
                caraMochi = false;
            }
            else if (isGrounded)    //  Si se a levantado despues de 0.2seg (mantenido pulsado) Y ESTA TOCANDO EL SUELO realizara el salto
            {
                InstantiateMochiSphere();
                if (mochiSphere.GetComponent<Rigidbody2D>().velocity.y < 0)
                    mochiSphere.GetComponent<Rigidbody2D>().velocity = new Vector2(mochiSphere.GetComponent<Rigidbody2D>().velocity.x, 0);
                mochiSphere.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  //  El salto de la esfera
                audioS.clip = audioJump;
                audioS.Play();
                Debug.Log("Salto");
            }
            else   //   Si se a levantado despues de 0.2seg (mantenido pulsado) Y NO ESTA TOCANDO EL SUELO cambiara a esfera
            {
                InstantiateMochiSphere();
            }

            jumpTime = 0;   //  Reinicia el contador
        }
    }

    void FirstInstantiateMochi()    //  Al iniciar la escena, spawnea en la forma slime de forma default
    {
        mochiSlime = Instantiate(mochiSlimePf, transform.position, transform.rotation, transform);    //  Instancia prefab de forma slime en la posicion del MochiController
        mochiSlime.SetActive(true);
        slimeCenter = GameObject.FindGameObjectWithTag("SlimeCenter");  //  Busca el centro del slime con un tag (Cada vez que se destruye o se instancia de 0 es necesario indicarlo)
    }

    public void InstantiateMochiSphere()
    {
        audioS.clip = audioForm;
        audioS.Play();
        IsSphere = true;
        //if (TakeInertia!= null) TakeInertia();
        inertia = slimeCenter.GetComponent<Rigidbody2D>().velocity; //  Envia la informacion de la inercia del modo slime a la variable
        //mochiSlime.SetActive(false);    //  Desactiva el modo slime
        mochiSphere = Instantiate(mochiSpherePf, slimeCenter.transform.position, slimeCenter.transform.rotation, transform);    //  Instancia prefab de modo esfera en la ubicacion del centro de forma de forma slime
        Destroy(mochiSlime);
        mochiSphere.GetComponent<SphereController>().spriteCara.sprite = mochiSphere.GetComponent<SphereController>().caras[2];
        //mochiSphere.SetActive(true);
        //mochiSphere.GetComponent<Rigidbody2D>().velocity = inertia; //  Recoje la informacion de la inercia del modo slime que estaba guardada en la variable
    }

    public void InstantiateMochiSlime()
    {
        audioS.clip = audioForm;
        audioS.Play();
        IsSphere = false;
        //if (TakeInertia != null) TakeInertia();
        inertia = mochiSphere.GetComponent<Rigidbody2D>().velocity; //  Envia la informacion de la inercia del modo esfera a la variable
        //mochiSphere.SetActive(false);    //  Desactiva el modo esfera
        mochiSlime = Instantiate(mochiSlimePf, mochiSphere.transform.position, mochiSphere.transform.rotation, transform);  //  Instancia prefab de forma slime en la ubicacion del modo esfera
        Destroy(mochiSphere);    //Destruye el modo esfera
        //mochiSlime.SetActive(true);
        slimeCenter = GameObject.FindGameObjectWithTag("SlimeCenter");
        mochiSlime.GetComponent<SoftBodyController>().spriteCara.sprite = mochiSlime.GetComponent<SoftBodyController>().caras[2];   
        //slimeCenter.GetComponent<Rigidbody2D>().velocity = inertia;   //  Para poder meterle la inercia a todos los RigidBody ahora lo coje desde su controller, ?Porque no lo hago igual con la esfera? porque si lo hago igual no salta la esfera, no pregunteis, no tengo ni idea.
    }

    bool CheckGrounded(bool _sphere = false)
    {
        return isGroundedSlime || _sphere;
    }
}
