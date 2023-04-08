using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    public abstract void SetMainTarget(Transform target);
    public abstract void LockCamera(bool toggle);
    public abstract void SetCameraHeight(float height);
    public abstract void FixCentering(float height);
    public abstract void ApplyConfig(CameraConfig config);

    public void SetEnabled(bool enable)
    {
        enabled = enable;
    }
    
    public virtual void OnCameraLive()
    {
        SetEnabled(true);
    }
}
