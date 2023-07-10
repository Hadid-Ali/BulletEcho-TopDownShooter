using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    [SerializeField] private Color32 m_SphereColor;
    [SerializeField] private float m_SphereRadius;

    [SerializeField] private GizmoShapeType m_GizmoType;
    [SerializeField] private bool m_RenderGizmos = true;

    private void OnDrawGizmos()
    {
        if (!m_RenderGizmos)
            return;

        Gizmos.color = this.m_SphereColor;

        switch (this.m_GizmoType)
        {
            case GizmoShapeType.Sphere:

                Gizmos.DrawSphere(this.transform.position, this.m_SphereRadius);
                break;

            case GizmoShapeType.Cube:
                Gizmos.DrawWireCube(this.transform.position, new Vector3(this.m_SphereRadius, this.m_SphereRadius, this.m_SphereRadius));
                break;
        }
        
    }
}
