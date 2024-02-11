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

	UIPanelDisplay datDisplay; // to display numeric data
	int dispCtr;
	int dispTHold;

	UIPlotter plotter;


	//------------------------------------------------------------------------
	// _Ready: Called once when the node enters the scene tree for the first 
	//         time.
	//------------------------------------------------------------------------
	public override void _Ready()
	{
		GD.Print("MEE 484 - Spinning Book");

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

		// Set up the data display
        datDisplay = GetNode<UIPanelDisplay>(
            "UINode/MarginContainer/DatDisplay");
        datDisplay.SetNDisplay(3);
        datDisplay.SetLabel(0,"Omega1");
        datDisplay.SetDigitsAfterDecimal(0,2);
        datDisplay.SetLabel(1,"Omega2");
        datDisplay.SetDigitsAfterDecimal(1,2);
        datDisplay.SetLabel(2,"Omega3");
        datDisplay.SetDigitsAfterDecimal(2,2);
		dispCtr = 0;
		dispTHold = 4;

		// set up the plotter
		plotter = GetNode<UIPlotter>("UINode/MarginContainer2/UIPlotter");
		// for testing purposes....
		plotter.AddDataPoint(0,10.0f, 100.0f);
		plotter.AddDataPoint(0,50.0f, 160.0f);
		plotter.AddDataPoint(0,100.0f, 90.0f);
		plotter.AddDataPoint(0,200.0f, 65.0f);
		plotter.AddDataPoint(0,310.0f, 5.0f);

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

		if(dispCtr > dispTHold){
			datDisplay.SetValue(0, (float)sim.omega1);
			datDisplay.SetValue(1, (float)sim.omega2);
			datDisplay.SetValue(2, (float)sim.omega3);
			dispCtr = 0;
		}
		++dispCtr;
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
