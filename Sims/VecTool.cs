//============================================================================
// VecTool.cs:  A set of simple tools for performing vector operations
//============================================================================
using System;

namespace SimUtils
{
    public class VecTool
    {
        double[] res;    // result vector
        double[,] R;     // dcm (rotation) matrix
        double _q0;
        double _q1;
        double _q2;
        double _q3;
        double q0Sq, q1Sq, q2Sq, q3Sq;
        double magQSq;

        //--------------------------------------------------------------------
        // Constructor for the class.
        //--------------------------------------------------------------------
        public VecTool()
        {
            res = new double[3];
            R = new double[3,3];
            _q0 = 1.0;
            _q1 = _q2 = _q3 = 0.0;
            magQSq = 1.0;
        }

        //--------------------------------------------------------------------
        // MapToFrame
        //--------------------------------------------------------------------
        public void MapToFrame(double[,] dcm, double[] v, double[] rr)
        {
            res = rr;

            res[0] = dcm[0,0]*v[0] + dcm[0,1]*v[1] + dcm[0,2]*v[2];
            res[1] = dcm[1,0]*v[0] + dcm[1,1]*v[1] + dcm[1,2]*v[2];
            res[2] = dcm[2,0]*v[0] + dcm[2,1]*v[1] + dcm[2,2]*v[2];
        }

        public void MapToFrameAdd(double[,] dcm, double[] v)
        {
            res[0] += dcm[0,0]*v[0] + dcm[0,1]*v[1] + dcm[0,2]*v[2];
            res[1] += dcm[1,0]*v[0] + dcm[1,1]*v[1] + dcm[1,2]*v[2];
            res[2] += dcm[2,0]*v[0] + dcm[2,1]*v[1] + dcm[2,2]*v[2];
        }

        //--------------------------------------------------------------------
        // MapBasisVecToFrame: Produces the ith column of DCM
        //--------------------------------------------------------------------
        public void MapBasisVecToFrame(double[,] dcm, int i, double[] rr)
        {
            if(i<0 || i>2)
                return;

            res = rr;

            res[0] = dcm[0,i];
            res[1] = dcm[1,i];
            res[2] = dcm[2,i];
        }

        //--------------------------------------------------------------------
        // MapBasisVecToFrameN3L: Newton's Third Law version of 
        //       MapsBasisVecToFrame. Puts the normal result into the normal
        //       result vector. Puts the result with opposite sign into 
        //       altRes.
        //--------------------------------------------------------------------
        public void MapBasisVecToFrameN3L(double[,] dcm, int i, double[] rr,
            double[] altRes)
        {
            if(i<0 || i>2)
                return;

            res = rr;

            res[0] = dcm[0,i];
            res[1] = dcm[1,i];
            res[2] = dcm[2,i];

            altRes[0] = -res[0];
            altRes[1] = -res[1];
            altRes[2] = -res[2];
        }

        //--------------------------------------------------------------------
        // Dot: Dot product
        //--------------------------------------------------------------------
        public double Dot(double[] vA, double[] vB)
        {
            return(vA[0]*vB[0] + vA[1]*vB[1] + vA[2]*vB[2]);
        }

        //--------------------------------------------------------------------
        // Cross: Cross product
        //--------------------------------------------------------------------
        public void Cross(double[] vA, double[] vB, double[] rr)
        {
            res = rr;

            res[0] = vA[1]*vB[2] - vA[2]*vB[1];
            res[1] = vA[2]*vB[0] - vA[0]*vB[2];
            res[2] = vA[0]*vB[1] - vA[1]*vB[0];
        }

        //--------------------------------------------------------------------
        // CrossAdd: Cross product, then add to result
        //--------------------------------------------------------------------
        public void CrossAdd(double[] vA, double[] vB, double[] rr)
        {
            res = rr;

            res[0] += vA[1]*vB[2] - vA[2]*vB[1];
            res[1] += vA[2]*vB[0] - vA[0]*vB[2];
            res[2] += vA[0]*vB[1] - vA[1]*vB[0];
        }

        public void CrossAdd(double[] vA, double[] vB)
        {
            res[0] += vA[1]*vB[2] - vA[2]*vB[1];
            res[1] += vA[2]*vB[0] - vA[0]*vB[2];
            res[2] += vA[0]*vB[1] - vA[1]*vB[0];
        }

