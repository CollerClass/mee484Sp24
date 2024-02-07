//============================================================================
// UIPanelDisplay.cs
//============================================================================
using Godot;
using System;

public partial class UIPanelDisplay : PanelContainer
{
    int nDisp;  // number of displays to show
    private VBoxContainer vBoxLabels;
    private VBoxContainer vBoxValues;
    private Label[] labels;
    private Label[] values;
    private String[] fStrings;
    //VBoxContainer vbox;
    HBoxContainer hbox;

	//------------------------------------------------------------------------
    // _Ready: Called when the node enters the scene tree for the first time.
    //------------------------------------------------------------------------
    public override void _Ready()
    {
        nDisp = 0;
        hbox = GetNode<HBoxContainer>("HBox");
        vBoxLabels = new VBoxContainer();
        vBoxValues = new VBoxContainer();
    }

    //------------------------------------------------------------------------
    // SetNDisplay: Sets the number of quantities to be displayed
    //------------------------------------------------------------------------
    public void SetNDisplay(int sz)
    {
        if (sz <= 0)
        {
            return;
        }

        labels = new Label[sz];
        values = new Label[sz];
        fStrings = new String[sz];

        for(int i=0;i<sz;++i)
        {
            labels[i] = new Label();
            values[i] = new Label();
            labels[i].Text = "Label not set";
            values[i].Text = "Value not set";
            vBoxLabels.AddChild(labels[i]);
            vBoxValues.AddChild(values[i]);

            fStrings[i] = "0.00";
        }
        hbox.AddChild(vBoxLabels);
        hbox.AddChild(vBoxValues);

        nDisp = sz;
    }

    //------------------------------------------------------------------------
    // SetLabel: Sets a label string
    //------------------------------------------------------------------------
    public void SetLabel(int idx, string str)
    {
        if(idx < 0 || idx >= nDisp)
        {
            return;
        }

        labels[idx].Text = str;
    }

    //------------------------------------------------------------------------
    // SetLabel: Sets a label string
    //------------------------------------------------------------------------
    public void SetValue(int idx, float val)
    {
        if(idx < 0 || idx >= nDisp)
        {
            return;
        }

        values[idx].Text = val.ToString(fStrings[idx]);
    }

    public void SetValue(int idx, string str)
    {
        if(idx < 0 || idx >= nDisp)
        {
            return;
        }

        values[idx].Text = str;
    }

    //------------------------------------------------------------------------
    // SetDigitsAfterDecimal: Sets number of digits after decimal point
    //------------------------------------------------------------------------
    public void SetDigitsAfterDecimal(int idx, int n)
    {
        if(idx < 0 || idx >= nDisp)
            return;

        if(n < 0)
            return;

        if(n > 10)
            n = 10;

        fStrings[idx] = "0.";
        for(int i=0;i<n;++i)
            fStrings[idx] += "0";

        //GD.Print(fStrings[idx]);
    }

    //------------------------------------------------------------------------
    // SetColor:  Sets the color of a label and/or value at the provided index
    //------------------------------------------------------------------------
    public void SetColor(int idx, Color cc, bool lbl=true, bool val=true)
    {
        if(idx < 0 || idx >= nDisp)
            return;

        if(lbl)
            labels[idx].Set("theme_override_colors/font_color",cc);
        if(val)
            values[idx].Set("theme_override_colors/font_color",cc);
    }

	public void SetWhite(int idx, bool lbl=true, bool val=true)
    {
        if(idx < 0 || idx >= nDisp)
            return;

        if(lbl)
            labels[idx].Set("theme_override_colors/font_color",new Color(1,1,1));
        if(val)
            values[idx].Set("theme_override_colors/font_color",new Color(1,1,1));
    }

    public void SetYellow(int idx, bool lbl=true, bool val=true)
    {
        if(idx < 0 || idx >= nDisp)
            return;

        if(lbl)
            labels[idx].Set("theme_override_colors/font_color",new Color(1,1,0));
        if(val)
            values[idx].Set("theme_override_colors/font_color",new Color(1,1,0));
    }

    public void SetMagenta(int idx, bool lbl=true, bool val=true)
    {
        if(idx < 0 || idx >= nDisp)
            return;

        if(lbl)
            labels[idx].Set("theme_override_colors/font_color",new Color(1,0,1));
        if(val)
            values[idx].Set("theme_override_colors/font_color",new Color(1,0,1));
    }

    public void SetCyan(int idx, bool lbl=true, bool val=true)
    {
        if(idx < 0 || idx >= nDisp)
            return;

        if(lbl)
            labels[idx].Set("theme_override_colors/font_color",new Color(0,1,1));
        if(val)
            values[idx].Set("theme_override_colors/font_color",new Color(0,1,1));
    }
}
