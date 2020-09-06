using Godot;
using System;

// ctrl + k comments ;)
// System.Diagnostics.Debug.WriteLine("this console logs");

public class Player : KinematicBody2D
{
	// [Export] public int score = 0;

   [Export] public float topSpeed = 660.0f;
   [Export] public float accel = 60f;
   [Export] public float decel = 40f;

   [Export] public float jumpForce = 500f;
   [Export] public float gravity = 1100.0f;
   [Export] public float maxFallSpeed = 4000.0f;

			// public float currentJumpForce = 0f;
			public float currentAccel = 0f;
			public bool facingRight = true;
			public bool isCrouching = false;

	Vector2 velocity;

//	public void Ready()
//	{
//	}

	public void MoveInputCheck()
	{
		// Jumping
		// check to see if velocity.y is positive - add a little extra jump force if so. Basically
		// you don't want to be able to add upward jump force if you are  falling.
		if (Input.IsActionPressed("jump") && IsOnFloor()) {
			velocity.y -= jumpForce;
		}

		if (Input.IsActionPressed("crouch") && IsOnFloor()) {
			isCrouching = true;
		} else {
			isCrouching = false;
		}

		if (isCrouching) {
			velocity.x = 0;
		}

		// moving left and right
		if (Input.IsActionPressed("move_right")) {
			if (!facingRight) {
				velocity.x = 0;
				facingRight = true;
			}
			
			// eventually write function that adds curve?
			currentAccel = accel; 

			if (velocity.x < topSpeed && !isCrouching) {
				velocity.x += currentAccel;
			}
		} else if (Input.IsActionPressed("move_left")) {
			if (facingRight) {
				velocity.x = 0;
				facingRight = false;
			}
			
			// eventually write function that adds curve?
			currentAccel = accel; 
			
			if (velocity.x > -topSpeed && !isCrouching) {
				velocity.x -= currentAccel;
			}

		} else {
			currentAccel = decel;  // leaving this assignment in for eventual curves. Could just be decel
			if (velocity.x < 0) {
				velocity.x += currentAccel;
				if (velocity.x > 0) {
					velocity.x = 0;
				}
			} else if (velocity.x > 0) {
				velocity.x -= currentAccel;
				if (velocity.x < 0) {
					velocity.x = 0;
				}
			}
		}
		// System.Diagnostics.Debug.WriteLine(velocity.x); // <== Console log <== Console log <== Console log <== Console log  
	}


		
	// 	if (Input.IsActionPressed("jump") && IsOnFloor()) {
	// 		velocity.y -= jumpForce;
	// 	}



	public override void _PhysicsProcess(float delta)
	{
		MoveInputCheck();
		
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
		
		//	--- gravity --- //
		if (velocity.y < maxFallSpeed) {
			velocity.y += gravity * delta;
		}
		
		System.Diagnostics.Debug.WriteLine(velocity); // <== Console log <== Console log <== Console log <== Console log  		

		Animate();

	}	


	public void Animate()
	{
		AnimatedSprite sprite = (AnimatedSprite)GetNode("sprite");

		// determine running / jumping
		if (Math.Abs(velocity.x) > 300 && IsOnFloor()) {
			// set running animation true
			sprite.Play("run");
		} else if (Math.Abs(velocity.x) > 0 && !IsOnFloor()) {
			sprite.Play("jump");
		} else if (IsOnFloor() && isCrouching) {
			sprite.Play("crouch");
		} else if (IsOnFloor()) {
			sprite.Play("idle");
		}

		if (facingRight == true) {
			sprite.FlipH = false;
		} else {
			sprite.FlipH = true;
		}
	}
	
//	--- determine what the char is doing and animate based on that --- //
//	public void ActionAnimator()
//	{
//
//	}
	
}


