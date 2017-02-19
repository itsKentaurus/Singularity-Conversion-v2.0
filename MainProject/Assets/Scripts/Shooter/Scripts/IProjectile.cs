//
// Script name: IProjectile
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections;

namespace Shooter
{
	public interface IProjectile
	{
        Vector3 Direction { get; set; }
        Vector3 Position { get; set; }
	}
}