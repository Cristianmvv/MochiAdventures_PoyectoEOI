using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MochiController : MonoBehaviour
{
    [Header("Formas Slime")]
    [SerializeField] GameObject mochiSlimePf;     //Prefabs para instanciar las formas
    [SerializeField] GameObject mochiSpherePf;
    bool IsSphere;  //  Permite indicar en que estado se encuentra el slime, si en esfera o slime
    GameObject mochiSlime, mochiSphere;  //  GameObjects donde se instanciaran las formas
    [SerializeField]GameObject slimeCenter; //  Hueso central del modo slime para poder instanciar el modo esfera en este punto, necesario porque de base se instanciara donde este el MochiController
    void Start()
    {
        FirstInstantiateMochi();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))    //  Cuando se presione el boton, cambiara de una forma a otra
        {
            if (!IsSphere)
            {
                InstantiateMochiSphere();
                IsSphere = true;
            }
            else
            {
                InstantiateMochiSlime();
                IsSphere = false;
            }
        }
    }
    void FirstInstantiateMochi()    //  Al iniciar la escena, spawnea en la forma slime de forma default
    {
        mochiSlime = Instantiate(mochiSlimePf, transform.position, transform.rotation, transform);    //  Instancia prefab de forma slime en la posicion del MochiController
        mochiSlime.SetActive(true);
        slimeCenter = GameObject.FindGameObjectWithTag("SlimeCenter");  //  Busca el centro del slime con un tag (Cada vez que se destruye o se instancia de 0 es necesario indicarlo)
    }

    void InstantiateMochiSlime()
    {
        mochiSphere.SetActive(false);    //  Desactiva el modo esfera
        mochiSlime = Instantiate(mochiSlimePf, mochiSphere.transform.position, mochiSphere.transform.rotation, transform);  //  Instancia prefab de forma slime en la ubicacion del modo esfera
        Destroy(mochiSphere);    //Destruye el modo esfera
        mochiSlime.SetActive(true);
        slimeCenter = GameObject.FindGameObjectWithTag("SlimeCenter");
    }

    void InstantiateMochiSphere()
    {
        mochiSlime.SetActive(false);    //  Desactiva el modo slime
        mochiSphere = Instantiate(mochiSpherePf, slimeCenter.transform.position, slimeCenter.transform.rotation, transform);    //  Instancia prefab de modo esfera en la ubicacion del centro de forma de forma slime
        Destroy(mochiSlime);
        mochiSphere.SetActive(true);
    }
}
