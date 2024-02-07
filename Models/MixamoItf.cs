//============================================================================
// MixamoItf.cs    Provides an interface to the GymBlockModel
//============================================================================
using Godot;
using System;

public class MixamoItf : CharacterItf
{
    Node3D model;

    Skeleton3D skel;

    int nJoints; // number of actuatable joints
    int[] bIdx;  // bone indices
    Quaternion[] quat; // quaternions relative to rest state
    Quaternion[] qR;  // rest quaternions
    Quaternion[] qGl;    // global quaternions

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
    public MixamoItf(Node3D mdl)
    {
        model = mdl;

        skel = model.GetNode<Skeleton3D>("RootNode/Skeleton3D");

        nJoints = 10;
        bIdx = new int[nJoints];
        quat = new Quaternion[nJoints];
        qR = new Quaternion[nJoints];
        qGl = new Quaternion[nJoints];

        bIdx[jWst] = skel.FindBone("mixamorig_Spine");
        bIdx[jTso] = skel.FindBone("mixamorig_Spine1");
        bIdx[jShL] = skel.FindBone("mixamorig_LeftArm");
        bIdx[jEbL] = skel.FindBone("mixamorig_LeftForeArm");
        bIdx[jShR] = skel.FindBone("mixamorig_RightArm");
        bIdx[jEbR] = skel.FindBone("mixamorig_RightForeArm");
        bIdx[jHpL] = skel.FindBone("mixamorig_LeftUpLeg");
        bIdx[jKnL] = skel.FindBone("mixamorig_LeftLeg");
        bIdx[jHpR] = skel.FindBone("mixamorig_RightUpLeg");
        bIdx[jKnR] = skel.FindBone("mixamorig_RightLeg");

        Transform3D tr;
        int i;
        for(i=0; i<nJoints; ++i){
            tr = skel.GetBoneRest(bIdx[i]);
            qR[i] = new Quaternion(tr.Basis);
            quat[i] = qR[i];
            tr = skel.GetBoneGlobalRest(bIdx[i]);
            qGl[i] = new Quaternion(tr.Basis); 
        }

    }

    //------------ Methods for the left shoulder -----------------------------
    public override void SetShoulderLQuat(Quaternion q)
    {
        //base.SetShoulderLQuat(q);
        quat[jShL] = q;
        skel.SetBonePoseRotation(bIdx[jShL], quat[jShL]);
    }
    public override void SetShoulderLAngleYXZ(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYXZ(ax, ay, az);
        
        QuatCalcEulerYXZ(ax,ay,az);
        quat[jShL] = qR[jShL]*qGl[jShL].Inverse()*qResult*qGl[jShL];
        skel.SetBonePoseRotation(bIdx[jShL], quat[jShL]);
    }
    public override void SetShoulderLAngleYZX(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYZX(ax, ay, az);
        
        QuatCalcEulerYZX(ax,ay,az);
        quat[jShL] = qR[jShL]*qGl[jShL].Inverse()*qResult*qGl[jShL];
        skel.SetBonePoseRotation(bIdx[jShL], quat[jShL]);
    }

    //------------ Methods for the right shoulder ----------------------------
    public override void SetShoulderRQuat(Quaternion q)
    {
        //base.SetShoulderRQuat(q);
        quat[jShR] = q;
        skel.SetBonePoseRotation(bIdx[jShR], quat[jShR]);
    }
    public override void SetShoulderRAngleYXZ(float ax, float ay, float az)
    {
        //base.SetShoulderRAngleYXZ(ax, ay, az);
        
        QuatCalcEulerYXZ(ax,ay,az);
        quat[jShR] = qR[jShR]*qGl[jShR].Inverse()*qResult*qGl[jShR];
        skel.SetBonePoseRotation(bIdx[jShR], quat[jShR]);
    }
    public override void SetShoulderRAngleYZX(float ax, float ay, float az)
    {
        //base.SetShoulderRAngleYZX(ax, ay, az);
        
        QuatCalcEulerYZX(ax,ay,az);
        quat[jShR] = qR[jShR]*qGl[jShR].Inverse()*qResult*qGl[jShR];
        skel.SetBonePoseRotation(bIdx[jShR], quat[jShR]);
    }
}