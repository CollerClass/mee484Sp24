//============================================================================
// GymBlockItf.cs    Provides an interface to the GymBlockModel
//============================================================================
using Godot;
using System;

public class GymBlockItf : CharacterItf
{
    GymBlockModel model;

    int nJoints = 10;
    Node3D[] joint;  // array of joints

    // joint indices
    int jWst   = 0;  // waist joint
    int jTso   = 1;  // mid torso joint
    int jShL   = 2;  // left shoulder
    int jEbL   = 3;  // left elbow
    int jShR   = 4;  // right shoulder
    int jEbR   = 5;  // right elbow
    int jHpL   = 6;  // left hip
    int jKnL   = 7;  // left knee
    int jHpR   = 8;  // right hip
    int jKnR   = 9;  // right knee


    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public GymBlockItf(GymBlockModel mdl)
    {
        model = mdl;

        joint = new Node3D[nJoints];

        string pathStr = "RootNode/PelvisNode/WaistJoint";
        joint[jWst] = model.GetNode<Node3D>(pathStr);

        pathStr += "/MidTorsoJoint/ShoulderLJoint";
        joint[jShL] = model.GetNode<Node3D>(pathStr);

        pathStr += "/ElbowLJoint";
        joint[jEbL] = model.GetNode<Node3D>(pathStr);

        //##### gotta keep goint
    }


    public override void SetShoulderLAngleYXZ(float ay, float ax, float az)
    {
        joint[jShL].Rotation = new Vector3(ax, ay, az);
    }

    public override void SetElbowLAngle(float angle)
    {

    }

    public override void SetSimpleWaistTwist(float angle)
    {  
        joint[jWst].Rotation = new Vector3(0.0f, angle, 0.0f);
    }
}