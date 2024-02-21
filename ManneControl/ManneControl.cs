//============================================================================
// ManneControl.cs
//============================================================================
using Godot;
using System;

public abstract class ManneControl
{
    protected double time;

    protected MarginContainer margContTL;
    protected MarginContainer margContTR;
    protected MarginContainer margContBL;
    protected MarginContainer margContBR;

    public virtual void Process(double delta)
    {
    }

    public virtual void PhysicsProcess(double delta)
    {
    }

    public virtual void Init2()
    {
    }

    // MarginContainer setters
    public MarginContainer MarginContainerTL
    {
        set{
            margContTL = value;
        }
    }
}