using MathNet.Numerics.LinearAlgebra;
using System;
using System.IO;




namespace Poliglot
{
    internal class Program
    {
        private const string filename = @"C:\Users\Rene\Documents\GitHub\MEF3d\Poliglot\3dtest.dat";
        //private const string filename=  @"delao"
        public static void Main(string[] args)
        {

            //definiendo nuestros vectores y variables. 

            /*Matrix<float>[] Localks;
            Vector<float>[] LocalBs;
            Matrix<float> K;
            Vector<float> B;
            Vector<float> T;*/

            mesh m = new mesh();

            //leer malla y condiciones.
            tools tls = new tools();

            

            tls.ReadMeshandConditions(ref m, filename);

            Console.ReadLine();









        }



    }

}