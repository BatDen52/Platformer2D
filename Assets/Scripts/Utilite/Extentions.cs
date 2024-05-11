using UnityEngine;

public static class Extentions
{
    public static void Flip(this Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
