//============================================================================
// GymnastScene.cs   Scene for simulating the spinning gymnast
//============================================================================
using Godot;
using System;

public partial class MannequinScene : Node3D
{
	GymBlockModel model;
	CharacterItf  modelItf;

	ManneControl mcObject;

	double time;
	
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
		//#####################################
		//#### Only one model and one ManneControl hard coded in for now
		//#### need to have this part choose from a selection
		model = GetNode<GymBlockModel>("GymBlockModel");
		modelItf = new GymBlockItf(model);
		time = 0.0;

		mcObject = new MCTestSimpleBC(modelItf);
		//######################################

		float cgHeight = 1.3f;
		camTg = new Vector3(0.0f, cgHeight, 0.0f);

		// Set up the camera rig
		longitudeDeg = 40.0f;
		latitudeDeg = 30.0f;
		camDist = 4.0f;
		camFOV = 30.0f;  // 40.0f
		
		cam = GetNode<CamRig>("CamRig");
		cam.LongitudeDeg = longitudeDeg;
		cam.LatitudeDeg = latitudeDeg;
		cam.Distance = camDist;
		cam.Target = camTg;
		cam.FOVDeg = camFOV;
	}

	//------------------------------------------------------------------------
	// _Process: Called every frame. 'delta' is the elapsed time since the 
	//           previous frame.
	//------------------------------------------------------------------------
	public override void _Process(double delta)
	{
	}
}
