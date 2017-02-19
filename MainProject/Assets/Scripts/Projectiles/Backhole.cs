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
            float angle = Vector3.Angle(m_Projectiles[i].Direction, transform.position - m_Projectiles[i].Position);
            
            float total = m_Collider.radius;
            float distance = Vector3.Distance(m_Projectiles[i].Position, transform.position);
            m_Projectiles[i].Direction = Quaternion.Euler(0f, 0f, angle * distance / total * 0.01f * GetCenterSign(transform.position - m_Projectiles[i].Position)) * m_Projectiles[i].Direction;
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
    private float GetCenterSign(Vector3 directionToCenter)
    {
        if (directionToCenter.x > 0 && directionToCenter.y > 0)
        {
            return 1f;
        }
        else if (directionToCenter.x > 0 && directionToCenter.y < 0)
        {
            return -1f;
        }
        else if (directionToCenter.x < 0 && directionToCenter.y > 0)
        {
            return -1f;
        }
        else if (directionToCenter.x < 0 && directionToCenter.y < 0)
        {
            return 1f;
        }
        return 0f;
    }
    #endregion

}