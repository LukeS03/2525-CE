using Godot;
using System;

public partial class Player : CharacterBody2D
{ 
	public const int MaxSpeed = 25;
	
	public const int MaxHealth = 10;
	public const int MaxWater = 10;
	public const int MaxEnergy = 10;
	
	public int CurrentHealth = 10;

	public override void _PhysicsProcess(double delta)
	{
		Move(delta);
	}

	public void Move(double delta)
	{
		var inputVector = Input.GetVector("move_left", "move_right", "move_down", "move_up");
		this.Velocity = inputVector * MaxSpeed;
		MoveAndSlide();
	}
	
	
	
}
