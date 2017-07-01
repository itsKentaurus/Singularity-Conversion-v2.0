//
// Script name: BasicProjectile
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

namespace Shooter
{
    public abstract class BasicProjectile : Subject, IProjectile
    {
        #region Variables
        public enum eProjectileEvents
        {
            NONE,
            LAUNCH,
            DESTROYED
        };

        protected float m_Velocity = 0;
        protected Transform m_Target;
        protected Vector3 m_Direction = Vector3.right;

        public Vector3 Direction { get; set; }
        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

		public float Velocity 
		{
			get { return m_Velocity; }
			set { m_Velocity = value; }
		}

        #endregion

        #region Unity API
        #endregion

        #region Public Methods
        public virtual void Launch(Transform target, float velocity)
        {
            m_Target = target;
            m_Velocity = velocity;
            NotifyObservers(eProjectileEvents.LAUNCH);
        }
        #endregion

        #region Protected Methods
        protected virtual void ProjectileDestroyed()
        {
            NotifyObservers(eProjectileEvents.DESTROYED);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}