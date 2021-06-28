using MathNet.Numerics.LinearAlgebra;
using System;
using System.IO;




namespace Poliglot
{
    internal class Program
    {
        //private const string filename = @"C:\Users\Rene\Documents\GitHub\MEF3d\Poliglot\3dtest.dat";
        private const string filename = @"C:\Users\cesar\RiderProjects\Last\Poliglot\Poliglot\3dtest.dat";
        public static void Main(string[] args)
        {
            tools tls = new tools();
            math_tools mtools= new math_tools();
            sel sel = new sel();
            //definiendo nuestros vectores y variables. 

            Matrix<float>[] Localks = new Matrix<float>[] { };
            Vector<float>[] LocalBs = new Vector<float>[] { };
            Matrix<float> K = null;
            Vector<float> B= null;
            Vector<float> T = null;

            mesh m = new mesh();

            //leer malla y condiciones.
           

            

            tls.ReadMeshandConditions(ref m, filename);

            sel.crearSistemasLocales(ref m, ref Localks, ref LocalBs);
            
            mtools.zeroesm(ref K,m.getSize(0));
            mtools.zeroesv(ref B,m.getSize(0));
            sel.ensamblaje(ref m,ref Localks,ref LocalBs,ref K,ref B);
             
              sel.applyNeumann(ref m,ref B);
              sel.applyDirichlet(ref m,ref K,ref B);
            
           
              mtools.zeroesv(ref T,B.Count);
              sel.calculate(ref K,ref B,ref T);
            
            Console.ReadLine();
            








        }



    }

}