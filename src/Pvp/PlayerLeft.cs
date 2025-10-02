using Godot;
using System;

public partial class PlayerLeft : StaticBody2D
{
	public float Speed = 1500f;
	public float MoveMinPosY;
	public float MoveMaxPosY;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var spri = this.GetNode<Sprite2D>("Pong板");
		var sizeY = spri.Texture.GetSize().Y;

		MoveMinPosY = sizeY / 2;
		MoveMaxPosY = 1080 - sizeY / 2;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var a = Input.GetAxis("Top1", "Buttom1");
		var Vec = new Vector2(Position.X, Position.Y + (a * Speed * (float)delta));

		if (Vec.Y < MoveMinPosY) Vec.Y = MoveMinPosY;
		else if (Vec.Y > MoveMaxPosY) Vec.Y = MoveMaxPosY;

		Position = Vec;



	}
}
