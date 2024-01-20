//============================================================================
// LinAlgEq.cs  Defines a class for a linear algebraic equations solver
//============================================================================
using System;

namespace SimUtils
{
    public class LinAlgEq
    {
        private int n = 3;       // number of active algebraic equations
        private int nCap;        // capacity of algebraic equations
        private double[][] _A;   // coefficient matrix
        private double[] _b;     // right hand side
        private double[][] M;    // augmented matrix
        private double[] _x;     // solution
        private bool[] toUseRow; // array of bools to indicate rows to use
        private bool[] toUseCol; // array of bools to indicate cols to use
        private int[] cIdx;      // array of column indices
        private int[] cInvIdx;   // inverse of cIdx
        private int[] rIdx;      // array of row indices
        private int[] rInvIdx;   // inverse of rIdx
        private bool toPivot;

        //--------------------------------------------------------------------
        // Constructor for the class.
        //--------------------------------------------------------------------
        public LinAlgEq(int nn = 3)
        {
            _b = new double[1];   // these three lines get rid of the warning
            _A = new double[1][];
            M  = new double[1][];
            _x = new double[1];

            toUseRow = new bool[1];
            toUseCol = toUseRow;
            cIdx = new int[1];
            cInvIdx = cIdx;
            rIdx = cIdx;
            rInvIdx = cIdx;

            toPivot = true;

            Resize(nn);
        }

        //--------------------------------------------------------------------
        // resize: resize the matrices to hold the right number of equations
        //         and unknowns.
        //--------------------------------------------------------------------
        public void Resize(int nn)
        {
            // ##### should check if nn is bigger than zero
            n = nn;
            nCap = nn;

            _b = new double[n];
            _A = new double[n][];
            M  = new double[n][];
            _x = new double[n];
            toUseRow = new bool[n];
            toUseCol = new bool[n];
            cIdx = new int[n+1];
            cInvIdx = new int[n+1];
            rIdx = new int[n];
            rInvIdx = new int[n];

            int i,j;
            for(i=0;i<n;++i)
            {
                _A[i] = new double[n];
                M[i] = new double[n+1];
                
                for(j=0;j<n;++j)
                {
                    _A[i][j] = 0.0;
                }
                _A[i][i] = 1.0;
                _b[i] = 0.0;
                _x[i] = 0.0;
                toUseRow[i] = true;
                toUseCol[i] = true;
                cIdx[i] = cInvIdx[i] = rIdx[i] = rInvIdx[i] = i;
            }
            cIdx[n] = n;
            cInvIdx[n] = n;
        }

        //--------------------------------------------------------------------
        // Deactivate: remove a row and column from the system of algebraic
        //             equations
        //--------------------------------------------------------------------
        public void Deactivate(int row, int col)
        {
            if(row < 0 || row>=nCap)   // out of range
                return;
            if(!toUseRow[row])      // row already deactivated
                return;

            if(col < 0 || col>=nCap)   // out of range
                return;
            if(!toUseCol[col])      // column already deactivated
                return;

            toUseRow[row] = false;
            toUseCol[col] = false;

            // process row
            int tgIdx = rInvIdx[row];
            rIdx[tgIdx] = rIdx[n-1];
            rInvIdx[rIdx[n-1]] = tgIdx;
            rIdx[n-1] = row;
            rInvIdx[row] = n-1;
            
            // process column
            tgIdx = cInvIdx[col];
            int i;
            for(i=tgIdx; i<n; ++i){
                cIdx[i] = cIdx[i+1];
                cInvIdx[cIdx[i]] = i;
            }
            cIdx[n] = col;
            cInvIdx[col] = n;
            --n;
        }

        //--------------------------------------------------------------------
        // Reactivate: return a previously deactivated row and column back to 
        //             the system of algebraic equations
        //--------------------------------------------------------------------
        public void Reactivate(int row, int col)
        {
            // if(row < 0 || row>=n)   // out of range
            //     return;
            // if(toUseRow[row])      // row already deactivated
            //     return;

            // if(col < 0 || col>=n)   // out of range
            //     return;
            // if(toUseCol[col])      // column already deactivated
            //     return;

            // toUseRow[row] = true;
            // toUseCol[col] = true;
            // ++n;
        }

