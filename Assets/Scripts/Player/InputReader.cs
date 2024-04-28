using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{    
    private bool _isJump;
    private bool _isInterect;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(ConstantsData.InputData.HORIZONTAL_AXIS);

        if (Input.GetKeyDown(KeyCode.W))
            _isJump = true;

        if (Input.GetKeyDown(KeyCode.F))
            _isInterect = true;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInterect);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool lockalValue = value;
        value = false;
        return lockalValue;
    }
}
