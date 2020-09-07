using Godot;
using System;

// System.Diagnostics.Debug.WriteLine("this console logs");

public class Player : KinematicBody2D
{
	// [Export] public int score = 0;

   	public const float topSpeed = 660.0f;
   	public const float accel = 60f;
   	public const float decel = 40f;

   	public const float jumpForce = 500f;
   	public const float gravity = 1100.0f;
  	public const float maxFallSpeed = 4000.0f;

	// public float currentJumpForce = 0f;
	public float currentAccel = 0f;
	public bool facingRight = true;
	public bool isCrouching = false;

	// public string currentWeapon = "sword";
	public bool canFire = true;
	public bool attackAnim = false;

	Vector2 velocity;

//	public void Ready()
//	{
//	}

	public override void _Process(float delta)
	{
		MoveInputCheck();
		ActionInputCheck();
		ColliderCheck();
		// HitBoxAnimator();
		Animate();
	}


	public override void _PhysicsProcess(float delta)
	{
		velocity = MoveAndSlide(velocity, new Vector2(0, -1));
		
		//	--- gravity --- //
		if (velocity.y < maxFallSpeed) {
			velocity.y += gravity * delta;
		}
		// System.Diagnostics.Debug.WriteLine(velocity); // <== Console log <== Console log <== Console log <== Console log <== Console log <== Console log <== Console log <== Console log  		
	}


	public void ColliderCheck()
	{
		// change weapon collider
		Area2D wpnArea2D = (Area2D)GetNode("wpn_area_2D");
		CollisionShape2D hitbox = (CollisionShape2D)GetNode("hitbox");

		if (facingRight) {
			wpnArea2D.Scale = new Vector2 (1, 1);
		} else {
			wpnArea2D.Scale = new Vector2 (-1, 1);
		}

		if (isCrouching) {
			hitbox.Scale = new Vector2 (1, 0.75f);
			hitbox.Position = new Vector2 (0, 28);
		} else {
			hitbox.Scale = new Vector2 (1, 1);
			hitbox.Position = new Vector2 (0, 10);
		}
	}


	public void MoveInputCheck()
	{
		// Jumping
		// lets check to see if velocity.y is positive - add a little extra jump force if so. Basically
		// you don't want to be able to add upward jump force if you are  falling.
		// oorrr DOUBLE JUMP?!!?
		if (Input.IsActionPressed("jump") && IsOnFloor()) {
			velocity.y -= jumpForce;
		}

		// Crouching
		if (Input.IsActionPressed("crouch") && IsOnFloor()) {
			isCrouching = true;
		} else {
			isCrouching = false;
		}

		// Moving left and right
		if (Input.IsActionPressed("move_right")) {
			if (!facingRight) {
				velocity.x = 0;
				facingRight = true;
			}
			
			currentAccel = accel; // eventually write function that adds curve instead of just assigning?

			if (velocity.x < topSpeed && !isCrouching) {
				velocity.x += currentAccel;
			} else if (isCrouching) {
				VelocityToZero(.5f);
			}
		} else if (Input.IsActionPressed("move_left")) {
			if (facingRight) {
				velocity.x = 0;
				facingRight = false;
			}
			
			currentAccel = accel; // eventually write function that adds curve instead of just assigning?
			
			if (velocity.x > -topSpeed && !isCrouching) {
				velocity.x -= currentAccel;
			} else if (isCrouching) {
				VelocityToZero(.5f);
			}

		} else {
			VelocityToZero(1f);
		}
	}


	public void VelocityToZero(float modifier)
	{
		currentAccel = decel * modifier; // eventually write function that adds curve instead of just assigning?

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


	public void ActionInputCheck()
	{
		Timer cooldownTimer = (Timer)GetNode("timer_cooldown");

		if (cooldownTimer.IsStopped()) {
			canFire = true;
		}

		if (Input.IsActionPressed("attack") && canFire == true) {
			Attack();
			cooldownTimer.Start();
			canFire = false;
		} else if (Input.IsActionJustReleased("attack") && cooldownTimer.IsStopped()) {
			canFire = true;
		}
	}


	public void Attack()
	{
		attackAnim = true;
		System.Diagnostics.Debug.WriteLine("SWORD ATTACK"); // <== Console log <== Console log <== Console log <== Console log <== Console log <== Console log <== Console log <== Console log  		
	}


	public void Animate()
	{
		AnimatedSprite sprite = (AnimatedSprite)GetNode("sprite");

		if (attackAnim == true) {
			sprite.Play("atk_sword");
			if (sprite.Frame == 3) {
				attackAnim = false;
			}
		} else if (Math.Abs(velocity.x) > 300 && IsOnFloor() && !isCrouching) {
			sprite.Play("run");
		} else if (!IsOnFloor()) {
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


	public void HitBoxAnimator()
	{
		// AnimationPlayer colliderAnimator = (AnimationPlayer)GetNode("colliderAnim");

		// if (isCrouching) {
		// 	colliderAnimator.Play("crouch");
		// } 
		// else {
			// colliderAnimator.play("idle");
		// }
	}
}