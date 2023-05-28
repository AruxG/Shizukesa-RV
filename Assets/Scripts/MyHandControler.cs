using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MyHandControler : MonoBehaviour
{
    [SerializeField] InputActionReference actionGrip;
    private Animator handAnimator;
    private void Awake()
    {
        actionGrip.action.performed += GripPress;
        handAnimator = GetComponent<Animator>();
    }
    private void GripPress(InputAction.CallbackContext obj)
    {
        handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }
}
