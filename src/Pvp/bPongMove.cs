using Godot;
using System;
using System.Security.AccessControl;

public partial class bPongMove : StaticBody2D
{
	//移动速度
	public float MinSpeed = 1500f;
	public float MaxSpeed = 2000f;
	public float Acceleration = 0;
	public float MoveY = 0;
	[Export]
	public string MoveActionTop;
	[Export]
	public string MoveActionButtom;
	//最低y移动值
	public float MoveMinPosY;

	//最高y移动值
	public float MoveMaxPosY;

	public int isMove=0;
	public override void _Ready()
	{
		GD.Print(this.Name);

		//相对获球拍大小，相对为移动限制赋值
		var spri = this.GetNode<Sprite2D>("Pong板");
		var sizeY = spri.Texture.GetSize().Y;
		MoveMinPosY = sizeY / 2;
		MoveMaxPosY = 1080 - sizeY / 2;

	}

	public override void _Process(double delta)
	{
		float isMove = 0;
		if (this.Name == "PlayerRight" && PvpSystem.mode == Mode.PVE)
		{
			//ai逻辑赋值

			isMove = AI.Link(this,(float)GetProcessDeltaTime());
		}
		else//如果模式是pvp，右边pong板，就由玩家输入逻辑
		{
			
			isMove = Input.GetAxis(MoveActionTop, MoveActionButtom);
		}


		if (isMove != 0)
		{
			Acceleration += (float)delta * 2;
			MoveY = isMove * MinSpeed * (float)delta * Acceleration;


			//限制速度
			if (MoveY > MaxSpeed * delta || MoveY < -MaxSpeed * delta)
			{
				MoveY = isMove * MaxSpeed * (float)delta;
				Acceleration = MaxSpeed / MinSpeed;
			}
		}
		else
		{

			//当停止操作时，缓慢减速
			Acceleration -= (float)delta;
			if (Acceleration <= 0) Acceleration = 0;

			if (MoveY > 0)
			{
				MoveY = MinSpeed * (float)delta * Acceleration;
			}
			else if (MoveY < 0)
			{
				MoveY = -1 * MinSpeed * (float)delta * Acceleration;
			}


		}

		var Vec = new Vector2(Position.X, Position.Y + MoveY);

		//移动超边界判定
		if (Vec.Y < MoveMinPosY) Vec.Y = MoveMinPosY;
		else if (Vec.Y > MoveMaxPosY) Vec.Y = MoveMaxPosY;

		Position = Vec;
	}


	

	

}
