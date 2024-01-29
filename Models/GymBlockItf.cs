//============================================================================
// GymBlockItf.cs    Provides an interface to the GymBlockModel
//============================================================================
using Godot;
using System;

public class GymBlockItf : CharacterItf
{
    GymBlockModel model;

    Node3D elbowLNode;
    Node3D waistJoint;
    bool modelLoaded;   //whether model has been loaded or not

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public GymBlockItf(GymBlockModel mdl)
    {
        model = mdl;

        waistJoint = model.GetNode<Node3D>
            ("RootNode/PelvisNode/WaistJoint");
    }

    // public void SetModel(GymBlockModel gbm)
    // {
    //     model = gbm;
    // }

    public override void SetElbowLAngle(float angle)
    {
        //base.SetElbowLAngle(angle);

    }

    public override void SetSimpleWaistTwist(float angle)
    {
        //base.SetSimpleWaistTwist(angle);
        waistJoint.Rotation = new Vector3(0.0f, angle, 0.0f);
    }
}