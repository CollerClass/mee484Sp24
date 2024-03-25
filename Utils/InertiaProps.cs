using Godot;
using System;
using System.Collections.Generic;

public class InertiaProps
{
    public InertiaProps(Dictionary<SegmentType,float> segmentLength, float totalModelMass) {
        // TODO, add initializations here as needed
    }
    
    // Matrix of coordinates of child joints
    public float[,] GetSegmentChildJointLocations(SegmentType segment) {
        throw new NotImplementedException();
    }

    // Rotation basis matrix relative to parent
    public float[,] GetSegmentRotationMatrixToParent(SegmentType segment) {
        throw new NotImplementedException();
    }

    // Rotation basis matrix relative to fixed frame
    public float[,] GetSegmentRotationMatrixToNewtonian(SegmentType segment) {
        throw new NotImplementedException();
    }

    // Vector of I_Gx, I_Gz, I_Gz princple moments of intertia
    public float[] GetSegmentPrincipleMomentsOfInertia(SegmentType segment) {
        throw new NotImplementedException();
    }

    // Mass for this segment
    public float GetSegmentMass(SegmentType segment) {
        throw new NotImplementedException();
    }

    // XYZ vector of coordinates of center of mass relative to parent
    public float[] GetSegmentCenterOfMass(SegmentType segment) {
        throw new NotImplementedException();
    }

}
