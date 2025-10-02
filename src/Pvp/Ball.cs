using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Export]
	public float Speed = 600f;
	public float MaxSpeed = 1500f;
	 private Vector2 _velocity;
	public override void _Ready()
	{
		base._Ready();
		_velocity = new Vector2(1, GD.Randf() * 2 - 1).Normalized() * Speed;
    }

	public override void _PhysicsProcess(double delta)
	{
		if (Position.X > 2100 || Position.X < -150)
		{
			this.Position = new Vector2(1920 / 2, 1080 / 2);
		}

		var collision = MoveAndCollide(_velocity * (float)delta);
		// 如果发生了碰撞
		if (collision != null)
		{
			// 获取碰撞到的对象（比如墙壁、球拍等）
			var other = (StaticBody2D)collision.GetCollider();

			if (other.Name == "PlayerLeft" || other.Name == "PlayerRight")
			{
				HandlePaddleCollision(other, collision);
			}
			else
			{
				// 不是球拍（比如撞到上下墙壁），进行普通反弹
				// Bounce() 方法会根据碰撞法线自动计算反射方向
				_velocity = _velocity.Bounce(collision.GetNormal());
			}

			if (Speed < MaxSpeed)
			{ 
				Speed *= 1.05f;
			}
			
			_velocity = _velocity.Normalized() * Speed;

		}
	}

	private void HandlePaddleCollision(StaticBody2D paddle, KinematicCollision2D collision)
	{
		float hitOffset = (collision.GetPosition().Y - paddle.Position.Y) / 90;
		_velocity = new Vector2(
			-_velocity.X,
			_velocity.Y + hitOffset * 200
		);

    }

}
