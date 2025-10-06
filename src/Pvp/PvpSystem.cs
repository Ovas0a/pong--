using Godot;
using System;

public partial class PvpSystem : Sprite2D
{
	private static Sprite2D ThisNode;
	public static Mode mode;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		ThisNode = this;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private bool down = false;
	public override void _Process(double delta)
	{



		var iskey = Input.IsKeyPressed(Key.Escape);


		if (iskey && down == false&&this.GetNode<TextureButton>("TextureButton").Visible) this.Visible = !Visible;

		down = iskey;


		if (Visible) this.GetTree().Paused = true;
		else this.GetTree().Paused = false;

	}

	public static Sprite2D getThisNode()
    {
		return ThisNode;
    }

	
	


}
