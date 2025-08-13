using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadowsOff : MonoBehaviour
{
    void Start()
    {
        // Get all MeshRenderer components in children (including inactive)
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>(true);

        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }
}
