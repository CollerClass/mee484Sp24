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

        Transform3D tr1, trG;
        Quaternion q1, q2, qG;
        q1 = skel.GetBonePoseRotation(bIdx[jShL]);
        GD.Print("Pose quat " + q1);
        tr1 = skel.GetBoneRest(bIdx[jShL]);
        GD.Print("Rest position " + tr1.Origin);
        q2 = new Quaternion(tr1.Basis);
        GD.Print("Rest quat " + q2);
        trG = skel.GetBoneGlobalRest(bIdx[jShL]);
        qG = new Quaternion(trG.Basis);
        GD.Print("Global position " + trG.Origin);
        GD.Print("Global quat " + qG);

        qR[jShL] = q2;
        qGl[jShL] = qG;
    }


    public override void SetShoulderLAngleYXZ(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYXZ(ax, ay, az);
        
        QuatCalcEulerYXZ(ax,ay,az);
        Quaternion qq = qGl[jShL].Inverse()*qResult*qGl[jShL]*qR[jShL];
        skel.SetBonePoseRotation(bIdx[jShL], qq);
    }

    public override void SetShoulderLAngleYZX(float ax, float ay, float az)
    {
        //base.SetShoulderLAngleYZX(ax, ay, az);
        // nothing yet
    }
}