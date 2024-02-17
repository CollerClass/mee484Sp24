using Godot;
using System;

public partial class VectorPointer : Node3D
{
	MeshInstance3D body;
	CylinderMesh bodyMesh;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		body = GetNode<MeshInstance3D>("Body");
		bodyMesh = (CylinderMesh)body.Mesh;
	}

	public void SetBodyRadius(float rad)
	{
		bodyMesh.TopRadius = rad;
		bodyMesh.BottomRadius = rad;
	}

	public void SetLength(float len)
	{
		bodyMesh.Height = len;
		body.Position = new Vector3(0.0f, 0.5f*len, 0.0f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
