using UnityEngine;

#if UNITY_EDITOR

[UnityEditor.CustomEditor(typeof(MechaArmatureAimingComponent))]
public class MechaArmatureAimingComponentEditor : UnityEditor.Editor
{
    private MechaArmatureAimingComponent m_ArmatureComponent;

    private void OnEnable()
    {
        m_ArmatureComponent = (MechaArmatureAimingComponent)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Change Aim Position"))
        {
            m_ArmatureComponent.ChangeAimBonePosition();
        }
    }
}
#endif

public class MechaArmatureAimingComponent : MonoBehaviour
{
    [SerializeField] private Transform m_TransformAimingBone;
    [SerializeField] private Transform m_AimingPointTransform;

    [SerializeField] private bool m_X;
    [SerializeField] private bool m_Y;
    [SerializeField] private bool m_Z;

    [SerializeField] private Vector3 m_AimingOffset = Vector3.zero;
    [SerializeField] private Vector3 m_AimingBonePositionOffset = Vector3.zero;

    private Vector3 m_DefaultAimingBoneRotation;
    private Vector3 m_DefaultAimingBonePosition;

    private bool CanAim => m_AimingPointTransform is not null;

    private void OnEnable()
    {
        m_DefaultAimingBoneRotation = m_TransformAimingBone.eulerAngles;
        m_DefaultAimingBonePosition = m_TransformAimingBone.position;
        
        ChangeAimBonePosition();
    }

    public void ChangeAimBonePosition()
    {
        m_TransformAimingBone.position = m_DefaultAimingBonePosition + m_AimingBonePositionOffset;
    }

    public void RegisterAimingPoint(Transform aimingPoint)
    {
        m_AimingPointTransform = aimingPoint;
    }

    void LateUpdate()
    {
        if (!CanAim)
            return;

        m_TransformAimingBone.transform.rotation =
            Quaternion.LookRotation(m_AimingPointTransform.position - m_TransformAimingBone.position);

        Vector3 vAiming = m_TransformAimingBone.eulerAngles;

        m_TransformAimingBone.eulerAngles = new Vector3(
            m_X ? vAiming.x + m_AimingOffset.x : m_DefaultAimingBoneRotation.x + m_AimingOffset.x,
            m_Y ? vAiming.y + m_AimingOffset.y : m_DefaultAimingBoneRotation.y + m_AimingOffset.y,
            m_Z ? vAiming.z + m_AimingOffset.z : m_DefaultAimingBoneRotation.z + +m_AimingOffset.z);

    }
}