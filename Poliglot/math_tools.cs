﻿using MathNet.Numerics.LinearAlgebra;

namespace Poliglot
{
    public class math_tools
    {

        public void zeroes(ref Matrix<float> M,int n){
            for(int i=0;i<n;i++){
                for(int j=0;j<n;j++){

                    M[i,j]=0;
                }

            }
            
       
        }

        public void zeroes(ref Matrix<float> M,int n,int m){
            for(int i=0;i<n;i++){
                for(int j=0;j<m;j++){

                    M[i,j]=0;
                }

            }
        }

        public void zeroes(ref Vector<float> v,int n){
            for(int i=0;i<n;i++){
                v[i]=0;
            }
        }
        public void transpose(Matrix<float> M, ref Matrix<float> T){
            
            zeroes(ref T,M.ColumnCount,M.RowCount);
            for(int i=0;i<M.RowCount;i++)
            for(int j=0;j<M.ColumnCount;j++)
                T[j,i] = M[i,j];
        }
        float calculateMember(int i,int j,int r,Matrix<float> A,Matrix<float> B){
            float member = 0;
            for(int k=0;k<r;k++)
                member += A[i,k]*B[k,j];
            return member;
        }
        public Matrix<float> productMatrixMatrix(Matrix<float> A,Matrix<float> B,int n,int r,int m){
            Matrix<float> R = null;

            zeroes(ref R,n,m);
            for(int i=0;i<n;i++)
            for(int j=0;j<m;j++)
                R[i,j] = calculateMember(i,j,r,A,B);

            return R;
        }
        public void productRealMatrix(float real,Matrix<float> M,ref Matrix<float> R){
            zeroes(ref R,M.RowCount);
            for(int i=0;i<M.RowCount;i++)
            for(int j=0;j<M.ColumnCount;j++)
                R[i,j] = real*M[i,j];
        }
    }
}