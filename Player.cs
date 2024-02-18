using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public AnimationStatus AnimStatus;

	public const int MaxSpeed = 25;
	
	public const int MaxHealth = 10;
	public const int MaxWater = 10;
	public const int MaxEnergy = 10;
	
	public int CurrentHealth = 10;
	public AnimatedSprite2D sprite;

	public override void _PhysicsProcess(double delta)
	{
		sprite = this.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Move(delta);
		switch (AnimStatus)
		{
			case AnimationStatus.UP:
				sprite.Play("walk_1");
				break;
			case AnimationStatus.DOWN:
				sprite.Play("walk_1");
				break;
			case AnimationStatus.RIGHT:
				sprite.Play("walk_1");
				break;
			case AnimationStatus.LEFT:
				sprite.Play("walk_1");
				break;
			case AnimationStatus.IDLE:
				sprite.Play("idle_1");
				break;
		}
	}

	public void Move(double delta)
	{
		var inputVector = Input.GetVector("move_right", "move_left", "move_up", "move_down");
		this.Velocity = inputVector * MaxSpeed;
		if (Velocity.Y == 0 && Velocity.X == 0) AnimStatus = AnimationStatus.IDLE;
		else if (Velocity.X > 0) AnimStatus = AnimationStatus.UP;
		else if (Velocity.X < 0) AnimStatus = AnimationStatus.DOWN;
		else if (Velocity.Y > 0) AnimStatus = AnimationStatus.RIGHT;
		else if (Velocity.Y < 0) AnimStatus = AnimationStatus.LEFT;
		MoveAndSlide();
	}
	
	
	
}

public enum AnimationStatus
{
	LEFT,
	RIGHT,
	UP,
	DOWN,
	IDLE
};
