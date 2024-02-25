//============================================================================
// MCTestSimpleBC.cs    
// A simple mannequin control class
//============================================================================
using Godot;
using System;

public class MCTestSimpleBC : ManneControl
{
    CharacterItf modelItf;
    
    float angle1;
    float angle2;
    float angle3;
    float dAngle;

    // UI stuff
    VBoxContainer vboxTL;
    OptionButton optionModel;
    UIPanelDisplay datDisplay;
    Button genericButton;

    DatDisplay2 dd2;

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MCTestSimpleBC(CharacterItf mitf)
    {
        modelItf = mitf;

        angle1 = angle2 = angle3 = 0.0f;
        dAngle = 2.0f;
    }

    //------------------------------------------------------------------------
    // Init2: A second initialization method for....
    //------------------------------------------------------------------------
    public override void Init2()
    {
        base.Init2();

        SetupUI();
    }

    //------------------------------------------------------------------------
    // Process
    //------------------------------------------------------------------------
    public override void Process(double delta)
    {
        //base.Process(delta);

        //float waistAngle = (float)Math.Cos(time);
        //modelItf.SetSimpleWaistTwist(waistAngle);

        bool angleUpdate = false;
        if(Input.IsActionPressed("ui_right")){
            angle1 += dAngle;
            angleUpdate = true;
        }
        if(Input.IsActionPressed("ui_left")){
            angle1 -= dAngle;
            angleUpdate = true;
        }

        if(Input.IsActionPressed("ui_up")){
            angle2 += dAngle;
            angleUpdate = true;
        }
        if(Input.IsActionPressed("ui_down")){
            angle2 -= dAngle;
            angleUpdate = true;
        }

        if(Input.IsActionPressed("ui_other_right")){
            angle3 += dAngle;
            angleUpdate = true;
        }
        if(Input.IsActionPressed("ui_other_left")){
            angle3 -= dAngle;
            angleUpdate = true;
        }

        if(angleUpdate){
            float ang1Rad = Mathf.DegToRad(angle1);
            float ang2Rad = Mathf.DegToRad(angle2);
            float ang3Rad = Mathf.DegToRad(angle3);
            
            //modelItf.SetShoulderLAngleYZX(ang3Rad, ang1Rad, ang2Rad);
            modelItf.SetShoulderRAngleYZX(ang3Rad, ang1Rad, ang2Rad);

            Vector3 vv = Mathf.Sin(0.5f*ang1Rad) * Vector3.Up;
            Quaternion q = new Quaternion();
            q.W = Mathf.Cos(0.5f*ang1Rad);
            q.X = vv.X;
            q.Y = vv.Y;
            q.Z = vv.Z;
            modelItf.SetShoulderLQuat(q);
        }

        time += delta;
    }

    //------------------------------------------------------------------------
    // PhysicsProcess
    //------------------------------------------------------------------------
    public override void PhysicsProcess(double delta)
    {
        //base.PhysicsProcess(delta);
    }

    //------------------------------------------------------------------------
    // SetupUI
    //------------------------------------------------------------------------
    private void SetupUI()
    {
        vboxTL = new VBoxContainer();
        margContTL.AddChild(vboxTL);

        // Model option
        optionModel = new OptionButton();
        optionModel.Text = "Model Choice";
        optionModel.AddItem("X Bot", 0);
        optionModel.AddItem("Y Bot", 1);
        optionModel.AddItem("GymBlock", 2);
        vboxTL.AddChild(optionModel);

        dd2 = new DatDisplay2(vboxTL);
        dd2.SetNDisplay(3,true, true);
        dd2.SetTitle("Joint Angles");
        dd2.SetLabel(0, "Angle 1:");
        dd2.SetLabel(1, "Angle 2:");
        dd2.SetLabel(2, "Angle 3:");

        dd2.SetDigitsAfterDecimal(0, 1);
        dd2.SetDigitsAfterDecimal(1, 1);
        dd2.SetDigitsAfterDecimal(2, 1);

        dd2.SetSuffixDegree(0);
        dd2.SetSuffixDegree(1);
        dd2.SetSuffixDegree(2);

        dd2.SetValue(0, 0.0f);
        dd2.SetValue(1, 0.0f);
        dd2.SetValue(2, 0.0f);

        

        // genericButton = new Button();
        // genericButton.Text = "Generic Button";
        // vboxTL.AddChild(genericButton);
    }
}