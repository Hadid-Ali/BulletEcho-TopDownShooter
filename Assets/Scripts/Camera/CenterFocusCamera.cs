using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFocusCamera : MonoBehaviour
{
    public Transform focusTransform;
    public float rotationSpeed;



    void Update()
    {
        if (this.focusTransform == null)
            return;

        this.transform.LookAt(this.focusTransform);
        this.transform.RotateAround(this.focusTransform.position, Vector3.up, this.rotationSpeed * Time.deltaTime);
    }
}
