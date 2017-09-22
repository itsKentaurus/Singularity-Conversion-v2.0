//
// Script name: Backhole.cs
//
//
// Programmer: Kentaurus
//

using UnityEngine;

using System.Collections;
using System.Collections.Generic;


public class Backhole : MonoBehaviour
{
    #region Variables
	[SerializeField] protected AnimationCurve m_AnimationCurve;
    protected List<Shooter.IProjectile> m_Projectiles = new List<Shooter.IProjectile>();
    protected SphereCollider m_Collider;
    #endregion


    #region Unity API
    protected void Awake()
    {
        if (m_Collider == null)
        {
            m_Collider = GetComponent<SphereCollider>();
        }
    }

    public void Update()
    {
        for (int i = 0; i < m_Projectiles.Count; ++i)
        {
			Vector3 direction = (transform.position - m_Projectiles[i].Position).normalized;
			direction.z = 0;
			Vector3 original = m_Projectiles[i].Direction;
			original.z = 0f;
			direction = Vector3.RotateTowards(original, direction, 50f * Time.deltaTime, 0f).normalized;
			direction.z = 0f;
			m_Projectiles[i].Direction = direction;

			Vector3 distance = (transform.position - m_Projectiles[i].Position);
			distance.z = 0f;
//			m_Projectiles[i].Velocity *= m_AnimationCurve.Evaluate(Mathf.Min(distance.magnitude / m_Collider.radius, 1f));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Shooter.IProjectile>() != null)
        {
            m_Projectiles.Add(other.GetComponent<Shooter.IProjectile>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Shooter.IProjectile>() != null)
        {
            m_Projectiles.Remove(other.GetComponent<Shooter.IProjectile>());
        }
    }
    #endregion


    #region Public Methods
    #endregion


    #region Protected Methods
    #endregion


    #region Private Methods
    #endregion
}