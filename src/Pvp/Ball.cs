using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Export]
	public float DefaultSpeed;
	public float Speed;
	[Export]
	public float MaxSpeed;
	private Vector2 _velocity;
	private Sprite2D ShowBall;

	private Group_F F;


	
	public override void _Ready()
	{
		base._Ready();
		Speed = DefaultSpeed;
		//获取出界后的显示球
		ShowBall=GetTree().CurrentScene.GetNode<Sprite2D>("ShowBall");
		_velocity = new Vector2(GD.Randf() < 0.5 ? -1 : 1, GD.Randf() * 2 - 1).Normalized() * Speed;

		F = new Group_F(GetTree().CurrentScene.GetNode<Label>("BallF"));
    }

	public override void _PhysicsProcess(double delta)
	{
		//进球操作
		if ((Position.X > 2100 || Position.X < -150) && ShowBall.Visible == false)
		{
			Speed = DefaultSpeed;
			ShowBall.Visible = true;
			GetTree().CreateTimer(3.0).Timeout += () =>
			{
				ShowBall.Visible = false;
				this.Position = new Vector2(1920 / 2, 1080 / 2);
			};


			if (Position.X > 0) F.removeHigF("RightF");     //右边被进球
			else if (Position.X < 0) F.removeHigF("LeftF"); //左边被进球

			//小比分有结果，上传大比分,重置
			var isBallVet = F.isBallVet();
			if (isBallVet != "null")
            {
				F.addBigF(isBallVet);
				F.ResetHigF();
            }
			





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
				GD.Print("速度：" + Speed);
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
