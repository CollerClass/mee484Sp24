//============================================================================
// MixamoItf.cs    Provides an interface to the GymBlockModel
//============================================================================
using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Collections.Immutable;

public class MixamoItf : CharacterItf
{
    Node3D model;

    Skeleton3D skel;
    Dictionary<JointType,int> bIdx;  // bone indices
    Dictionary<JointType,Quaternion> quat; // quaternions relative to rest state
    Dictionary<JointType,Quaternion> qR;  // rest quaternions
    Dictionary<JointType,Quaternion> qGl;    // global quaternions

    private static ImmutableDictionary<JointType,Vector3> hingeVectors = ImmutableDictionary.CreateRange(
        new KeyValuePair<JointType,Vector3>[] {
            KeyValuePair.Create(JointType.ElbowL,Vector3.Back),
            KeyValuePair.Create(JointType.ElbowR,Vector3.Back),
            KeyValuePair.Create(JointType.KneeL,Vector3.Right),
            KeyValuePair.Create(JointType.KneeR,Vector3.Right),
            KeyValuePair.Create(JointType.Torso,Vector3.Right),
            KeyValuePair.Create(JointType.Waist,Vector3.Right),
        }
    );

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MixamoItf(Node3D mdl)
    {
        model = mdl;

        skel = model.GetNode<Skeleton3D>("RootNode/Skeleton3D");

        bIdx = new Dictionary<JointType,int>();
        quat = new Dictionary<JointType,Quaternion>(); 
        qR = new Dictionary<JointType,Quaternion>();
        qGl = new Dictionary<JointType,Quaternion>(); 
    

        bIdx.Add(JointType.Waist, skel.FindBone("mixamorig_Spine"));
        bIdx.Add(JointType.Torso, skel.FindBone("mixamorig_Spine1"));
        bIdx.Add(JointType.ShoulderL, skel.FindBone("mixamorig_LeftArm"));
        bIdx.Add(JointType.ElbowL, skel.FindBone("mixamorig_LeftForeArm"));
        bIdx.Add(JointType.ShoulderR, skel.FindBone("mixamorig_RightArm"));
        bIdx.Add(JointType.ElbowR, skel.FindBone("mixamorig_RightForeArm"));
        bIdx.Add(JointType.HipL, skel.FindBone("mixamorig_LeftUpLeg"));
        bIdx.Add(JointType.KneeL, skel.FindBone("mixamorig_LeftLeg"));
        bIdx.Add(JointType.HipR, skel.FindBone("mixamorig_RightUpLeg"));
        bIdx.Add(JointType.KneeR, skel.FindBone("mixamorig_RightLeg"));

        Transform3D tr;
        foreach(var id in bIdx){
            tr = skel.GetBoneRest(id.Value);
            qR.Add(id.Key, new Quaternion(tr.Basis));
            quat.Add(id.Key, qR[id.Key]);
            tr = skel.GetBoneGlobalRest(id.Value);
            qGl.Add(id.Key, new Quaternion(tr.Basis)); 
        }

    }

    public override ImmutableDictionary<JointType,Vector3> HingeVectors() 
    {
        return hingeVectors;
    }


