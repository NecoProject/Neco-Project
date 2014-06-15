using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	public Vector2 speed = new Vector2(50, 50);

	private Vector2 movement;
	
	void Update()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY);
		
	}
	
	void FixedUpdate()
	{
		rigidbody2D.velocity = movement;
	}
}