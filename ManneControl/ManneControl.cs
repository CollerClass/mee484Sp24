//============================================================================
// ManneControl.cs
//============================================================================
using Godot;
using System;

public abstract class ManneControl
{
    protected double time;

    public virtual void Process(double delta)
    {
    }

    public virtual void PhysicsProcess(double delta)
    {
    }
}