//============================================================================
// UIPanelDisplay.cs
//============================================================================
using Godot;
using System;

public class DatDisplay2{
    Container parent;
    GridContainer grid;
    VBoxContainer vbox;
    Button testButton;

    int nDisp;
    bool hasTitle;
    bool hasButtons;

    Label[] labels;
    Label[] values;
    private String[] fStrings;
    Label title;

    CheckBox[] checkBoxes;
    bool initialized;


    public DatDisplay2(Container pContainer)
    {
        GD.Print("DatDisplay2 constructor");
        parent = pContainer;

        vbox = new VBoxContainer();
        parent.AddChild(vbox);

        hasTitle = false;
        hasButtons = false;
        initialized = false;
        // testButton = new Button();
        // testButton.Text = "Test Button";
        // parent.AddChild(testButton);
    }

    public void SetNDisplay(int sz, bool _hasTitle = false, bool _hasButtons = false)
    {
        if(initialized)
            return;

        if (sz <= 0)
        {
            return;
        }

        hasTitle = _hasTitle;
        hasButtons = _hasButtons;

        if(hasTitle){
            title = new Label();
            title.Text = "Title";
            vbox.AddChild(title);
        }

        labels = new Label[sz];
        values = new Label[sz];
        fStrings = new String[sz];
        

        grid = new GridContainer();
        if(hasButtons){
            checkBoxes = new CheckBox[sz];
            grid.Columns = 3;
        }
        else
            grid.Columns = 2;
        vbox.AddChild(grid);

        for(int i=0;i<sz;++i)
        {
            labels[i] = new Label();
            values[i] = new Label();
            labels[i].Text = "Label not set";
            values[i].Text = "Value not set";
            //vBoxLabels.AddChild(labels[i]);
            //vBoxValues.AddChild(values[i]);
            grid.AddChild(labels[i]);
            grid.AddChild(values[i]);
            if(hasButtons)
                checkBoxes[i] = new CheckBox();
                grid.AddChild(checkBoxes[i]);

            fStrings[i] = "0.00";
        }
        //hbox.AddChild(vBoxLabels);
        //hbox.AddChild(vBoxValues);

        nDisp = sz;
    }

    public void SetTitle(string str)
    {
        if(!hasTitle)
            return;

        title.Text = str;       
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
}