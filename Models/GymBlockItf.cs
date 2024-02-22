//============================================================================
// GymBlockItf.cs    Provides an interface to the GymBlockModel
//============================================================================
using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

public class GymBlockItf : CharacterItf
{
    Node3D model;

    Dictionary<JointType, Node3D> joint;  // array of joints
    Dictionary<JointType,Quaternion> quat; // array of quaternions;

    private static ImmutableDictionary<JointType,Vector3> hingeVectors = ImmutableDictionary.CreateRange(
        new KeyValuePair<JointType,Vector3>[] {
            KeyValuePair.Create(JointType.ElbowL,Vector3.Up),
            KeyValuePair.Create(JointType.ElbowR,Vector3.Up),
            KeyValuePair.Create(JointType.KneeL,Vector3.Right),
            KeyValuePair.Create(JointType.KneeR,Vector3.Right),
            KeyValuePair.Create(JointType.Torso,Vector3.Right),
            KeyValuePair.Create(JointType.Waist,Vector3.Right),
        }
    );

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public GymBlockItf(Node3D mdl)
    {
        model = mdl;

        joint = new Dictionary<JointType, Node3D>();
        quat = new Dictionary<JointType,Quaternion>();

        string pathStr = "RootNode/PelvisNode/WaistJoint";
        joint.Add(JointType.Waist,model.GetNode<Node3D>(pathStr));

        pathStr += "/MidTorsoJoint";
        joint.Add(JointType.Torso,model.GetNode<Node3D>(pathStr));

        pathStr += "/ShoulderLJoint";
        joint.Add(JointType.ShoulderL, model.GetNode<Node3D>(pathStr));

        pathStr += "/ElbowLJoint";
        joint.Add(JointType.ElbowL, model.GetNode<Node3D>(pathStr));

        pathStr = "RootNode/PelvisNode/WaistJoint";
        pathStr += "/MidTorsoJoint/ShoulderRJoint";
        joint.Add(JointType.ShoulderR, model.GetNode<Node3D>(pathStr));

        pathStr += "/ElbowRJoint";
        joint.Add(JointType.ElbowR, model.GetNode<Node3D>(pathStr));

        pathStr = "RootNode/PelvisNode/HipLJoint";
        joint.Add(JointType.HipL, model.GetNode<Node3D>(pathStr));

        pathStr += "/KneeLJoint";
        joint.Add(JointType.KneeL, model.GetNode<Node3D>(pathStr));

        pathStr = "RootNode/PelvisNode/HipRJoint";
        joint.Add(JointType.HipR, model.GetNode<Node3D>(pathStr));

        pathStr += "/KneeRJoint";
        joint.Add(JointType.KneeR, model.GetNode<Node3D>(pathStr));
    }

    public override ImmutableDictionary<JointType,Vector3> HingeVectors() 
    {
        return hingeVectors;
    }

    //------------ Methods for the left shoulder -----------------------------
    public override void SetShoulderLQuat(Quaternion q)
    {
        //base.SetShoulderLQuat(q);
        joint[JointType.ShoulderL].Quaternion = q;
    }
    public override void SetShoulderLAngleYXZ(float ax, float ay, float az)
    {
        // uVec.X = ax;   uVec.Y = ay;   uVec.Z = az;
        // joint[JointType.ShoulderL].Rotation = uVec;

        QuatCalcEulerYXZ(ax,ay,az);
        joint[JointType.ShoulderL].Quaternion = qResult;
    }
    public override void SetShoulderLAngleYZX(float ax, float ay, float az)
    {
        QuatCalcEulerYZX(ax,ay,az);
        joint[JointType.ShoulderL].Quaternion = qResult;
    }

    //------------ Methods for the left shoulder -----------------------------
    public override void SetShoulderRQuat(Quaternion q)
    {
        //base.SetShoulderRQuat(q);
        joint[JointType.ShoulderR].Quaternion = q;
    }
    public override void SetShoulderRAngleYXZ(float ax, float ay, float az)
    {
        QuatCalcEulerYXZ(ax,ay,az);
        joint[JointType.ShoulderR].Quaternion = qResult;
    }
    public override void SetShoulderRAngleYZX(float ax, float ay, float az)
    {
        QuatCalcEulerYZX(ax,ay,az);
        joint[JointType.ShoulderR].Quaternion = qResult;
    }


    public override void SetElbowLAngle(float angle)
    {
        joint[JointType.ElbowL].Rotation = new Vector3(0.0f, angle, 0.0f);
    }

    public override void SetSimpleWaistTwist(float angle)
    {  
        joint[JointType.Waist].Rotation = new Vector3(0.0f, angle, 0.0f);
    }

    public override void SetElbowRAngle(float angle)
    {
        joint[JointType.ElbowR].Rotation = new Vector3(0.0f, angle, 0.0f);
    }

    public override void SetSimpleMidTorsoTwist(float angle)
    {
       joint[JointType.Torso].Rotation = new Vector3(0.0f, angle, 0.0f);
    }

    public override void SetHipLAngle(Quaternion q)
    {
        joint[JointType.HipL].Quaternion = q;
    }

    public override void SetHipRAngle(Quaternion q)
    {
        joint[JointType.HipR].Quaternion = q;
    }

    public override void SetKneeLAngle(float angle)
    {
        joint[JointType.KneeL].Rotation = new Vector3(0.0f, angle, 0.0f);
    }

    public override void SetKneeRAngle(float angle)
    {
        joint[JointType.KneeR].Rotation = new Vector3(0.0f, angle, 0.0f);
    }

    public override void ResetAllJoints()
    {
        foreach(var bone in joint)
        {
            bone.Value.Quaternion = Quaternion.Identity;
        }
    }

    public override void ResetJoint(JointType jointType) 
    {
        joint[jointType].Quaternion = Quaternion.Identity;
    }
    public override Quaternion GetJointQuat(JointType jointType)
    {
        return joint[jointType].Quaternion;
    }

    public override void SetJointQuat(JointType jointType, Quaternion newQuat)
    {
        joint[jointType].Quaternion = newQuat;
    }
}