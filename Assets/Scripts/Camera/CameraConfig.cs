using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraConfig
{
    public CameraMode mode;
    public float Distance;
    public float Height;
    public float RightOffset;
    
    public bool ApplyRotationConstraints;

    public Vector2 XRotationConstraint;
    public Vector2 YRotationConstraint;

    public float fOV = 60f;
}
