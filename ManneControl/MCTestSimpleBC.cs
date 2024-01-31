//============================================================================
// MCTestSimpleBC.cs    
// A simple mannequin control class
//============================================================================
using Godot;
using System;

public class MCTestSimpleBC : ManneControl
{
    CharacterItf modelItf;
    
    float shoulderLYaw;
    float shoulderLRoll;
    float dAngle;

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MCTestSimpleBC(CharacterItf mitf)
    {
        modelItf = mitf;

        shoulderLYaw = 0.0f;
        shoulderLRoll = 0.0f;
        dAngle = Mathf.DegToRad(2.0f);
    }

    //------------------------------------------------------------------------
    // Process
    //------------------------------------------------------------------------
    public override void Process(double delta)
    {
        //base.Process(delta);

        float waistAngle = (float)Math.Cos(time);
        modelItf.SetSimpleWaistTwist(waistAngle);

        if(Input.IsActionPressed("ui_right")){
            shoulderLYaw += dAngle;
        }
        if(Input.IsActionPressed("ui_left")){
            shoulderLYaw -= dAngle;
        }

        if(Input.IsActionPressed("ui_up")){
            shoulderLRoll += dAngle;
        }
        if(Input.IsActionPressed("ui_down")){
            shoulderLRoll -= dAngle;
        }

        modelItf.SetShoulderLAngleYXZ(shoulderLYaw, 0.0f, shoulderLRoll);

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