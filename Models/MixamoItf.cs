//============================================================================
// MixamoItf.cs    Provides an interface to the GymBlockModel
//============================================================================
using Godot;
using System;

public class MixamoItf : CharacterItf
{
    Node3D model;

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MixamoItf(Node3D mdl)
    {
        model = mdl;
    }


    public override void SetShoulderLAngleYXZ(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYXZ(ax, ay, az);
        // nothing yet
        //GD.Print("Mixamo: My left shoulder not working yet.");
    }

    public override void SetShoulderLAngleYZX(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYZX(ax, ay, az);
        // nothing yet
    }
}