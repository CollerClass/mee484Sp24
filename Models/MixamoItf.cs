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

    Dictionary<SegmentType,int> segmentStartId;
    Dictionary<SegmentType,int> segmentEndId;
    Dictionary<SegmentType,Transform3D> segmentStart;
    Dictionary<SegmentType,Transform3D> segmentEnd;
    Dictionary<SegmentType,float> segmentLength;
    //int mixamoRootNodeId;
    //int simulationRootNodeId;
    Transform3D mixamoToSimulationTransform;

    Node3D segmentSphere;

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

        var mixamoRootNodeId = skel.FindBone("mixamorig_Hips");
        // simulationRootNodeId = skel.FindBone("mixamorig_Spine2");
        // var simulationSpine1Node = skel.FindBone("mixamorig_Spine1");
        // var simulationSpine0Node = skel.FindBone("mixamorig_Spine");
        //mixamoToSimulationTransform = skel.GetBonePose(simulationSpine0Node)*skel.GetBonePose(simulationSpine1Node)*skel.GetBonePose(simulationRootNodeId); 
        //mixamoToSimulationTransform = skel.GetBonePose(skel.FindBone("mixamorig_Spine"))*skel.GetBonePose(skel.FindBone("mixamorig_Spine1"))*skel.GetBonePose(skel.FindBone("mixamorig_Spine2")); 
        mixamoToSimulationTransform = skel.GetBoneGlobalPose(skel.FindBone("mixamorig_Hips")).AffineInverse()*skel.GetBoneGlobalPose(skel.FindBone("mixamorig_Spine2")); 

        segmentSphere = model.GetNode<MeshInstance3D>("SegmentSphere");
        segmentSphere.Transform = skel.GetBoneGlobalPose(mixamoRootNodeId);
        segmentSphere.Transform = segmentSphere.Transform*mixamoToSimulationTransform;

        //segmentStartId.Add(SegmentType.UpperTorso,skel.FindBone("mixamo_Spine2"));
        //segmentStartId.Add(SegmentType.MidTorso,skel.FindBone("mixamo_Spine"));
        //segmentStartId.Add(SegmentType.Pelvis,skel.FindBone("mixamo_Spine2"));
        // segmentStartId.Add(SegmentType.UpperLeftArm,skel.FindBone("mixamo_"));
        // segmentStartId.Add(SegmentType.UpperRightArm,skel.FindBone("mixamo_"));
        // segmentStartId.Add(SegmentType.LowerLeftArm,skel.FindBone("mixamo_"));
        // segmentStartId.Add(SegmentType.LowerRightArm,skel.FindBone("mixamo_"));
        // segmentStartId.Add(SegmentType.UpperLeftLeg,skel.FindBone("mixamo_"));
        // segmentStartId.Add(SegmentType.UpperRightLeg,skel.FindBone("mixamo_"));
        // segmentStartId.Add(SegmentType.LowerLeftLeg,skel.FindBone("mixamo_"));
        // segmentStartId.Add(SegmentType.LowerRightLeg,skel.FindBone("mixamo_"));


        // foreach(SegmentType segment in  Enum.GetValues(typeof(SegmentType))) {
            
        // }
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

    public override void MirrorLeftToRight()
    {
        quat[JointType.ShoulderR] = quat[JointType.ShoulderL];
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderR],quat[JointType.ShoulderR]);

        quat[JointType.ElbowR] = quat[JointType.ElbowL];
        skel.SetBonePoseRotation(bIdx[JointType.ElbowR],quat[JointType.ElbowR]);

        quat[JointType.HipR] = quat[JointType.HipL];
        skel.SetBonePoseRotation(bIdx[JointType.HipR],quat[JointType.HipR]);

        quat[JointType.KneeR] = quat[JointType.KneeL];
        skel.SetBonePoseRotation(bIdx[JointType.KneeR],quat[JointType.KneeR]);
    }

    public override void MirrorRightToLeft()
    {
        quat[JointType.ShoulderL] = quat[JointType.ShoulderR];
        skel.SetBonePoseRotation(bIdx[JointType.ShoulderL],quat[JointType.ShoulderL]);

        quat[JointType.ElbowL] = quat[JointType.ElbowR];
        skel.SetBonePoseRotation(bIdx[JointType.ElbowL],quat[JointType.ElbowL]);

        quat[JointType.HipL] = quat[JointType.HipR];
        skel.SetBonePoseRotation(bIdx[JointType.HipL],quat[JointType.HipL]);

        quat[JointType.KneeL] = quat[JointType.KneeR];
        skel.SetBonePoseRotation(bIdx[JointType.KneeL],quat[JointType.KneeL]);
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