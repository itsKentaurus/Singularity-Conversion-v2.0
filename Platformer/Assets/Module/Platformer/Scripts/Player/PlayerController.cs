using UnityEngine;
using System.Collections;

public class PlayerController : CharacterPhysics {
	
	public float gravity = 20;
	public float speed = 8;
	public float jumpHeight = 12;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;

	protected override void Update () 
	{
		base.Update();

		currentSpeed = Input.GetAxisRaw("Horizontal") * speed;

		if (m_IsGrounded) {
			amountToMove.y = 0;
			
			// Jump
			if (Input.GetButtonDown("Jump")) {
				amountToMove.y = jumpHeight;	
			}
		}
		
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		Move(amountToMove * Time.deltaTime);
	}

	public virtual void Reset()
	{

	}
}
