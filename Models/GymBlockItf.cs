//============================================================================
// GymBlockItf.cs    Provides an interface to the GymBlockModel
//============================================================================
using Godot;
using System;

public class GymBlockItf : CharacterItf
{
    Node3D model;

    int nJoints = 10;
    Node3D[] joint;  // array of joints
    Quaternion[] quat; // array of quaternions;

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
    public GymBlockItf(Node3D mdl)
    {
        model = mdl;

        joint = new Node3D[nJoints];
        quat = new Quaternion[nJoints];

        string pathStr = "RootNode/PelvisNode/WaistJoint";
        joint[jWst] = model.GetNode<Node3D>(pathStr);

        pathStr += "/MidTorsoJoint/ShoulderLJoint";
        joint[jShL] = model.GetNode<Node3D>(pathStr);

        pathStr += "/ElbowLJoint";
        joint[jEbL] = model.GetNode<Node3D>(pathStr);

        pathStr = "RootNode/PelvisNode/WaistJoint";
        pathStr += "/MidTorsoJoint/ShoulderRJoint";
        joint[jShR] = model.GetNode<Node3D>(pathStr);

        pathStr += "/ElbowRJoint";
        joint[jEbR] = model.GetNode<Node3D>(pathStr);

        //##### gotta keep going
    }


    public override void SetShoulderLAngleYXZ(float ax, float ay, float az)
    {
        // uVec.X = ax;   uVec.Y = ay;   uVec.Z = az;
        // joint[jShL].Rotation = uVec;

        QuatCalcEulerYXZ(ax,ay,az);
        joint[jShL].Quaternion = qResult;
    }

    public override void SetElbowLAngle(float angle)
    {

    }

    public override void SetSimpleWaistTwist(float angle)
    {  
        joint[jWst].Rotation = new Vector3(0.0f, angle, 0.0f);
    }
}