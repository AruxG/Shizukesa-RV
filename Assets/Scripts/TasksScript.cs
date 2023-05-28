using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TasksScript : MonoBehaviour
{
    [Space] [SerializeField] private InputActionAsset myActionsAsset;
    [SerializeField] private Material platformMaterial;

    //Teleport Variables
    //Use Y key to init teleport
    //Use T key to confirm teleport
    //Use ←↑↓→ keys to move the teleport point
    private InputAction initTeleport;
    private InputAction confirmTeleport;
    private InputAction moveTeleport;
    private GameObject tpPos;
    //Reference to the main camera transform
    private Transform playerView;
    //Boolean that allows or disables teleport
    private bool tpinited=false;
    void Start()
    {

        initTeleport = myActionsAsset.FindAction("XRI RightHand/StartTeleport");
        confirmTeleport = myActionsAsset.FindAction("XRI RightHand/ConfirmTeleport");
        moveTeleport = myActionsAsset.FindAction("XRI RightHand/MoveTeleport");

        playerView = Camera.main.transform;
    }
    void Update()
    {
        //If input to start teleport is triggered and we aren't already teleporting
        if (initTeleport.triggered&&!tpinited)
        {
            //Create a cylinder
            tpPos = createPlatform();
            tpinited = true;
        }
        //If the teleport has been inited
        if(tpinited)
        {
            //Change the platform position smoothly according to input
            tpPos.transform.position += new Vector3(moveTeleport.ReadValue<Vector2>().x,0, moveTeleport.ReadValue<Vector2>().y)*Time.deltaTime*10;
        }
        //If teleport has been inited and we trigger the finish teleport action
        if (tpinited && confirmTeleport.triggered)
        {
            //Changes our position to the platform one
            transform.position = new Vector3(tpPos.transform.position.x, transform.position.y, tpPos.transform.position.z);
            //Destroy the platform
            GameObject.Destroy(tpPos);
            //Change boolean to disable teleport
            tpinited = false;
        }
    }
    private GameObject createPlatform()
    {
        //Instancia de cilindro
        GameObject ObjectAux=GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //Posición inicial delante del jugador
        Vector3 aux = transform.position + (playerView.forward * 3);
        ObjectAux.transform.position = new Vector3(aux.x, aux.y/3, aux.z);

        //Change its scale so it looks like a platform, and change boolean does allow teleport
        ObjectAux.transform.localScale = new Vector3(1.5f, 0.03f, 1.5f);
        //Cambiamos el tipo de sahder y el color y alpha
        ObjectAux.GetComponent<Renderer>().material=platformMaterial;
        //Añadimos un rigidbody al que bloqueamos la rotación, para poder usar la plataforma a diferentes alturas
        ObjectAux.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //Subimos el collider para poder subirlo uy bajando de diversas alturas
        ObjectAux.GetComponent<CapsuleCollider>().center = new Vector3(ObjectAux.GetComponent<CapsuleCollider>().center.x, 
            ObjectAux.GetComponent<CapsuleCollider>().center.y + 22, ObjectAux.GetComponent<CapsuleCollider>().center.z);
        ObjectAux.layer = 8;
        return ObjectAux;
    }
}
