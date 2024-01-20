//============================================================================
// SpinBookScene.cs   Scene for simulating a spinning book
//============================================================================
using Godot;
using Sim;
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

	SpinBook sim;   // spinning book simulation

	Quaternion quat; // quaternion to send book's orientation to model
	double time;

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

		float bookHeight = 0.7f;
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

		// set up the simulation
		sim = new SpinBook();
		sim.omega1 = (double)omega1_IC;
        sim.omega2 = (double)omega2_IC;
        sim.omega3 = (double)omega3_IC;
        sim.IG1 = 1.0;
        sim.IG2 = 2.0;
        sim.IG3 = 3.0;

		quat = new Quaternion();
		quat = Quaternion.Identity;

        time = 0.0;
	}

	
	//------------------------------------------------------------------------
	// _Process: Called every frame. 'delta' is the elapsed time since the 
	//           previous frame.
	//------------------------------------------------------------------------
	public override void _Process(double delta)
	{
		quat.W = (float)sim.q0;
		quat.X = (float)sim.q1;
		quat.Y = (float)sim.q2;
		quat.Z = (float)sim.q3;

		model.SetOrientation(quat);
	}

	//------------------------------------------------------------------------
    // _PhysicsProcess:
    //------------------------------------------------------------------------
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		sim.Step(time, delta);
		time += delta;
    }
}
