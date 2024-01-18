//============================================================================
// SpinBookScene.cs   Scene for simulating a spinning book
//============================================================================
using Godot;
using System;

public partial class SpinBookScene : Node3D
{
	CamRig cam;
	float longitudeDeg;
	float latitudeDeg;
	float camDist;
	float camFOV;
	Vector3 camTg;       // coords of camera target


	//------------------------------------------------------------------------
	// _Ready: Called once when the node enters the scene tree for the first 
	//         time.
	//------------------------------------------------------------------------
	public override void _Ready()
	{
		GD.Print("Hello MEE 484");

		float bookHeight = 0.8f;

		// Set up the camera rig
		longitudeDeg = 30.0f;
		latitudeDeg = 15.0f;
		camDist = 3.0f;

		camTg = new Vector3(0.0f, bookHeight, 0.0f);
		cam = GetNode<CamRig>("CamRig");
		cam.LongitudeDeg = longitudeDeg;
		cam.LatitudeDeg = latitudeDeg;
		cam.Distance = camDist;
		cam.Target = camTg;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
