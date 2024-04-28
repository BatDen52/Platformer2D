using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstantsData
{
    public static class AnimatorParameters
    {
        public static readonly int SpeedX = Animator.StringToHash(nameof(SpeedX));
    }

    public static class Tags
    {
        public const string GROUND_TAG = "Ground";
    }

    public static class InputData
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
    }
}