        //--------------------------------------------------------------------
        // SetBaseVector
        //--------------------------------------------------------------------
        public void SetBaseVector(double[] rr)
        {
            res = rr;
        }

        //--------------------------------------------------------------------
        // ZeroBaseVector
        //--------------------------------------------------------------------
        public void ZeroBaseVector(double[] rr)
        {
            res = rr;
            res[0] = 0.0;
            res[1] = 0.0;
            res[2] = 0.0;
        }

        //--------------------------------------------------------------------
        // Copy:
        //--------------------------------------------------------------------
        public void Copy(double[] vA, double[] rr)
        {
            res = rr;
            res[0] = vA[0];
            res[1] = vA[1];
            res[2] = vA[2];
        }

        public void Copy(double[,] M, double[,] Mtg)
        {
            Mtg[0,0] = M[0,0];  Mtg[0,1] = M[0,1];  Mtg[0,2] = M[0,2];
            Mtg[1,0] = M[1,0];  Mtg[1,1] = M[1,1];  Mtg[1,2] = M[1,2];
            Mtg[2,0] = M[2,0];  Mtg[2,1] = M[2,1];  Mtg[2,2] = M[2,2];
        }

        //--------------------------------------------------------------------
        // ZeroBaseVector
        //--------------------------------------------------------------------
        public void ZeroBaseVector()
        {
            res[0] = 0.0;
            res[1] = 0.0;
            res[2] = 0.0;
        }

        //--------------------------------------------------------------------
        // Add:
        //--------------------------------------------------------------------
        public void Add(double[] v1, double[] v2, double[] rr)
        {
            res = rr;

            res[0] = v1[0] + v2[0];
            res[1] = v1[1] + v2[1];
            res[2] = v1[2] + v2[2];
        }

        //--------------------------------------------------------------------
        // AddTo:
        //--------------------------------------------------------------------
        public void AddTo(double[] v, double[] rr)
        {
            res = rr;

            res[0] += v[0];
            res[1] += v[1];
            res[2] += v[2];
        }

        public void AddTo(double sc,double[] v, double[] rr)
        {
            res = rr;

            res[0] += sc*v[0];
            res[1] += sc*v[1];
            res[2] += sc*v[2];
        }

        public void AddTo(double[] v)
        {
            res[0] += v[0];
            res[1] += v[1];
            res[2] += v[2];
        }

        public void AddTo(double sc,double[] v)
        {
            res[0] += sc*v[0];
            res[1] += sc*v[1];
            res[2] += sc*v[2];
        }

        //--------------------------------------------------------------------
        // SubtractFrom:
        //--------------------------------------------------------------------
        public void SubtractFrom(double[] v)
        {
            res[0] -= v[0];
            res[1] -= v[1];
            res[2] -= v[2];
        }
        public void SubtractFrom(double sc,double[] v)
        {
            res[0] -= sc*v[0];
            res[1] -= sc*v[1];
            res[2] -= sc*v[2];
        }

        //--------------------------------------------------------------------
        // Subtract:
        //--------------------------------------------------------------------
        public void Subtract(double[] vA, double[] vB, double[] rr)
        {
            res = rr;

            res[0] = vA[0] - vB[0];
            res[1] = vA[1] - vB[1];
            res[2] = vA[2] - vB[2];
        }

        //--------------------------------------------------------------------
        // ScalarMult:
        //--------------------------------------------------------------------
        public void ScalarMult(double sc, double[] vA, double[] rr)
        {
            res = rr;

            res[0] = sc*vA[0];
            res[1] = sc*vA[1];
            res[2] = sc*vA[2];
        }

        public void ScalarMult(double sc)
        {
            res[0] *= sc;
            res[1] *= sc;
            res[2] *= sc;
        }

        //--------------------------------------------------------------------
        // MatVecMult: Calculate the product of a matrix and a vector
        //--------------------------------------------------------------------
        public void MatVecMult(double[,] M, double[] v, double[] rr)
        {
            res = rr;

            res[0] = M[0,0]*v[0] + M[0,1]*v[1] + M[0,2]*v[2];
            res[1] = M[1,0]*v[0] + M[1,1]*v[1] + M[1,2]*v[2];
            res[2] = M[2,0]*v[0] + M[2,1]*v[1] + M[2,2]*v[2];
        }

