﻿using UnityEngine;
using System.Collections;
using Invector;
using Cinemachine;
using UnityAtoms.BaseAtoms;

public enum CameraMode
{
    Orbit,
    TopDown
}

public class vThirdPersonCamera : CameraController
{
    private static vThirdPersonCamera _instance;

    public Vector3 lockCameraOffset;
    public Vector3 normalCameraOffset;

    #region inspector properties

    public Transform target;
    [Tooltip("Lerp speed between Camera States")]
    public float smoothCameraRotation = 12f;
    [Tooltip("What layer will be culled")]
    public LayerMask cullingLayer = 1 << 0;
    [Tooltip("Debug purposes, lock the camera behind the character for better align the states")]
    public bool lockCamera;

    public float rightOffset = 0f;
    public float defaultDistance = 2.5f;
    public float height = 1.4f;
    public float smoothFollow = 10f;
    public float xMouseSensitivity = 3f;
    public float yMouseSensitivity = 3f;
    public float yMinLimit = -40f;
    public float yMaxLimit = 80f;

    #endregion

    public Vector2Event CameraInput_Event;
    
    #region hide properties

    [HideInInspector]
    public int indexList, indexLookPoint;
    [HideInInspector]
    public float offSetPlayerPivot;
    [HideInInspector]
    public string currentStateName;
    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public Vector2 movementSpeed;

    private Transform targetLookAt;
    private Vector3 currentTargetPos;
    private Vector3 lookPoint;
    private Vector3 current_cPos;
    private Vector3 desired_cPos;
    private Vector3 RotationConstraint;

    [SerializeField]
    private Vector3 finalXVector;
    [SerializeField]
    private Vector3 finalYVector;
    private Vector3 CamPosition;
    private CinemachineVirtualCamera m_CinemachineVirtualCamera;
    private float distance = 5f;
    private float mouseY = 0f;
    private float mouseX = 0f;
    private float currentHeight;
    private float cullingDistance;
    private float checkHeightRadius = 0.4f;
    private float clipPlaneMargin = 0f;
    private float forward = -1f;
    private float cullingHeight = 0.2f;
    private float cullingMinDist = 0.1f;
    [SerializeField]
    private bool ApplyPosition;
    private CameraMode mode;
    private Camera m_Camera;

    #endregion

