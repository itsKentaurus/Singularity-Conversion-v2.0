//
// Script name: Projectile.cs
//
//
// Programmer: Kentaurus
//

using UnityEngine;

using System.Collections;



public class Projectile : MonoBehaviour, Shooter.IProjectile
{
    #region Variables
    [SerializeField] protected Vector3 m_Direction = Vector3.right;

    public Vector3 Direction
    {
        get { return m_Direction;  }
        set { m_Direction = value; }
    }
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
    #endregion


    #region Unity API
    protected void Update()
    {
        transform.position += Direction;
    }
    #endregion


    #region Public Methods
    #endregion


    #region Protected Methods
    #endregion


    #region Private Methods
    #endregion

}