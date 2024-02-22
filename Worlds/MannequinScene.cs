//============================================================================
// MannequinScene.cs   Scene for developing and testing gymnast models, and
//           and gymnast actions (choreography, controllers, etc). The model
//           here is disconnected from the physics engine.
//============================================================================
using Godot;
using System;

public partial class MannequinScene : Node3D
{
	// When new models are created, they should be added
	Node3D model;
	enum ModelType{
		GymBlock,
		XBot,
		YBot
	}
	ModelType modelType;
	CharacterItf  modelItf;

	ManneControl mcObject;

	// When new ManneControl classes are created, they should be added to
	// this list. 
	enum ManneControlType{
		SimpleBC,
		JointControl,
	}
	ManneControlType mcType;
	
	CamRig cam;
	float longitudeDeg;
	float latitudeDeg;
	float camDist;
	float camFOV;
	Vector3 camTg;       // coords of camera target

	MarginContainer margContTL;
	MarginContainer margContTR;
	MarginContainer margContBL;
	MarginContainer margContBR;
	

	//------------------------------------------------------------------------
	// _Ready: Called once when the node enters the scene tree for the first 
	//         time.
	//------------------------------------------------------------------------
	public override void _Ready()
	{
		// Specify the model type here
		 modelType = ModelType.GymBlock;
		//modelType = ModelType.XBot;
		//modelType = ModelType.YBot;


		// Specify the class for mannequin interaction here
		mcType = ManneControlType.SimpleBC;
		mcType = ManneControlType.JointControl;


		//----------------- Mechanism for model and control specification
		switch(modelType){
			// when new models are created, add them to the list
			case ModelType.GymBlock:
				model = GetNode<Node3D>("GymBlockModel");
				modelItf = new GymBlockItf(model);
				break;
			case ModelType.XBot:
				model = GetNode<Node3D>("XBotModel");
				modelItf = new MixamoItf(model);
				break;
			case ModelType.YBot:
				model = GetNode<Node3D>("YBotModel");
				modelItf = new MixamoItf(model);
				break;
			default:
				model = GetNode<Node3D>("GymBlockModel");
				modelItf = new GymBlockItf(model);
				break;
		}
		// modelGymBlock = GetNode<GymBlockModel>("GymBlockModel");
		// modelItf = new GymBlockItf(modelGymBlock);
		model.Show();

		switch(mcType){
			//## when new ManneControl classes created, add them to the list
			case ManneControlType.SimpleBC:
				mcObject = new MCTestSimpleBC(modelItf);
				break;
			case ManneControlType.JointControl:
				mcObject = new JointControl(modelItf);
				break;

			default:
				GD.PrintErr("MannequinScene: mcType not is switch list.");
				mcObject = new MCTestSimpleBC(modelItf);
				break;
		}
		//------------------

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

		// get MarginContainer nodes
		MarginContainer mcTL = GetNode<MarginContainer>("UINode/MargContTL");
		MarginContainer mcTR = GetNode<MarginContainer>("UINode/MargContTR");
		MarginContainer mcBL = GetNode<MarginContainer>("UINode/MargContBL");
		MarginContainer mcBR = GetNode<MarginContainer>("UINode/MargContBR");
		mcObject.SetMarginContainers(mcTL, mcTR, mcBL, mcBR);

		mcObject.Init2();
	}

	//------------------------------------------------------------------------
	// _Process: Called every frame. 'delta' is the elapsed time since the 
	//           previous frame.
	//------------------------------------------------------------------------
	public override void _Process(double delta)
	{
		mcObject.Process(delta);
	}

    //------------------------------------------------------------------------
    // _PhysicsProcess:
    //------------------------------------------------------------------------
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		mcObject.PhysicsProcess(delta);
    }
}
