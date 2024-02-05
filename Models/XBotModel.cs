using Godot;
using System;

public partial class XBotModel : Node3D
{

	Skeleton3D skel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// GD.Print("XBot Model");

		// skel = GetNode<Skeleton3D>("RootNode/Skeleton3D");
		// GD.Print("Hips " + skel.FindBone("mixamorig_Hips"));
		// GD.Print("RShoulder " + skel.FindBone("mixamorig_RightShoulder"));
		// GD.Print(skel.GetBonePoseRotation(7));
		// GD.Print(skel.GetBonePosePosition(7));
		
		// GD.Print("+++++++");
		// Transform3D tr = new Transform3D();
		// tr = skel.GetBoneRest(7);
		// GD.Print(tr.Origin);
		// GD.Print(tr.Basis.GetRotationQuaternion());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
