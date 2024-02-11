//============================================================================
// UIPlotter: Utility for plotting data, typically as a function of time.
//============================================================================
using Godot;
using System;

public partial class UIPlotter : PanelContainer
{
	Line2D curve1;
	Vector2 windowSize;

	Vector2 pt;

	//------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
	public override void _Ready()
	{
		GD.Print("UIPlotter is alive");
		curve1 = GetNode<Line2D>("Curve1");

		windowSize = Size;
		GD.Print("windowSize = " + windowSize);
	}

	//------------------------------------------------------------------------
	// AddDataPoint
	//------------------------------------------------------------------------
	public void AddDataPoint(int idx, float t, float dat)
	{
		pt.X = t;
		pt.Y = dat;
		curve1.AddPoint(pt);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
