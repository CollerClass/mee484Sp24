//============================================================================
// GymnastScene.cs   Scene for simulating the spinning gymnast
//============================================================================
using Godot;
using System;

public partial class MannequinScene : Node3D
{
	// When new models are created, they should be added
	GymBlockModel modelGymBlock;
	enum ModelType{
		GymBlock,
	}
	ModelType modelType;
	CharacterItf  modelItf;

	ManneControl mcObject;

	// When new ManneControl classes are created, they should be added to
	// this list. 
	enum ManneControlType{
		SimpleBC,
	}
	ManneControlType mcType;
	
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
		// Specify the model type here
		modelType = ModelType.GymBlock;

		// Specify the class for mannequin interaction here
		mcType = ManneControlType.SimpleBC;


		//----------------- Mechanism for model and control specification
		switch(modelType){
			// when new models are created, add them to the list
			case ModelType.GymBlock:
				modelGymBlock = GetNode<GymBlockModel>("GymBlockModel");
				modelItf = new GymBlockItf(modelGymBlock);
				break;

			default:
				modelGymBlock = GetNode<GymBlockModel>("GymBlockModel");
				modelItf = new GymBlockItf(modelGymBlock);
				break;
		}
		// modelGymBlock = GetNode<GymBlockModel>("GymBlockModel");
		// modelItf = new GymBlockItf(modelGymBlock);


		switch(mcType){
			//## when new ManneControl classes created, add them to the list
			case ManneControlType.SimpleBC:
				mcObject = new MCTestSimpleBC(modelItf);
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
