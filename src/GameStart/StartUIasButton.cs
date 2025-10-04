using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class StartUIasButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
		var ani = this.GetParent<AnimatedSprite2D>();
		if (ani.Frame == 6)
        {
			ani.Stop();
			ani.Frame = 6;
        }
    }

	public void press()
	{
		this.GetParent().GetParent<Sprite2D>().Visible = false;
        this.GetTree().ChangeSceneToFile("res://tscn/Pvp.tscn");
    }
	public void mouseEnt()
	{
		this.GetParent<AnimatedSprite2D>().Play();
	}
	public void mouseOut()
    {
		this.GetParent<AnimatedSprite2D>().Frame = 0;
    }
}
