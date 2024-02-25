//============================================================================
// MCTestSimpleBC.cs    
// A simple mannequin control class
//============================================================================
using Godot;
using System;

public class MCTestSimpleBC : ManneControl
{
    CharacterItf modelItf;
    
    float[] angles;
    float dAngle;

    // UI stuff
    VBoxContainer vboxTL;
    OptionButton optionModel;
    UIPanelDisplay datDisplay;
    Button genericButton;
    DatDisplay2 dd2;

    HBoxContainer adjHbox;
    Button[] adjButtons;
    bool adjFastPress;

    //------------------------------------------------------------------------
    // Constructor
    //------------------------------------------------------------------------
    public MCTestSimpleBC(CharacterItf mitf)
    {
        modelItf = mitf;

        angles = new float[3];
        angles[0] = angles[1] = angles[2] = 0.0f;
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
        // if(Input.IsActionPressed("ui_right")){
        //     angles[0] += dAngle;
        //     angleUpdate = true;
        // }
        // if(Input.IsActionPressed("ui_left")){
        //     angles[0] -= dAngle;
        //     angleUpdate = true;
        // }

        // if(Input.IsActionPressed("ui_up")){
        //     angles[1] += dAngle;
        //     angleUpdate = true;
        // }
        // if(Input.IsActionPressed("ui_down")){
        //     angles[1] -= dAngle;
        //     angleUpdate = true;
        // }

        // if(Input.IsActionPressed("ui_other_right")){
        //     angles[2] += dAngle;
        //     angleUpdate = true;
        // }
        // if(Input.IsActionPressed("ui_other_left")){
        //     angles[2] -= dAngle;
        //     angleUpdate = true;
        // }

        if(angleUpdate){
            float ang1Rad = Mathf.DegToRad(angles[0]);
            float ang2Rad = Mathf.DegToRad(angles[1]);
            float ang3Rad = Mathf.DegToRad(angles[2]);
            
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

        // Data display
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

        // Adjustment Buttons
        adjHbox = new HBoxContainer();
        vboxTL.AddChild(adjHbox);

        adjButtons = new Button[5];
        adjButtons[0] = new Button();
        adjButtons[0].Text = "<<";
        adjButtons[0].Pressed += OnAdjButtonFastL;

        adjButtons[1] = new Button();
        adjButtons[1].Text = ">>";
        adjButtons[1].Pressed += OnAdjButtonFastR;

        adjButtons[2] = new Button();
        adjButtons[2].Text = "<";
        adjButtons[2].Pressed += OnAdjButtonSlowL;

        adjButtons[3] = new Button();
        adjButtons[3].Text = ">";
        adjButtons[3].Pressed += OnAdjButtonSlowR;

        adjButtons[4] = new Button();
        adjButtons[4].Text = "R";
        adjButtons[4].Pressed += OnAdjButtonReset;

        adjHbox.AddChild(adjButtons[0]);
        adjHbox.AddChild(adjButtons[2]);
        adjHbox.AddChild(adjButtons[4]);
        adjHbox.AddChild(adjButtons[3]);
        adjHbox.AddChild(adjButtons[1]);

        adjFastPress = false;

        // genericButton = new Button();
        // genericButton.Text = "Generic Button";
        // vboxTL.AddChild(genericButton);
    }

    //------------------------------------------------------------------------
    // adjButtonHandlers
    //------------------------------------------------------------------------
    private void OnAdjButtonFastL()
    {
        GD.Print("OnAdjButtonFastL");
    }
    private void OnAdjButtonFastR()
    {
        GD.Print("OnAdjButtonFastR");
    }
    private void OnAdjButtonSlowL()
    {
        GD.Print("OnAdjButtonSlowL");
    }
    private void OnAdjButtonSlowR()
    {
        GD.Print("OnAdjButtonSlowR");
    }
    private void OnAdjButtonReset()
    {
        GD.Print("OnAdjButtonReset");
    }
}