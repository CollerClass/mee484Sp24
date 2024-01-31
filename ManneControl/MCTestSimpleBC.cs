//============================================================================
// MCTestSimpleBC.cs    
// A simple mannequin control class
//============================================================================
using Godot;
using System;

public class MCTestSimpleBC : ManneControl
{
    CharacterItf modelItf;

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MCTestSimpleBC(CharacterItf mitf)
    {
        modelItf = mitf;
    }

    //------------------------------------------------------------------------
    // Process
    //------------------------------------------------------------------------
    public override void Process(double delta)
    {
        //base.Process(delta);
    }

    //------------------------------------------------------------------------
    // PhysicsProcess
    //------------------------------------------------------------------------
    public override void PhysicsProcess(double delta)
    {
        //base.PhysicsProcess(delta);
    }
}