//============================================================================
// SpinBookModel.cs: Code for the graphical/kinematic model of the book
//============================================================================
using Godot;
using System;

public partial class SpinBookModel : Node3D
{
	Transform3D tr;      // Transform
	Vector3 cgLoc;

	//------------------------------------------------------------------------
    // _Ready
    //------------------------------------------------------------------------
	public override void _Ready()
	{
		cgLoc = new Vector3();
		tr = new Transform3D();

		tr.Origin = cgLoc;
		tr.Basis = Basis.Identity;
	}

	//------------------------------------------------------------------------
    // setCGLoc: Set location of the center of mass
    //------------------------------------------------------------------------
    public void SetCGLoc(Vector3 org)
    {
		cgLoc = org;
		tr.Origin = cgLoc;

		Transform = tr;
	}

	//------------------------------------------------------------------------
    // SetOrientation:
    //------------------------------------------------------------------------
    public void SetOrientation(Quaternion q)
    {
		tr.Basis = new Basis(q);
		Transform = tr;
	}
}
