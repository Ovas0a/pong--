using Godot;
using System;

public partial class Timers : Label
{
	// Called when the node enters the scene tree for the first time.
	public static bool Show;
	public static float Timer=3;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Show)
		{
			this.Text = Timer.ToString("F2");
			this.Visible = true;
			Timer -= (float)delta;
			if (Timer <= 0)
			{
				this.Visible = false;
				Show = false;
				Timer = 3;
            }
        }
		
		
    }
}
