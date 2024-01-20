using Godot;
using System;

public partial class CamRig : Node3D
{
    private float longitude = 0.0f; // longitude angle in radians
    private float latitude = 0.0f;  // latitude angle in radians
    private Vector3 sphAngles;      // container for the longit. & lat angles
    private Vector3 tgLoc;          // target location
    private Camera3D cam;           // reference to the camera; why?
    private Vector3 camLoc;         // camera location

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sphAngles = new Vector3(-latitude, longitude, 0.0f);
        setSphericalAngles();

        tgLoc = new Vector3();

        cam = GetNode<Camera3D>("Camera3D");
        camLoc = new Vector3(0.0f, 0.0f, 3.0f);
        cam.Position = camLoc;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//------------------------------------------------------------------------
    // setSphericalAngles: sets the longitude and latitude angles for the 
    //                     camera.
    //------------------------------------------------------------------------
    private void setSphericalAngles()
    {
        Rotation = sphAngles;
    }

    //------------------------------------------------------------------------
    // setter for longitude, but in degrees
    //------------------------------------------------------------------------
    public float LongitudeDeg
    {
        set
        {
            longitude = Mathf.DegToRad(value);
            sphAngles.Y = longitude;
            setSphericalAngles();
        }
    }

    //------------------------------------------------------------------------
    // setter for latitude, but in degrees
    //------------------------------------------------------------------------
    public float LatitudeDeg
    {
        get{ return(Mathf.RadToDeg(latitude)); }

        set
        {
            float v = value;
            if(v > 90.0f)
                v = 90.0f;
            else if(v < -90.0f)
                v = -90.0f;
            
            latitude = Mathf.DegToRad(v);

            sphAngles.X = -latitude;
            setSphericalAngles();
        }
    }

    //------------------------------------------------------------------------
    // setter distance
    //------------------------------------------------------------------------
    public float Distance
    {
        get{ return camLoc.Z; }

        set{
            if(value <= 0.0f)
                return;

            camLoc.Z = value;
            cam.Position = camLoc;
        }
    }

    //------------------------------------------------------------------------
    // setter for target at which camera points
    //------------------------------------------------------------------------
    public Vector3 Target
    {
        set{
            tgLoc = value;
            Position = tgLoc;
        }
    }

    //------------------------------------------------------------------------
    // setter for camera field of view in degrees
    //------------------------------------------------------------------------
    public float FOVDeg
    {
        set{
            if(value < 120.0f && value > 5.0f)
            {
                cam.Fov = value;
            }
        }
    }

    public void SetOrthographic(float sz)
    {
        cam.Projection = Camera3D.ProjectionType.Orthogonal;
        cam.Size = sz;
    }
}
