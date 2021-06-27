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
            sel sel = new sel();
            //definiendo nuestros vectores y variables. 

            Matrix<float>[] Localks = new Matrix<float>[] { };
            Vector<float>[] LocalBs = new Vector<float>[] { };
            Matrix<float> K;
            Vector<float> B;
            Vector<float> T;

            mesh m = new mesh();

            //leer malla y condiciones.
           

            

            tls.ReadMeshandConditions(ref m, filename);
            Console.WriteLine("elementos main");
            element[] e = m.getElements();
            Console.WriteLine(e.Length);
            Console.WriteLine(e[1].getNode1());
            Console.WriteLine(e[2].getNode1());
            Console.WriteLine(e[3].getNode1());
            Console.WriteLine(e[4].getNode1());
            Console.WriteLine(e[5].getNode1());
            Console.WriteLine(e[6].getNode1());
            //sel.crearSistemasLocales(ref m, ref Localks, ref LocalBs);
            Console.ReadLine();









        }



    }

}