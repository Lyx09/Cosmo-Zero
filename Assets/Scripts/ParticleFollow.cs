using System.Diagnostics;
using UnityEngine;

public sealed class ParticleFollow : MonoBehaviour
{
    private Transform m_cachedTransform;
    private ParticleSystemRenderer m_particleRenderer;
    [SerializeField] private Vector2 m_particleScaleRange;
    [SerializeField] private float m_positionSmooth;
    [SerializeField] private float m_rotatoionSmooth;
    [SerializeField] private Transform m_target;
    [SerializeField] private Rigidbody m_rb;
    [SerializeField] private float SpeedFactor;
    private void Awake()
    {
        m_cachedTransform = transform;
        m_particleRenderer = GetComponent<ParticleSystemRenderer>();
    }

    private void Update()
    {

        

        m_cachedTransform.position = Vector3.Lerp(m_cachedTransform.position,
            m_target.position, Time.deltaTime*m_positionSmooth);

        m_cachedTransform.rotation = Quaternion.Slerp(m_cachedTransform.rotation,
            m_target.rotation, Time.deltaTime*m_rotatoionSmooth);

        Vector3 projected = Vector3.Project(m_rb.velocity, m_rb.transform.forward);
        if (projected.magnitude * SpeedFactor < 0.2)
        {
            m_particleRenderer.maxParticleSize = 0;
        }
        else
        {
            //float rotx = m_rb.velocity.y * 2;
            //float roty = - m_rb.velocity.x * 2;
            //transform.localRotation = Quaternion.Euler(rotx,roty,0);
            if (Vector3.Angle(projected, transform.forward) < 95) //Only works when going forward
            {
                transform.localRotation = Quaternion.Euler(0,0,0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            m_particleRenderer.maxParticleSize = 0.5F;
            m_particleRenderer.lengthScale = Mathf.Lerp(m_particleScaleRange.x,
            m_particleScaleRange.y, m_rb.velocity.magnitude * SpeedFactor);
        }
    }
}