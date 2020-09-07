using Godot;
using System;

public class Enemy : KinematicBody2D
{
    // Declare member variables here. Examples:
    private int health = 3;


    // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {
    //
    // }

    public void GetsHit()
    {
        health -= 1;
        if (health <= 0) {
            Die();
        }
    }

    public void Die()
    {
        System.Diagnostics.Debug.WriteLine("I am ded. woe is me"); // logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== 
        // QueueFree();
    }

    public void Attack()
    {
        System.Diagnostics.Debug.WriteLine("attacking!"); // logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== logs <== 
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

//  public override void _PhysicsProcess(float delta)
//  {
//
//  }

}
