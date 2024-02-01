//============================================================================
// CharacterItf.cs   Abstract class for defining a characterinterface
//============================================================================
using Godot;
using System;

public abstract class CharacterItf
{
    public virtual void SetShoulderLAngleYXZ(float ay, float ax, float az)
    {
    }
    public virtual void SetElbowLAngle(float angle)
    {
    }

    public virtual void SetSimpleWaistTwist(float angle)
    {
    }

}