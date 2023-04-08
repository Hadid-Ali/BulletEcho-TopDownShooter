using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.

    [HideInInspector]
    public float shakeDuration = 0f;
    public float shakeDurationNormal = 0.87f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public float moveTowardsValue = 15f;

    Vector3 originalPos;

    public vThirdPersonCamera cameraScript;

    void Start()
    {

    }

    public void SetValues(float shakeDuration, float shakeAmount = 0.7f)
    {

    }

    void OnEnable()
    {
        if (this.cameraScript)
            if (this.cameraScript.enabled)
                this.cameraScript.enabled = false;

        this.shakeDuration = this.shakeDurationNormal;

        originalPos = camTransform.position;
    }

    private void OnDisable()
    {
        if (this.cameraScript)
            if (!this.cameraScript.enabled)
                this.cameraScript.enabled = true;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.position = Vector3.MoveTowards(this.transform.position, originalPos + Random.insideUnitSphere * shakeAmount, this.moveTowardsValue * Time.deltaTime);

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            this.enabled = false;
        }
    }
}