        //--------------------------------------------------------------------
        // SolveGauss: Solve by Gauss Eliminition
        //--------------------------------------------------------------------
        public void SolveGauss()
        {
            // form augmented matrix
            int i, j, k;

            // for(i=0;i<nCap;++i)
            //     Console.Write(rIdx[i].ToString() + "  ");
            // Console.WriteLine();
            // for(i=0;i<nCap;++i)
            //     Console.Write(rInvIdx[i].ToString() + "  ");
            // Console.WriteLine();
            // for(j=0;j<=nCap;++j)
            //     Console.Write(cIdx[j].ToString() + "  ");
            // Console.WriteLine();
            // for(j=0;j<=nCap;++j)
            //     Console.Write(cInvIdx[j].ToString() + "  ");
            // Console.WriteLine();
            // Console.WriteLine();

            for(i=0;i<n;++i)
            {
                for(j=0; j<=n; ++j)
                {
                    if(cIdx[j] == nCap)
                        M[i][j] = _b[rIdx[i]];
                    else
                        M[i][j] = _A[rIdx[i]][cIdx[j]];
                    //Console.Write(M[i][j].ToString() + "  ");
                }
                //Console.WriteLine();
            }

            // perform Gauss elimination
            double fac;
            for(i=0;i<(n-1);++i)
            {
                if(toPivot)
                    PivotRow(i);

                for(j=i+1;j<n;++j)
                {
                    fac = M[j][i] / M[i][i];
                    for(k=i; k<=n;++k)
                    {
                        M[j][k] -= fac*M[i][k];
                    }
                }
            }

            // perform back substitution
            double sum;
            for(i=0;i<n;++i)
            {
                sum = M[n-i-1][n];
                for(j=n-1; j>(n-i-1); --j)
                {
                    sum -= M[n-i-1][j] * _x[j];
                }
                _x[n-i-1] = sum/M[n-i-1][n-i-1];
            }
            
        }

        //--------------------------------------------------------------------
        // PivotRow
        //--------------------------------------------------------------------
        public void PivotRow(int j)
        {
            double[] holder;
            double maxElem = Math.Abs(M[j][j]);
            int rowIdx = j;
            int i;

            for(i = j+1; i<n; ++i)
            {
                // find largest element in jth column
                if(Math.Abs(M[i][j])>maxElem)
                {
                    maxElem = Math.Abs(M[i][j]);
                    rowIdx = i;
                }

                // swap rows
                if(rowIdx != j)
                {
                    holder = M[j];
                    M[j] = M[rowIdx];
                    M[rowIdx] = holder;
                    //Console.WriteLine("Swap " + j.ToString());
                }
            }

        }

        //--------------------------------------------------------------------
        // SetAZero: Sets all elements of A to zero
        //--------------------------------------------------------------------
        public void SetAZero()
        {
            int i;
            int j;
            for(i=0;i<n;++i){
                for(j=0;j<n;++j){
                    _A[i][j] = 0.0;
                }
            }
        }

        //--------------------------------------------------------------------
        // SetABZero: Sets all elements of A and B to zero
        //--------------------------------------------------------------------
        public void SetABZero()
        {
            int i;
            int j;
            for(i=0;i<n;++i){
                for(j=0;j<n;++j){
                    _A[i][j] = 0.0;
                }
                _b[i] = 0.0;
            }
        }

        //--------------------------------------------------------------------
        // SetBZero: Sets all elements of B to zero
        //--------------------------------------------------------------------
        public void SetBZero()
        {
            int i;
            for(i=0;i<n;++i){
                _b[i] = 0.0;
            }
        }

        //--------------------------------------------------------------------
        // Check: checks the solution
        //--------------------------------------------------------------------
        public double Check()
        {
            double sum = 0.0;
            double sum2 = 0.0;

            int i,j;
            for(i=0;i<n;++i)
            {
                sum = 0.0;
                for(j=0;j<n;++j)
                {
                    sum += _A[i][j] * _x[j];
                }
                double delta = sum - _b[i];
                sum2 += delta*delta; 
            }

            return(Math.Sqrt(sum2/(1.0*n)));
        }

        //--------------------------------------------------------------------
        // GetPivotElement
        //--------------------------------------------------------------------
        public double GetPivotElement(int i)
        {
            return M[i][i];
        }

        //--------------------------------------------------------------------
        // GetSol: Gets the solution for provided variable index. This method
        //         is to be used when some of the rows/columns are deactivated
        //--------------------------------------------------------------------
        public double GetSol(int i)
        {
            if(!toUseCol[i])
                return(0.0);

            return(_x[cInvIdx[i]]);
        }  

        //--------------------------------------------------------------------
        // getters and setters 
        //--------------------------------------------------------------------
        public double[] b 
        {
            get
            {
                return _b;
            }            
            set
            {
                _b = value;
            }
        }

        public double[][] A
        {
            get
            {
                return _A;
            }
            set
            {
                _A = value;
            }
        }

        public double[] sol
        {
            get
            {
                return _x;
            }
        }
        public bool ToPivot
        {
            set
            {
                toPivot = value;
            }
        }
    }
}