using Godot;
using System;

// ctrl + k comments ;)
// System.Diagnostics.Debug.WriteLine("this console logs");

public class Player : KinematicBody2D
{
	// [Export] public int score = 0;

	[Export] public float currentSpeed = 10f;
	[Export] public float topSpeed = 660.0f;
	[Export] public float accel = 10f;
	[Export] public float decel = 30f;

	[Export] public int jumpForce = 500;
	[Export] public float gravity = 1100.0f;
	[Export] public float maxFallSpeed = 4000.0f;

	Vector2 velocity;

//	public void Ready()
//	{
//	}



	public void MoveInputCheck()
	{
		if (Input.IsActionPressed("move_right")) {
			if (velocity.x < topSpeed) {
				currentSpeed += accel;
				velocity.x += currentSpeed;
			} else {
				System.Diagnostics.Debug.WriteLine("topSpeedMaxed");
				currentSpeed = topSpeed;
				velocity.x = topSpeed;
			}

		} else if (Input.IsActionPressed("move_left")) {
			if (Math.Abs(velocity.x) < topSpeed) {
				currentSpeed += accel;
				velocity.x -= currentSpeed;
			} else {
				System.Diagnostics.Debug.WriteLine("topSpeedMaxed");
				currentSpeed = topSpeed;
				velocity.x = -topSpeed;
			}

		} else {
			if (currentSpeed > 0) {
				currentSpeed -= decel;
			}

			if (velocity.x > 0) {
				velocity.x -= decel;
			} else if (velocity.x < 0) {
				velocity.x += decel;
			}
		}
		
		// if (Input.IsActionPressed("dash")) {
		// 	velocity.x *= 1.65f;
		// }
		
		if (Input.IsActionPressed("jump") && IsOnFloor()) {
			velocity.y -= jumpForce;
		}
	}



	public override void _PhysicsProcess(float delta)
	{
		// velocity.x = 0;
		MoveInputCheck();
		
		// if you are in the air move faster on x axis
//		if (Math.Abs(velocity.y) > 30) {
		if (!IsOnFloor()){ 
			velocity.x *= 1.5f;
		}
		
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
		
		//	--- gravity --- //
		if (velocity.y < maxFallSpeed) {
			velocity.y += gravity * delta;
		}
		
		// System.Diagnostics.Debug.WriteLine(velocity); // <== Console log <== Console log <== Console log <== Console log  
		System.Diagnostics.Debug.WriteLine(currentSpeed); // <== Console log <== Console log <== Console log <== Console log  
		
		//	--- animations --- //
		if (Math.Abs(velocity.x) > 0 && IsOnFloor()) {
			// set running animation true
			// System.Diagnostics.Debug.WriteLine("running");
		}
		if (velocity.x < 0) {
			DetermineDirection(true);
		} else if (velocity.x > 0) {
			DetermineDirection(false);
		}

	}
	


	public void DetermineDirection(bool facingLeft)
	{
		Sprite sprite = (Sprite)GetNode("sprite");
		if (facingLeft == true) {
			sprite.FlipH = true;
		} else {
			sprite.FlipH = false;
		}
	}
	
//	--- determine what the char is doing and animate based on that --- //
//	public void ActionAnimator()
//	{
//
//	}
	
}


