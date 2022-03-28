using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : Singleton<TouchManager>
{


    private Input inputs;
    private void Awake()
    {
        inputs = new Input();
    }

    private void OnEnable()
    {
        inputs.Touch.Enable();
    }

    private void OnDisable()
    {
        inputs.Touch.Disable();
    }



}
