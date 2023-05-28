using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputActions : MonoBehaviour
{

private InputAction myAction;
[Space][SerializeField] private InputActionAsset myActionsAsset;
void Start()
{
        //B key
    myAction = myActionsAsset.FindAction("XRI LeftHand/MyAction");
}
void Update()
{
    if (myAction.triggered)
    {
        Debug.Log("PULSADO");
    }
}
}
