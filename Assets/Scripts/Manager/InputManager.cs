using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [Header("PC Input")]
    [SerializeField] KeyCode upButton;
    [SerializeField] KeyCode backButton;

    [Header("Mobile Input")]
    [SerializeField] FloatingJoystick joystick;

    public bool ForwardMovement()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if(Input.GetKey(upButton))
            return true;
        else
            return false;
#else
        if (joystick.Vertical.Equals(1f))
            return true;
        else
            return false;
#endif
    }

    public bool IdleMovement()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyUp(upButton))
            return true;
        else
            return false;
#else
        if (joystick.Vertical.Equals(0f))
            return true;
        else
            return false;
#endif
    }

    public bool BackwardMovement()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetKeyUp(backButton))
            return true;
        else
            return false;
#else
        if (joystick.Vertical.Equals(-1f))
            return true;
        else
            return false;
#endif
    }

    public bool ScreenDrag()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButton(0))
            return true;
        else
            return false;
#else
        if (Input.touchCount > 0)
            return true;
        else
            return false;
#endif
    }
}
