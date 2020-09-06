using Godot;
using System;

// ctrl + k comments ;)
// System.Diagnostics.Debug.WriteLine("this console logs");

public class Player : KinematicBody2D
{
	[Export] public int score = 0;

	[Export] public float speed = 400.0f;
	[Export] public float dashSpeed = 1.65f;
	
	[Export] public int jumpForce = 500;
	[Export] public float gravity = 800.0f;
	[Export] public float maxFallSpeed = 4000.0f;

	Vector2 velocity;

//	public void Ready()
//	{
//	}

	public void GetInput()
	{
		if (Input.IsActionPressed("move_right")) {
			velocity.x += speed;
		} else if (Input.IsActionPressed("move_left")) {
			velocity.x -= speed;
		} else {
			velocity.x = 0;
		}
		
		if (Input.IsActionPressed("dash")) {
			velocity.x *= dashSpeed;
		}
		
		if (Input.IsActionPressed("jump") && IsOnFloor()) {
			velocity.y -= jumpForce;
		}

//		if (Input.IsActionPressed("dash")) {
//		}

//		if (Input.IsActionPressed("attack")) {
//		}

	}

	public override void _PhysicsProcess(float delta)
	{
		velocity.x = 0;
		GetInput();
		if (Math.Abs(velocity.y) > 30) { 
			velocity.x *= dashSpeed;
		}
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
		
		//	--- gravity --- //
		if (velocity.y < maxFallSpeed) {
			velocity.y += gravity * delta;
		}
		
		System.Diagnostics.Debug.WriteLine(velocity); // <== Console log <== Console log <== Console log <== Console log  
		
		//	--- animations --- //
		if (Math.Abs(velocity.x) > 0 && IsOnFloor()) {
			// set running animation true
			System.Diagnostics.Debug.WriteLine("running");
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


