//============================================================================
// GymBlockModel.cs   Geometric model for a generic gymnast. To be used as a
//                    placeholder until better models are created
//============================================================================
using Godot;
using System;

public partial class GymBlockModel : Node3D
{
	Vector3 pelvisAnchor;
	Vector3 waistRelCoords;


	Vector3 lHipRelCoords;


	Vector3 pelvisBoxDim;
	
	//------------------------------------------------------------------------
	// _Ready: Called once when the node enters the scene tree for the first 
	//         time.
	//------------------------------------------------------------------------
	public override void _Ready()
	{
	}

	//------------------------------------------------------------------------
	// InitBodyCoords: Initialize body coordinates
	//------------------------------------------------------------------------
	private void InitBodyCoords()
	{
		pelvisAnchor = new Vector3(0.0f, 1.042f, 0.0f);
		waistRelCoords = new Vector3(0.0f, 0.101f, 0.0f);


		lHipRelCoords = new Vector3(0.082f, -0.068f, -0.016f);
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