    void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        CameraInput_Event.Register(CameraInput);
    }

    private void OnDisable()
    {
        CameraInput_Event.Unregister(CameraInput);
    }

    void CameraInput(Vector2 vector)
    {
        RotateCamera(vector.x, vector.y);
    }

    private bool isCentered = false;

    public override void LockCamera(bool toggle)
    {
        this.lockCamera = toggle;
    }

    public override void SetCameraHeight(float height)
    {
        this.height = height;
    }

    public override void FixCentering(float point = 1f)
    {
        this.transform.position += new Vector3(0f, point, 0f);
        this.lockCamera = true;
        this.isCentered = true;
        Invoke("RestoreCamera", 0.5f);
    }

    private void RestoreCamera()
    {
        this.LockCamera(false);
    }

    public void Init()
    {
        if (target == null)
            return;

        m_CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        m_Camera = Camera.main;
        currentTarget = target;
        currentTargetPos = new Vector3(currentTarget.position.x, currentTarget.position.y + offSetPlayerPivot, currentTarget.position.z);

        targetLookAt = new GameObject("targetLookAt").transform;
        targetLookAt.position = currentTarget.position;
        targetLookAt.hideFlags = HideFlags.HideInHierarchy;
        targetLookAt.rotation = currentTarget.rotation;     

        mouseY = 15;
        mouseX = currentTarget.eulerAngles.y;

        distance = defaultDistance;
        currentHeight = height;
    }

    bool shiftFov = false;
    float targetFov;
    public float valueShiftRate = 1f;

    private void Update()
    {
        if(this.shiftFov)
        {
            m_CinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp( m_CinemachineVirtualCamera.m_Lens.FieldOfView, this.targetFov, this.valueShiftRate * Time.deltaTime);
            if ( m_CinemachineVirtualCamera.m_Lens.FieldOfView== this.targetFov)
                this.shiftFov = false;
        }
    }

    public override void ApplyConfig(CameraConfig configuration)
    {
        this.defaultDistance = configuration.Distance;
        this.height = configuration.Height;
        this.rightOffset = configuration.RightOffset;

        this.targetFov = configuration.fOV;
        this.shiftFov = true;
       
        this.yMinLimit = configuration.YRotationConstraint.x;
        this.yMaxLimit = configuration.YRotationConstraint.y;
        
        // if (!AnglesCalculated)
        // {
        //     Vector3 eulerangle = this.transform.eulerAngles;
        //
        //     if (configuration.ApplyRotationConstraints)
        //     {
        //         this.xMinLimit = configuration.XRotationConstraint.x + this.transform.eulerAngles.y;
        //         this.xMaxLimit = configuration.XRotationConstraint.y + this.transform.eulerAngles.y;
        //     }
        //     else
        //     {
        //
        //     }
        //     AnglesCalculated = true;
        // }
    }

    public IEnumerator GetCameraPosition()
    {
        yield return new WaitForSeconds(2f);
        this.CamPosition = this.transform.position;
        this.ApplyPosition = true;
    }

    public bool AnglesCalculated = false;

    void LateUpdate()
    {
        if (target == null || targetLookAt == null)
            return;

        CameraMovement();
    }

    /// <summary>
    /// Set the target for the camera
    /// </summary>
    /// <param name="New cursorObject"></param>
    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget ? newTarget : target;
        this.FixCentering();
    }

    public override void SetMainTarget(Transform newTarget)
    {
        target = newTarget;
        currentTarget = newTarget;

        if (newTarget == null)
            return;

        mouseY = currentTarget.rotation.eulerAngles.x;
        mouseX = currentTarget.rotation.eulerAngles.y;
        Init();
    }


    /// <summary>    
    /// Convert a point in the screen in a Ray for the world
    /// </summary>
    /// <param name="Point"></param>
    /// <returns></returns>
    public Ray ScreenPointToRay(Vector3 Point)
    {
        return this.GetComponent<Camera>().ScreenPointToRay(Point);
    }


    /// <summary>
    /// Camera Rotation behaviour
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void RotateCamera(float x, float y)
    {
        float speed =  1;
        x *= speed;
        y *= speed;
        // free rotation 
        mouseX += x * xMouseSensitivity;
        mouseY -= y * yMouseSensitivity;

        movementSpeed.x = x;
        movementSpeed.y = -y;
        if (!lockCamera)
        {
            mouseY = vExtensions.ClampAngle(mouseY + this.normalCameraOffset.y, yMinLimit, yMaxLimit);
            mouseX = mouseX + this.normalCameraOffset.x;
        }
        else
        {
            mouseY = currentTarget.root.localEulerAngles.x + this.lockCameraOffset.x;
            mouseX = currentTarget.root.localEulerAngles.y + this.lockCameraOffset.y;
        }

//        if (this.ApplyRotationContraint)
//        {
//            this.transform.eulerAngles = new Vector3(Mathf.Clamp(this.transform.eulerAngles.x, this.finalXVector.x, this.finalXVector.y), Mathf.Clamp(this.transform.eulerAngles.y, this.finalYVector.x, this.finalYVector.y), this.transform.eulerAngles.z);
//            if (this.ApplyPosition & this.CamPosition != Vector3.zero)
//                this.transform.position = this.CamPosition;
//        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        if (angle > 180)
            angle = 360 - angle;
        angle = Mathf.Clamp(angle, from, to);
        if (angle < 0)
            angle = 360 + angle;

        return angle;
    }

    /// <summary>
    /// Camera behaviour
    /// </summary>    
    void CameraMovement()
    {
        if (currentTarget == null)
            return;

        distance = Mathf.Lerp(distance, defaultDistance, smoothFollow * Time.deltaTime);
        //m_CinemachineVirtualCamera.fieldOfView = fov;
        cullingDistance = Mathf.Lerp(cullingDistance, distance, Time.deltaTime);
        var camDir = (forward * targetLookAt.forward) + (rightOffset * targetLookAt.right);

        camDir = camDir.normalized;

        var targetPos = new Vector3(currentTarget.position.x, currentTarget.position.y + offSetPlayerPivot, currentTarget.position.z);
        currentTargetPos = targetPos;
        desired_cPos = targetPos + new Vector3(0, height, 0);
        current_cPos = currentTargetPos + new Vector3(0, currentHeight, 0);
        RaycastHit hitInfo;
        

        ClipPlanePoints planePoints = m_Camera.NearClipPlanePoints(current_cPos + (camDir * (distance)), clipPlaneMargin);
        ClipPlanePoints oldPoints = m_Camera.NearClipPlanePoints(desired_cPos + (camDir * distance), clipPlaneMargin);

        //Check if Height is not blocked 
        if (Physics.SphereCast(targetPos, checkHeightRadius, Vector3.up, out hitInfo, cullingHeight + 0.2f, cullingLayer))
        {
            var t = hitInfo.distance - 0.2f;
            t -= height;
            t /= (cullingHeight - height);
            cullingHeight = Mathf.Lerp(height, cullingHeight, Mathf.Clamp(t, 0.0f, 1.0f));
        }

        //Check if desired target position is not blocked       
        if (CullingRayCast(desired_cPos, oldPoints, out hitInfo, distance + 0.2f, cullingLayer, Color.blue))
        {
            distance = hitInfo.distance - 0.2f;
            if (distance < defaultDistance)
            {
                var t = hitInfo.distance;
                t -= cullingMinDist;
                t /= cullingMinDist;
                currentHeight = Mathf.Lerp(cullingHeight, height, Mathf.Clamp(t, 0.0f, 1.0f));
                current_cPos = currentTargetPos + new Vector3(0, currentHeight, 0);
            }
        }
        else
        {
            currentHeight = height;
        }
        //Check if target position with culling height applied is not blocked
        if (CullingRayCast(current_cPos, planePoints, out hitInfo, distance, cullingLayer, Color.cyan))
            distance = Mathf.Clamp(cullingDistance, 0.0f, defaultDistance);
        var lookPoint = current_cPos + targetLookAt.forward * 2f;
        lookPoint += (targetLookAt.right * Vector3.Dot(camDir * (distance), targetLookAt.right));
        targetLookAt.position = current_cPos;

        Quaternion newRot = Quaternion.Euler(mouseY, mouseX, 0);
        targetLookAt.rotation = Quaternion.Slerp(targetLookAt.rotation, newRot, smoothCameraRotation * Time.deltaTime);
        transform.position = current_cPos + (camDir * (distance));
        var rotation = Quaternion.LookRotation((lookPoint) - transform.position);

        //lookTargetOffSet = Vector3.Lerp(lookTargetOffSet, Vector3.zero, 1 * Time.fixedDeltaTime);

        //rotation.eulerAngles += rotationOffSet + lookTargetOffSet;
        Vector3 eulerAng = rotation.eulerAngles;
        Vector3 neweulerAngles = eulerAng;
//        if (this.ApplyRotationContraint)
//            neweulerAngles = new Vector3(Mathf.Clamp(eulerAng.x, this.finalXVector.x, this.finalXVector.y), Mathf.Clamp(eulerAng.y, this.finalYVector.x, this.finalYVector.y), eulerAng.z);
        this.transform.eulerAngles = neweulerAngles;
        //transform.rotation = rotation;
        // movementSpeed = Vector2.zero;

//        if (this.ApplyRotationContraint)
//            this.transform.eulerAngles = new Vector3(Mathf.Clamp(this.transform.eulerAngles.x, this.finalXVector.x, this.finalXVector.y), Mathf.Clamp(this.transform.eulerAngles.y, this.finalYVector.x, this.finalYVector.y), this.transform.eulerAngles.z);
    }


    /// <summary>
    /// Custom Raycast using NearClipPlanesPoints
    /// </summary>
    /// <param name="_to"></param>
    /// <param name="from"></param>
    /// <param name="hitInfo"></param>
    /// <param name="distance"></param>
    /// <param name="cullingLayer"></param>
    /// <returns></returns>
    bool CullingRayCast(Vector3 from, ClipPlanePoints _to, out RaycastHit hitInfo, float distance, LayerMask cullingLayer, Color color)
    {
        bool value = false;

        if (Physics.Raycast(from, _to.LowerLeft - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            cullingDistance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.LowerRight - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            if (cullingDistance > hitInfo.distance)
                cullingDistance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.UpperLeft - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            if (cullingDistance > hitInfo.distance)
                cullingDistance = hitInfo.distance;
        }

        if (Physics.Raycast(from, _to.UpperRight - from, out hitInfo, distance, cullingLayer))
        {
            value = true;
            if (cullingDistance > hitInfo.distance)
                cullingDistance = hitInfo.distance;
        }

        return value;
    }

    public void SetTransform(Transform trans)
    {
        transform.position = trans.position;
        transform.rotation = trans.rotation;
    }
}
