//
// Script name: BasicLauncher
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

namespace Shooter
{
	public abstract class BasicLauncher : SubjectObserver, ILauncher
	{
		#region Variables
		[SerializeField] protected BasicProjectile m_Projectile;
		[SerializeField] protected float m_LaunchVelocity = 20f;
		[SerializeField] [Range(0, 100f)] protected float m_ReloadTime;
		protected bool m_CanShoot = false;
		protected Timer m_Timer = new Timer();
		protected int m_NumOfProjectilesOut = 0;

		public int NumOfProjectileOut 
		{
			get { return m_NumOfProjectilesOut; }
		}
		#endregion

		#region Unity API
		protected virtual void Update()
		{
			if (m_Timer != null)
			{
				m_Timer.OnUpdate();
			}
		}
		#endregion

		#region Public Methods
		public void Fire(Transform target)
		{
			if (!m_CanShoot)
			{
				OnFireDenial();
				return;
			}

			m_CanShoot = m_ReloadTime == 0f;

			m_Timer.StartTimer(m_ReloadTime, OnReloadCOmplete, true);
			CreateProjectiles(target);
		}
		#endregion

		#region Protected Methods
		protected virtual void CreateProjectiles(Transform target)
		{
			BasicProjectile projectile = Instantiate(m_Projectile);
			projectile.transform.position = transform.position;
			projectile.RegisterObserver(this);
			projectile.Launch(target, m_LaunchVelocity);
			ProjectileManager.Instance.AddProjectile(projectile);
		}

		protected virtual void OnReloadCOmplete()
		{
			m_CanShoot = true;
		}

		protected virtual void OnFireDenial()
		{
			// STUB
		}

		protected virtual void OnProjectileLaunched()
		{
			++m_NumOfProjectilesOut;
		}

		protected virtual void OnProjectileDestroyed()
		{
			--m_NumOfProjectilesOut;
		}
		#endregion

		#region Private Methods
		#endregion

		#region SubjectObserver
		public override void OnNotify(ISubject subject, params object[] args)
		{
			base.OnNotify(subject, args);
			if (subject is BasicProjectile)
			{
				switch((BasicProjectile.eProjectileEvents)args[0])
				{
					case BasicProjectile.eProjectileEvents.DESTROYED:
					OnProjectileDestroyed();
					break;
					case BasicProjectile.eProjectileEvents.LAUNCH:
					OnProjectileLaunched();
					break;
				}
			}
		}
		#endregion
	}
}