        //--------------------------------------------------------------------
        // MatMatMult: multiply two matrices.
        //--------------------------------------------------------------------
        public void MatMatMult(double[,] A, double[,] B, double[,] result)
        {
            int i;
            int j;
            int k;

            for(i=0;i<3;++i)
            {
                for(j=0;j<3;++j)
                {
                    result[i,j] = 0.0;
                    for(k=0;k<3;++k)
                    {
                        result[i,j] += A[i,k] * B[k,j];
                    }
                }
            }

        }

        //--------------------------------------------------------------------
        // Quat2DCM:
        //--------------------------------------------------------------------
        public void Quat2DCM(double q0, double q1, double q2, double q3,
            double[,] dcm)
        {
            q0Sq = q0*q0;
            q1Sq = q1*q1;
            q2Sq = q2*q2;
            q3Sq = q3*q3;

            dcm[0,0] = q0Sq + q1Sq - q2Sq - q3Sq;
            dcm[0,1] = 2.0*(q1*q2 - q0*q3);
            dcm[0,2] = 2.0*(q0*q2 + q1*q3);

            dcm[1,0] = 2.0*(q0*q3 + q1*q2);
            dcm[1,1] = q0Sq - q1Sq + q2Sq - q3Sq;
            dcm[1,2] = 2.0*(q2*q3 - q0*q1);

            dcm[2,0] = 2.0*(q1*q3 - q0*q2);
            dcm[2,1] = 2.0*(q0*q1 + q2*q3);
            dcm[2,2] = q0Sq - q1Sq - q2Sq + q3Sq;
        }

        //--------------------------------------------------------------------
        // MapEulerYZXtoQuat
        //--------------------------------------------------------------------
        public void MapEulerYZXtoQuat(double psi, double theta, double phi)
        {
            MapEulerYZXtoDCM(psi,theta,phi);
            MapRotationMat2Quat();
        }

        //--------------------------------------------------------------------
        // MapEulerYZXtoDCM
        //--------------------------------------------------------------------
        public void MapEulerYZXtoDCM(double psi, double theta, double phi)
        {
            double cPsi = Math.Cos(psi);
            double sPsi = Math.Sin(psi);
            double cTheta = Math.Cos(theta);
            double sTheta = Math.Sin(theta);
            double cPhi = Math.Cos(phi);
            double sPhi = Math.Sin(phi);

            R[0,0] = cPsi*cTheta;
            R[0,1] = sPhi*sPsi - sTheta*cPhi*cPsi;
            R[0,2] = sPhi*sTheta*cPsi + sPsi*cPhi;

            R[1,0] = sTheta;
            R[1,1] = cPhi*cTheta;
            R[1,2] = -sPhi*cTheta;

            R[2,0] = -sPsi*cTheta;
            R[2,1] = sPhi*cPsi+sPsi*sTheta*cPhi;
            R[2,2] = -sPhi*sPsi*sTheta + cPhi*cPsi;
        }

        public void MapEulerYZXtoDCM(double psi, double theta, double phi,
            double[,] dcm)
        {
            double cPsi = Math.Cos(psi);
            double sPsi = Math.Sin(psi);
            double cTheta = Math.Cos(theta);
            double sTheta = Math.Sin(theta);
            double cPhi = Math.Cos(phi);
            double sPhi = Math.Sin(phi);

            dcm[0,0] = cPsi*cTheta;
            dcm[0,1] = sPhi*sPsi - sTheta*cPhi*cPsi;
            dcm[0,2] = sPhi*sTheta*cPsi + sPsi*cPhi;

            dcm[1,0] = sTheta;
            dcm[1,1] = cPhi*cTheta;
            dcm[1,2] = -sPhi*cTheta;

            dcm[2,0] = -sPsi*cTheta;
            dcm[2,1] = sPhi*cPsi+sPsi*sTheta*cPhi;
            dcm[2,2] = -sPhi*sPsi*sTheta + cPhi*cPsi;
        }

        //--------------------------------------------------------------------
        // MapEulerYtoDCM:
        //--------------------------------------------------------------------
        public void MapEulerYtoDCM(double psi, double[,] dcm)
        {
            double cPsi = Math.Cos(psi);
            double sPsi = Math.Sin(psi);

            dcm[0,0] = cPsi;
            dcm[0,1] = 0.0;
            dcm[0,2] = sPsi;

            dcm[1,0] = 0.0;
            dcm[1,1] = 1.0;
            dcm[1,2] = 0.0;

            dcm[2,0] = -sPsi;
            dcm[2,1] = 0.0;
            dcm[2,2] = cPsi;
        }

