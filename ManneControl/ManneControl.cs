//============================================================================
// ManneControl.cs
//============================================================================
using Godot;
using System;

public abstract class ManneControl
{

    public virtual void Process(double delta)
    {
    }

    public virtual void PhysicsProcess(double delta)
    {
    }
}