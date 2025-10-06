using Godot;
using System;
using System.Runtime.CompilerServices;

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
	private static CharacterBody2D thisNode;
	public static CharacterBody2D ThisNode
    {
		get
        {
			return thisNode;
        }
    }

	
	
	public override void _Ready()
	{
		base._Ready();
		thisNode = this;
		Speed = DefaultSpeed;
		//获取出界后的显示球
		ShowBall = GetTree().CurrentScene.GetNode<Sprite2D>("ShowBall");
		_velocity = new Vector2(0, 0);
		this.GetTree().CreateTimer(2.0).Timeout += () =>
        {
            _velocity = new Vector2(GD.Randf() < 0.5 ? -1 : 1, GD.Randf() * 2 - 1).Normalized() * Speed;
        };

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
				_velocity = new Vector2(GD.Randf() < 0.5 ? -1 : 1, GD.Randf() * 2 - 1).Normalized() * Speed;
				var timers = this.GetParent().GetNode<Label>("timers");
				ShowBall.Visible = false;
				this.Position = new Vector2(1920 / 2, 1080 / 2);
			};

			//发球提示
			Timers.Show = true;


			if (Position.X > 0) F.removeHigF("RightF");     //右边被进球
			else if (Position.X < 0) F.removeHigF("LeftF"); //左边被进球

			//小比分有结果，上传大比分,重置
			var isBallVet = F.isBallVet();
			if (isBallVet != "null")
            {
				F.addBigF(isBallVet);
				//判断是否有人获胜
				if (F.isGameOver() != "null")
				{
					GD.Print("有人获胜");
					//关闭发球提示
					Timers.Show = false;
					//有人获胜弹出菜单，继续游戏或者退出主页面
					var SystemNode = PvpSystem.getThisNode();
					SystemNode.GetNode<AnimatedSprite2D>("Reload").Visible = true;
					SystemNode.GetNode<TextureButton>("TextureButton").Visible = false;
					SystemNode.Visible = true;
					//胜利
					//目前无需重新赋值，因为仅有的返回界面和重新游戏 都会重新加载整个场景树
					Label l = this.GetTree().CurrentScene.GetNode<Label>("over");
					l.Visible = true;
					if (F.isGameOver() == "LeftF")
					{
						l.GetNode<Label>("left").Visible = true;
					}
					else
					{
						l.GetNode<Label>("right").Visible = true;
					}
				}
				else
				{
					//重置小球
					F.ResetHigF();
				}
			   
					
				
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


    public override void _EnterTree()
    {
		base._EnterTree();
		Timers.Show = false;
		Timers.Timer = 3;
    }

}
