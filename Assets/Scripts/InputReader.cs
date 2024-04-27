using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";

    private bool _isJump;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(HORIZONTAL_AXIS);

        if (Input.GetKeyDown(KeyCode.W))
            _isJump = true;
    }

    public bool GetIsJump()
    {
        bool isJump = _isJump;
        _isJump = false;
        return isJump;
    }
}
