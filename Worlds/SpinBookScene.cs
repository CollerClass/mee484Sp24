//============================================================================
// SpinBookScene.cs   Scene for simulating a spinning book
//============================================================================
using Godot;
using System;

public partial class SpinBookScene : Node3D
{
	// initial angular velocity modifyable in the inspector
	[Export]
    float omega1_IC = 0.001f;
    [Export]
    float omega2_IC = 3.0f;
    [Export]
    float omega3_IC = 0.0f;

	private SpinBookModel model;       // reference to the graphical model

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

		float bookHeight = 0.6f;
		camTg = new Vector3(0.0f, bookHeight, 0.0f);
		model = GetNode<SpinBookModel>("SpinBookModel");
		model.SetCGLoc(camTg);

		// Set up the camera rig
		longitudeDeg = 30.0f;
		latitudeDeg = 20.0f;
		camDist = 8.0f;
		camFOV = 15.0f;  // 40.0f
		
		cam = GetNode<CamRig>("CamRig");
		cam.LongitudeDeg = longitudeDeg;
		cam.LatitudeDeg = latitudeDeg;
		cam.Distance = camDist;
		cam.Target = camTg;
		cam.FOVDeg = camFOV;
		//cam.SetOrthographic(2.0f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
