//============================================================================
// Simulator.cs : Defines the base class for creating simulations.
//============================================================================

using System;

namespace SimUtils
{
    public class Simulator
    {

        protected int n;           // number of first order odes
        protected double[] x;      // array of states
        protected double[] xi;     // array of intermediate states
        protected double[][] f;    // 2d array that holds values of rhs
        protected int nAuxDat;     // number of data in auxData
        protected double[] auxDat; // Array to hold aux data
        private Action<double[], double, double[]> rhsFunc;

        protected int subStepNo; // which sub-step one is on during current
                                 //      integration step

        //--------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------
        public Simulator(int nn)
        {
            //Console.WriteLine("Simulator Constructor");
            n = nn;
            x = new double[n];
            xi = new double[n];
            f = new double[4][];
            f[0] = new double[n];
            f[1] = new double[n];
            f[2] = new double[n];
            f[3] = new double[n];

            subStepNo = 0;
            nAuxDat = 0;
            auxDat = new double[1];

            rhsFunc = nothing;
        }

        //--------------------------------------------------------------------
        // StepEuler: Executes one numerical integration step using Euler's 
        //            method.
        //--------------------------------------------------------------------
        public void StepEuler(double time, double dTime)
        {
            int i;

            subStepNo = 0;
            rhsFunc(x,time,f[0]);
            for(i=0;i<n;++i)
            {
                x[i] += f[0][i] * dTime;
            }
        }

        //--------------------------------------------------------------------
        // StepRK2: Executes one numerical integration step using the RK2 
        //            method.
        //--------------------------------------------------------------------
        public void StepRK2(double time, double dTime)
        {
            int i;

            subStepNo = 0;
            rhsFunc(x,time,f[0]);
            for(i=0;i<n;++i)
            {
                xi[i] = x[i] + f[0][i] * dTime;
            }

            subStepNo = 1;
            rhsFunc(xi,time+dTime,f[1]);
            for(i=0;i<n;++i)
            {
                x[i] += 0.5*(f[0][i] + f[1][i])*dTime;
            }

            subStepNo = 0;
        }

        //--------------------------------------------------------------------
        // Step: Executes one numerical integration step using the RK4 
        //            method.
        //--------------------------------------------------------------------
        public void Step(double time, double dTime)
        {
            int i;

            double dtByTwo = 0.5*dTime;

            subStepNo = 0;
            rhsFunc(x,time,f[0]);
            for(i=0;i<n;++i)
            {
                xi[i] = x[i] + f[0][i] * dtByTwo;
            }

            subStepNo = 1;
            rhsFunc(xi,time+dtByTwo,f[1]);
            for(i=0;i<n;++i)
            {
                xi[i] = x[i] + f[1][i] * dtByTwo;
            }

            subStepNo = 2;
            rhsFunc(xi,time+dtByTwo,f[2]);
            for(i=0;i<n;++i)
            {
                xi[i] = x[i] + f[2][i] * dTime;
            }

            subStepNo = 3;
            rhsFunc(xi,time+dTime,f[3]);
            for(i=0;i<n;++i)
            {
                x[i] += (f[0][i] + 2.0*f[1][i] + 2.0*f[2][i] + f[3][i]) * 
                    dTime/6.0;
            }

            subStepNo = 0;
        }

        //--------------------------------------------------------------------
        // StateString: constructs a string in csv format that contains the
        //              state and Aux data.
        //--------------------------------------------------------------------
        public string StateString(double time)
        {
            string s = time.ToString();

            int i;
            for (i=0; i<n; ++i)
            {
                s += ',' + x[i].ToString();
            }
            for(i=0; i<nAuxDat; ++i)
            {
                s += ',' + auxDat[i].ToString();
            }

            return s;
        }

        //--------------------------------------------------------------------
        // ResizeAuxDat
        //--------------------------------------------------------------------
        protected void ResizeAuxDat(int nn)
        {
            if(nn <= 0)
                return;

            nAuxDat = nn;
            auxDat = new double[nn];
        }

        //--------------------------------------------------------------------
        // GetAuxDat:
        //--------------------------------------------------------------------
        public double GetAuxDat(int i)
        {
            if(i>0 && i<nAuxDat)
                return auxDat[i];

            return 0.0;
        }

        //--------------------------------------------------------------------
        // AuxString: constructs a string in csv format that contains the
        //            aux data.
        //--------------------------------------------------------------------
        public string AuxString(double time)
        {
            string s = time.ToString();

            for (int i = 0;i<nAuxDat;++i)
            {
                s += ',' + auxDat[i].ToString();
            }
            return s;
        }

        //--------------------------------------------------------------------
        // SetRHSFunc: Receives function from child to calculate rhs of ODE.
        //--------------------------------------------------------------------
        protected void SetRHSFunc(Action<double[],double,double[]> rhs)
        {
            rhsFunc = rhs;
        }

        private void nothing(double[] st,double t,double[] ff)
        {

        }

        //--------------------------------------------------------------------
        // Getter
        //--------------------------------------------------------------------
        int SubStepNumber
        {
            get{
                return subStepNo;
            }
        }

    }  // end class Simulator
}