        //--------------------------------------------------------------------
        // MapEulerXtoDCM:
        //--------------------------------------------------------------------
        public void MapEulerXtoDCM(double phi, double[,] dcm)
        {
            double cPhi = Math.Cos(phi);
            double sPhi = Math.Sin(phi);

            dcm[0,0] = 1.0;
            dcm[0,1] = 0.0;
            dcm[0,2] = 0.0;

            dcm[1,0] = 0.0;
            dcm[1,1] = cPhi;
            dcm[1,2] = -sPhi;

            dcm[2,0] = 0.0;
            dcm[2,1] = sPhi;
            dcm[2,2] = cPhi;
        }

        //--------------------------------------------------------------------
        // MapRotationMat2Quat
        //--------------------------------------------------------------------
        public void MapRotationMat2Quat()
        {
            double tr = R[0,0] + R[1,1] + R[2,2];
            double s;

            if(tr > 0.0)
            {
                s = Math.Sqrt(tr + 1.0);
                _q0 = s*0.5;
                s = 0.5/s;
                _q1 = (-R[1,2] + R[2,1])*s;
                _q2 = (-R[2,0] + R[0,2])*s;
                _q3 = (-R[0,1] + R[1,0])*s;
                magQSq = _q0*_q0 + _q1*_q1 + _q2*_q2 + _q3*_q3;

                return;
            }

            if((R[0,0]>R[1,1]) && (R[0,0]>R[2,2]))
            {
                s = 2.0*Math.Sqrt(1.0+R[0,0]-R[1,1]-R[2,2]);
                _q0 = (R[2,1] - R[1,2])/s;
                _q1 = 0.25*s;
                _q2 = (R[0,1] + R[1,0])/s;
                _q3 = (R[0,2] + R[2,0])/s;
                magQSq = _q0*_q0 + _q1*_q1 + _q2*_q2 + _q3*_q3;

                return;
            }

            if(R[1,1] > R[2,2])
            {
                s = 2.0*Math.Sqrt(1.0 + R[1,1] - R[0,0] - R[2,2]);
                _q0 = (R[0,2] - R[2,0])/s;
                _q1 = (R[0,1] + R[1,0])/s;
                _q2 = 0.25*s;
                _q3 = (R[1,2] + R[2,1])/s;
                magQSq = _q0*_q0 + _q1*_q1 + _q2*_q2 + _q3*_q3;

                return;
            }

            s = 2.0*Math.Sqrt(1.0 + R[2,2] - R[0,0] - R[1,1]);
            _q0 = (R[1,0] - R[0,1])/s;
            _q1 = (R[0,2] + R[2,0])/s;
            _q2 = (R[1,2] + R[2,1])/s;
            _q3 = 0.25*s;
            magQSq = _q0*_q0 + _q1*_q1 + _q2*_q2 + _q3*_q3;

            return;
        }

        //--------------------------------------------------------------------
        // GenInertiaMatrix
        //--------------------------------------------------------------------
        public void GenInertiaMatrix(double[,] R, double[] IgP, 
            double[,] IgMat)
        {
            int i,j,k;

            for(i=0;i<3;++i)
            {
                for(j=i;j<3;++j)
                {
                    IgMat[i,j] = 0.0;
                    for(k=0;k<3;++k)
                        IgMat[i,j] += IgP[k]*R[i,k]*R[j,k];
                    if(i != j)
                        IgMat[j,i] = IgMat[i,j];
                }
            }
        }

        //--------------------------------------------------------------------
        // PrintMat
        //--------------------------------------------------------------------
        public void PrintMat(double[,] M)
        {
            int i,j;

            for(i=0;i<3;++i){
                for(j=0;j<3;++j)
                    Console.Write(M[i,j].ToString() + "   ");
                Console.Write('\n');
            }
        }

        //--------------------------------------------------------------------
        // getters/setters
        //--------------------------------------------------------------------
        public double q0
        {
            get{ return _q0; }
        }

        public double q1
        {
            get{ return _q1; }
        }

        public double q2
        {
            get{ return _q2; }
        }

        public double q3
        {
            get{ return _q3; }
        }
    }
}