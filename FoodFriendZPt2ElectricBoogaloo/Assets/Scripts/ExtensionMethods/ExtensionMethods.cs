using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void AddExplosionForce2D(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir);

    }
}
