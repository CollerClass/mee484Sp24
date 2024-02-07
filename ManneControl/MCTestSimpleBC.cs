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

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MCTestSimpleBC(CharacterItf mitf)
    {
        modelItf = mitf;

        angShL_X = angShL_Y = angShL_Z = 0.0f;
        dAngle = Mathf.DegToRad(2.0f);

    }

    //------------------------------------------------------------------------
    // Process
    //------------------------------------------------------------------------
    public override void Process(double delta)
    {
        //base.Process(delta);

        float waistAngle = (float)Math.Cos(time);
        //modelItf.SetSimpleWaistTwist(waistAngle);

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

        modelItf.SetShoulderLAngleYXZ(angShL_X, angShL_Y, angShL_Z);
        //modelItf.SetShoulderLAngleYZX(angShL_X, angShL_Y, angShL_Z);
        //modelItf.SetShoulderRAngleYZX(angShL_X, angShL_Y, angShL_Z);

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