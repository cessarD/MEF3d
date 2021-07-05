﻿
using MathNet.Numerics.LinearAlgebra;

using System;
using System.IO;




namespace Poliglot
{
    internal class Program
    {
        private const string filename = @"C:\Users\Rene\Documents\GitHub\MEF3d\Poliglot\3dtest.dat";
        //private const string filename = @"C:\Users\cesar\RiderProjects\MEF3d\Poliglot\3dtest.dat";
        public static void Main(string[] args)
        {
            tools tls = new tools();
            math_tools mtools= new math_tools();
            sel sel = new sel();
            //definiendo nuestros vectores y variables. 

            var Localks = new Matrix<float>[16];
            var LocalBs = new Vector<float>[16] ;
     

            mesh m = new mesh();

            //leer malla y condiciones.
           

       
            tls.ReadMeshandConditions(ref m, filename);
            Console.WriteLine("m.getsize "+m.getSize(0));
            sel.crearSistemasLocales(ref m, ref Localks, ref LocalBs);
            //finales
            //creando u
            Matrix<float> U = Matrix<float>.Build.Dense(10, 10, 0);
            U = sel.calculateU(1, m,0, ref U);
            Console.WriteLine("Post U");
            for (int j = 0; j < U.RowCount; j++)
            {
                for (int l = 0; l < U.ColumnCount; l++)
                {   

                    Console.Write(U[j, l] + " ");




                }

                Console.WriteLine();




            }








            Matrix<float> K = Matrix<float>.Build.Dense(30,30,0);
            Vector<float> B= Vector<float>.Build.Dense(16,0);
           // Vector<float> T = null;
            //mtools.zeroesm(ref K,m.getSize(0));
            //mtools.zeroesv(ref B,m.getSize(0));
            sel.ensamblaje(ref m,ref Localks,ref LocalBs,ref K,ref B, ref U);
            Console.WriteLine("POST K");
            for (int j = 0; j < K.RowCount; j++)
            {        for (int l = 0; l < K.ColumnCount; l++)
                {
                    
                    Console.Write(K[j,l] + " ");
                

              

                } 
                    
                    Console.WriteLine();
                

              

            } 

           sel.applyNeumann(ref m,ref B);
              sel.applyDirichlet(ref m,ref K,ref B);
              Console.WriteLine("POST Condition");
              for (int j = 0; j < K.RowCount; j++)
              {        for (int l = 0; l < K.ColumnCount; l++)
                  {
                    
                      Console.Write(K[j,l] + " ");
                

              

                  } 
                    
                  Console.WriteLine();
                

              

              } 
              Vector<float> T= Vector<float>.Build.Dense(B.Count,0);
              //mtools.zeroesv(ref T,B.Count);
             
              sel.calculate(ref K,ref B,ref T);

            
            
            Console.ReadLine();
            








        }



    }

}