    //------------ Methods for the left shoulder -----------------------------
    public override void SetShoulderLQuat(Quaternion q)
    {
        //base.SetShoulderLQuat(q);
        quat[JointType.ShoulderL] = q;
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderL], quat[JointType.ShoulderL]);
    }
    public override void SetShoulderLAngleYXZ(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYXZ(ax, ay, az);
        
        QuatCalcEulerYXZ(ax,ay,az);
        quat[JointType.ShoulderL] = qR[JointType.ShoulderL]*qGl[JointType.ShoulderL].Inverse()*qResult*qGl[JointType.ShoulderL];
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderL], quat[JointType.ShoulderL]);
    }
    public override void SetShoulderLAngleYZX(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYZX(ax, ay, az);
        
        QuatCalcEulerYZX(ax,ay,az);
        quat[JointType.ShoulderL] = qR[JointType.ShoulderL]*qGl[JointType.ShoulderL].Inverse()*qResult*qGl[JointType.ShoulderL];
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderL], quat[JointType.ShoulderL]);
    }

    //------------ Methods for the right shoulder ----------------------------
    public override void SetShoulderRQuat(Quaternion q)
    {
        //base.SetShoulderRQuat(q);
        quat[JointType.ShoulderR] = q;
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderR], quat[JointType.ShoulderR]);
    }
    public override void SetShoulderRAngleYXZ(float ax, float ay, float az)
    {
        //base.SetShoulderRAngleYXZ(ax, ay, az);
        
        QuatCalcEulerYXZ(ax,ay,az);
        quat[JointType.ShoulderR] = qR[JointType.ShoulderR]*qGl[JointType.ShoulderR].Inverse()*qResult*qGl[JointType.ShoulderR];
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderR], quat[JointType.ShoulderR]);
    }
    public override void SetShoulderRAngleYZX(float ax, float ay, float az)
    {
        //base.SetShoulderRAngleYZX(ax, ay, az);
        
        QuatCalcEulerYZX(ax,ay,az);
        quat[JointType.ShoulderR] = qR[JointType.ShoulderR]*qGl[JointType.ShoulderR].Inverse()*qResult*qGl[JointType.ShoulderR];
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderR], quat[JointType.ShoulderR]);
    }

 //------------ Methods for the Left Elbow  ----------------------------
    public override void SetElbowLAngle(float angle)
    {
        // Vector3 elbowNormal = qR[JointType.ElbowL].GetAxis().Normalized().Normalized();
        Quaternion q = new Quaternion(hingeVectors[JointType.ElbowL],angle);
        quat[JointType.ElbowL] = q;
        skel.SetBonePoseRotation(bIdx[JointType.ElbowL], quat[JointType.ElbowL]);
    }
   
 //------------ Methods for the Right Elbow  ----------------------------
    public override void SetElbowRAngle(float angle)
    {
        // Vector3 elbowNormal = qR[JointType.ElbowL].GetAxis().Normalized().Normalized();
        Quaternion q = new Quaternion(hingeVectors[JointType.ElbowR],angle);
        quat[JointType.ElbowR] = q;
        skel.SetBonePoseRotation(bIdx[JointType.ElbowR], quat[JointType.ElbowR]);
    }

 //------------ Methods for the Waist  ----------------------------
    public override void SetSimpleWaistTwist(float angle)
    {
        // Vector3 elbowNormal = qR[JointType.ElbowL].GetAxis().Normalized().Normalized();
        Quaternion q = new Quaternion(hingeVectors[JointType.Waist],angle);
        quat[JointType.Waist] = q;
        skel.SetBonePoseRotation(bIdx[JointType.Waist], quat[JointType.Waist]);
    }

 //------------ Methods for the Mid Torso  ----------------------------
    public override void SetSimpleMidTorsoTwist(float angle)
    {
        // Vector3 elbowNormal = qR[JointType.ElbowL].GetAxis().Normalized().Normalized();
        Quaternion q = new Quaternion(hingeVectors[JointType.Torso],angle);
        quat[JointType.Torso] = q;
        skel.SetBonePoseRotation(bIdx[JointType.Torso], quat[JointType.Torso]);
    }

 //------------ Methods for the Left Hip  ----------------------------
    public override void SetHipLAngle(Quaternion q)
    {
        quat[JointType.HipL] = q;
        skel.SetBonePoseRotation(bIdx[JointType.HipL], quat[JointType.HipL]);
    }


 //------------ Methods for the Left Hip  ----------------------------
    public override void SetHipRAngle(Quaternion q)
    {
        quat[JointType.HipR] = q;
        skel.SetBonePoseRotation(bIdx[JointType.HipR], quat[JointType.HipR]);
    }

 //------------ Methods for the Left Knee  ----------------------------
    public override void SetKneeLAngle(float angle)
    {
        Quaternion q = new Quaternion(hingeVectors[JointType.KneeL],angle);
        quat[JointType.KneeL] = q;
        skel.SetBonePoseRotation(bIdx[JointType.KneeL], quat[JointType.KneeL]);
    }
   
 //------------ Methods for the Right Knee  ----------------------------
    public override void SetKneeRAngle(float angle)
    {
        Quaternion q = new Quaternion(hingeVectors[JointType.KneeR],angle);
        quat[JointType.KneeR] = q;
        skel.SetBonePoseRotation(bIdx[JointType.KneeR], quat[JointType.KneeR]);
    }

 //------------ Methods Reset all joints  ----------------------------
    public override void ResetAllJoints()
    {
        foreach(var bone in qR)
        {
            quat[bone.Key] = bone.Value;
            skel.SetBonePoseRotation(bIdx[bone.Key], quat[bone.Key]);
        }
    }

    public override void ResetJoint(JointType jointType) 
    {
        quat[jointType] = qR[jointType];
        skel.SetBonePoseRotation(bIdx[jointType], quat[jointType]);
    }
    public override Quaternion GetJointQuat(JointType jointType)
    {
        return quat[jointType];
    }

    public override void SetJointQuat(JointType jointType, Quaternion newQuat)
    {
        quat[jointType] = newQuat;
        skel.SetBonePoseRotation(bIdx[jointType],quat[jointType]);
    }

}