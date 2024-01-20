//============================================================================
// SpinBook.cs  
// Class for defining the dynamics of a spinning "book".
//============================================================================
using System;
using SimUtils;

namespace Sim
{    
    public class SpinBook : Simulator
    {
        private double I1 = 1.0;   // Principal moments of inertia
        private double I2 = 2.0;
        private double I3 = 3.0;

        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public SpinBook() : base(7)
        {
            
            // default initial conditions
            x[0] = 1.0;    // q0
            x[1] = 0.0;    // q1
            x[2] = 0.0;    // q2
            x[3] = 0.0;    // q3
            x[4] = 0.05;    // u1   body angular velocity
            x[5] = 1.05;   // u2   body angular velocity
            x[6] = 0.0;    // u3   body angular velocity

            SetRHSFunc(RHSFunc);
        }

        //--------------------------------------------------------------------
        // rhsFunc: function which calculates the right side of the
        //          differential equation.
        //--------------------------------------------------------------------
        public void RHSFunc(double[] st, double t, double[] ff)
        {
            double Q0 = st[0];
            double Q1 = st[1];
            double Q2 = st[2];
            double Q3 = st[3];

            double u1 = st[4];
            double u2 = st[5];
            double u3 = st[6];

            // kinematic differential equations
            ff[0] = 0.5*(-Q1*u1 - Q2*u2 - Q3*u3);
            ff[1] = 0.5*( Q0*u1 - Q3*u2 + Q2*u3);
            ff[2] = 0.5*( Q3*u1 + Q0*u2 - Q1*u3);
            ff[3] = 0.5*(-Q2*u1 + Q1*u2 + Q0*u3);
    
            // kinetics
            ff[4] = (I2 - I3)*u2*u3/I1;
            ff[5] = (I3 - I1)*u1*u3/I2;
            ff[6] = (I1 - I2)*u1*u2/I3;

        }

        //--------------------------------------------------------------------
        // getters/setters
        //--------------------------------------------------------------------
        public double q0
        {
            get{ return x[0]; }
        }

        public double q1
        {
            get{ return x[1]; }
        }
        public double q2
        {
            get{ return x[2]; }
        }
        public double q3
        {
            get{ return x[3]; }
        }
        public double omega1
        {
            get{ return x[4]; }

            set{ x[4] = value; }
        }

        public double omega2
        {
            get{ return x[5]; }

            set{ x[5] = value; }
        }

        public double omega3
        {
            get{ return x[6]; }

            set{ x[6] = value; }
        }
        public double IG1
        {
            get{ return I1; }

            set
            {
                if(value >= 0.1)
                    I1 = value;
            }
        }
        public double IG2
        {
            get{ return I2; }

            set
            {
                if(value >= 0.1)
                    I2 = value;
            }
        }
        public double IG3
        {
            get{ return I3; }

            set
            {
                if(value >= 0.1)
                    I3 = value;
            }
        }
    }

}