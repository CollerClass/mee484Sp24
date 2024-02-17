//============================================================================
// MCTestSimpleBC.cs    
// A simple mannequin control class
//============================================================================
using Godot;
using System;

public class MCTestSimpleBC : ManneControl
{
    CharacterItf modelItf;
    
    float angShL_X;
    float angShL_Y;
    float angShL_Z;
    float dAngle;
    JointType selectedJoint;

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MCTestSimpleBC(CharacterItf mitf)
    {
        modelItf = mitf;
        selectedJoint = JointType.Waist; //sets defalut joint to waist
        angShL_X = angShL_Y = angShL_Z = 0.0f;
        dAngle = Mathf.DegToRad(2.0f);

    }

    //------------------------------------------------------------------------
    // Process
    //------------------------------------------------------------------------
    public override void Process(double delta)
    {
        //base.Process(delta);

        //float waistAngle = (float)Math.Cos(time);
        //modelItf.SetSimpleWaistTwist(waistAngle);

        //Sets desired joint to be adjusted equal to a number pressed on the keypad
        if(Input.IsKeyPressed(Key.Key0)){
            selectedJoint = JointType.ShoulderL;
        }

        if(Input.IsKeyPressed(Key.Key1)){
            selectedJoint = JointType.ShoulderR;
        }

        if(Input.IsKeyPressed(Key.Key2)){
            selectedJoint = JointType.ElbowL;
        }

        if(Input.IsKeyPressed(Key.Key3)){
            selectedJoint = JointType.ElbowR;
        }

        if(Input.IsKeyPressed(Key.Key4)){
            selectedJoint = JointType.Waist;
        }

        if(Input.IsKeyPressed(Key.Key5)){
            selectedJoint = JointType.Torso;
        }

        if(Input.IsKeyPressed(Key.Key6)){
            selectedJoint = JointType.HipL;
        }

        if(Input.IsKeyPressed(Key.Key7)){
            selectedJoint = JointType.HipR;
        }

        if(Input.IsKeyPressed(Key.Key8)){
            selectedJoint = JointType.KneeL;
        }

        if(Input.IsKeyPressed(Key.Key9)){
            selectedJoint = JointType.KneeR;
        }


        // Takes inputs from key arrows. 
        if(Input.IsActionPressed("ui_right")){
            angShL_Y += dAngle;
        }
        if(Input.IsActionPressed("ui_left")){
            angShL_Y -= dAngle;
        }

        if(Input.IsActionPressed("ui_up")){
            angShL_Z += dAngle;
        }
        if(Input.IsActionPressed("ui_down")){
            angShL_Z -= dAngle;
        }

        if(Input.IsActionPressed("ui_other_right")){
            angShL_X += dAngle;
        }
        if(Input.IsActionPressed("ui_other_left")){
            angShL_X -= dAngle;
        }

        modelItf.QuatCalcEulerYZX(angShL_X,angShL_Y,angShL_Z);
        
        switch (selectedJoint){
            case JointType.ShoulderL:
                modelItf.SetShoulderLQuat(modelItf.qResult);
                break;
            case JointType.ShoulderR:
                modelItf.SetShoulderRQuat(modelItf.qResult);
                break;
            case JointType.ElbowL:
                modelItf.SetElbowLAngle(angShL_Y);
                break;
            case JointType.ElbowR:
                modelItf.SetElbowRAngle(angShL_Y);
                break;
            case JointType.Torso:
                modelItf.SetSimpleMidTorsoTwist(angShL_Z);
                break;
            case JointType.HipL:
                modelItf.SetHipLAngle(modelItf.qResult);
                break;
            case JointType.HipR:
                modelItf.SetHipRAngle(modelItf.qResult);
                break;
            case JointType.KneeL:
                modelItf.SetKneeLAngle(angShL_Z);
                break;
            case JointType.KneeR:
                modelItf.SetKneeRAngle(angShL_Z);
                break;  
            default:
                modelItf.SetSimpleWaistTwist(angShL_Z);
                break;
        }

        time += delta;
    }

    //------------------------------------------------------------------------
    // PhysicsProcess
    //------------------------------------------------------------------------
    public override void PhysicsProcess(double delta)
    {
        //base.PhysicsProcess(delta);
    }
}