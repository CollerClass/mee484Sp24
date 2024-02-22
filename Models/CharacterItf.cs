//============================================================================
// CharacterItf.cs   Abstract class for defining a characterinterface
//============================================================================
using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

public abstract class CharacterItf
{
    private Quaternion qA;
    private Quaternion qB;
    private Quaternion qC;
    public Quaternion qResult;

    protected Vector3 uVec;

    public CharacterItf()
    {
        //GD.Print("CharacterItf Constructor");
        uVec = new Vector3();
    }
    
    public virtual ImmutableDictionary<JointType,Vector3> HingeVectors() 
    {
        throw new NotImplementedException();
    }

    protected void QuatCalcEulerYXZ(float ax, float ay, float az)
    {
        // in the future, I don't want to do this with a constructor
        qA = new Quaternion(Vector3.Up, ay);
        qB = new Quaternion(Vector3.Right, ax);//right in positive x direction
        qC = new Quaternion(Vector3.Back, az); // back in positive z direction

        qResult = qA*qB*qC;
    }

    public void QuatCalcEulerYZX(float ax, float ay, float az)
    {
        // in the future, I don't want to do this with a constructor
        qA = new Quaternion(Vector3.Up, ay);
        qB = new Quaternion(Vector3.Back, az); // back in positive z direction
        qC = new Quaternion(Vector3.Right, ax);//right in positive x direction
    
        qResult = qA*qB*qC;
    }

    public virtual void SetShoulderLAngleYXZ(float ax, float ay, float az)
    {
    }

    public virtual void SetShoulderRAngleYXZ(float ax, float ay, float az)
    {
    }

    public virtual void SetShoulderLAngleYZX(float ax, float ay, float az)
    {
    }

    public virtual void SetShoulderRAngleYZX(float ax, float ay, float az)
    {
    }

    public virtual void SetShoulderLQuat(Quaternion q)
    {
    }

    public virtual void SetShoulderRQuat(Quaternion q)
    {
    }

    public virtual void SetElbowLAngle(float angle)
    {
    }

    public virtual void SetElbowRAngle(float angle)
    {
    }

    public virtual void SetSimpleWaistTwist(float angle)
    {
    }
    public virtual void SetSimpleMidTorsoTwist(float angle)
    {
    }

    public virtual void SetHipLAngle(Quaternion q)
    {
    }

    public virtual void SetHipRAngle(Quaternion q)
    {
    }

    public virtual void SetKneeLAngle(float angle)
    {
    }

    public virtual void SetKneeRAngle(float angle)
    {
    }

    public virtual void ResetAllJoints()
    {
    }

    public virtual void ResetJoint(JointType jointType) 
    {
    }

    public virtual Quaternion GetJointQuat(JointType jointType)
    {
        throw new NotImplementedException();
    }

    public virtual void SetJointQuat(JointType jointType, Quaternion newQuat)
    {
        throw new NotImplementedException();
    }
}