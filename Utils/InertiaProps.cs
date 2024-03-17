using Godot;
using System;
using System.Collections.Generic;

public class InertiaProps
{
    public InertiaProps() {
        // TODO, add initializations here as needed
    }

    public void Initialize(Dictionary<SegmentType,float> segmentLength, float modelMass) {
        // TODO, allow derived classes to initialize stuff here
    }
    
    // For use for the simulation to get joint rest angles
    public float[,] GetJointAnglesMatrix() {
        throw new NotImplementedException();
    }

    public float[,] GetSegmentRotationMatrixToParent(SegmentType segment) {
        throw new NotImplementedException();
    }

    public float[,] GetSegmentRotationMatrixToRoot(SegmentType segment) {
        throw new NotImplementedException();
    }

    public float[] GetSegmentPrincipleMomentsOfInertia(SegmentType segment) {
        throw new NotImplementedException();
    }

}
