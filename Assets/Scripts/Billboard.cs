using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        var cam = Camera.main;
        if (cam != null)
        {
            // Face camera along Y axis only
            Vector3 lookPos = transform.position + cam.transform.rotation * Vector3.forward;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos, Vector3.up);
        }
    }
}
