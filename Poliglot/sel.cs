using System;
using MathNet.Numerics.LinearAlgebra;

namespace Poliglot
{
   
    public class sel
    {

        public void crearSistemasLocales(ref mesh m,ref Matrix<float>[] localKs,ref Vector<float>[] localbs)
        {
            Console.WriteLine("elementos");
            element[] e = m.getElements();
            Console.WriteLine(e[0].getNode1());
             for(int i=0;i<m.getSize(1);i++)
             {
               
                   // localKs[i]=createLocalK(i,ref m);
                 // localbs[i]=createLocalb(i,ref m);
             }
        }
        float calculateLocalD(int ind,mesh m){
            float D,a,b,c,d,e,f,g,h,i;
            Console.WriteLine(ind);
            element el = m.getElement(ind+1);

            node n1 = m.getNode(el.getNode1()-1);
            node n2 = m.getNode(el.getNode2()-1);
            node n3 = m.getNode(el.getNode3()-1);
            node n4 = m.getNode(el.getNode4()-1);

            a=n2.getX()-n1.getX();b=n2.getY()-n1.getY();c=n2.getZ()-n1.getZ();
            d=n3.getX()-n1.getX();e=n3.getY()-n1.getY();f=n3.getZ()-n1.getZ();
            g=n4.getX()-n1.getX();h=n4.getY()-n1.getY();i=n4.getZ()-n1.getZ();
            //Se calcula el determinante de esta matriz utilizando
            //la Regla de Sarrus.
            D = a*e*i+d*h*c+g*b*f-g*e*c-a*h*f-d*b*i;

            return D;
        }
        float calculateLocalVolume(int ind,mesh m){
            //Se utiliza la siguiente fórmula:
            //      Dados los 4 puntos vértices del tetrahedro A, B, C, D.
            //      Nos anclamos en A y calculamos los 3 vectores:
            //              V1 = B - A
            //              V2 = C - A
            //              V3 = D - A
            //      Luego el volumen es:
            //              V = (1/6)*det(  [ V1' ; V2' ; V3' ]  )
    
            float V,a,b,c,d,e,f,g,h,i;
            element el = m.getElement(ind);
            node n1 = m.getNode(el.getNode1()-1);
            node n2 = m.getNode(el.getNode2()-1);
            node n3 = m.getNode(el.getNode3()-1);
            node n4 = m.getNode(el.getNode4()-1);

            a = n2.getX()-n1.getX();b = n2.getY()-n1.getY();c = n2.getZ()-n1.getZ();
            d = n3.getX()-n1.getX();e = n3.getY()-n1.getY();f = n3.getZ()-n1.getZ();
            g = n4.getX()-n1.getX();h = n4.getY()-n1.getY();i = n4.getZ()-n1.getZ();
            //Para el determinante se usa la Regla de Sarrus.
            V = (1/6)*(a*e*i+d*h*c+g*b*f-g*e*c-a*h*f-d*b*i);

            return V;
        }
        float ab_ij(float ai, float aj, float a1, float bi, float bj, float b1){
            return (ai - a1)*(bj - b1) - (aj - a1)*(bi - b1);
        }
        void calculateLocalA(int i,ref Matrix<float> A,mesh m){
            element e = m.getElement(i);
            node n1 = m.getNode(e.getNode1()-1);
            node n2 = m.getNode(e.getNode2()-1);
            node n3 = m.getNode(e.getNode3()-1);
            node n4 = m.getNode(e.getNode4()-1);

            A[0,0] = ab_ij(n3.getY(),n4.getY(),n1.getY(),n3.getZ(),n4.getZ(),n1.getZ());
            A[0,1] = ab_ij(n4.getY(),n2.getY(),n1.getY(),n4.getZ(),n2.getZ(),n1.getZ());
            A[0,2] = ab_ij(n2.getY(),n3.getY(),n1.getY(),n2.getZ(),n3.getZ(),n1.getZ());
            A[1,0] = ab_ij(n4.getX(),n3.getX(),n1.getX(),n4.getZ(),n3.getZ(),n1.getZ());
            A[1,1] = ab_ij(n2.getX(),n4.getX(),n1.getX(),n2.getZ(),n4.getZ(),n1.getZ());
            A[1,2] = ab_ij(n3.getX(),n2.getX(),n1.getX(),n3.getZ(),n2.getZ(),n1.getZ());
            A[2,0] = ab_ij(n3.getX(),n4.getX(),n1.getX(),n3.getY(),n4.getY(),n1.getY());
            A[2,1] = ab_ij(n4.getX(),n2.getX(),n1.getX(),n4.getY(),n2.getY(),n1.getY());
            A[2,2] = ab_ij(n2.getX(),n3.getX(),n1.getX(),n2.getY(),n3.getY(),n1.getY());
        }
        void calculateB(ref Matrix<float> B){
            B[0,0] = -1; B[0,1] = 1; B[0,2] = 0; B[0,3] = 0;
            B[1,0] = -1; B[1,1] = 0; B[1,2] = 1; B[1,3] = 0;
            B[2,0] = -1; B[2,1] = 0; B[2,2] = 0; B[2,3] = 1;
        }
        Matrix<float> createLocalK(int element,ref mesh m){
            // K = (k*Ve/D^2)Bt*At*A*B := K_4x4
            math_tools mtools=new math_tools();
            float D,Ve,k = m.getParameter(0);
            Matrix<float> K = null,A = null,B = null,Bt = null,At = null;

            D = calculateLocalD(element,m);
            Ve = calculateLocalVolume(element,m);

            mtools.zeroes(ref A,3);
            mtools.zeroes(ref B,3,4);
            calculateLocalA(element,ref A,m);
            calculateB(ref B);
            mtools.transpose(A,ref At);
            mtools.transpose(B,ref Bt);

            mtools.productRealMatrix(k*Ve/(D*D),mtools.productMatrixMatrix(Bt,mtools.productMatrixMatrix(At,mtools.productMatrixMatrix(A,B,3,3,4),3,3,4),4,3,4),ref K);

            return K;
        }
        public Vector<float> createLocalb(int element,ref mesh m){
            Vector<float> b = null;

            float Q = m.getParameter(1),J,b_i;
            J = calculateLocalJ(element,ref m);

            b_i = Q*J/24;
            b[0] = b_i;
            b[1] = b_i;
            b[2] = b_i;
            b[3] = b_i;

            return b;
        }
        public float calculateLocalJ(int ind, ref mesh m){
            float J,a,b,c,d,e,f,g,h,i;

            element el = m.getElement(ind);

            node n1 = m.getNode(el.getNode1()-1);
            node n2 = m.getNode(el.getNode2()-1);
            node n3 = m.getNode(el.getNode3()-1);
            node n4 = m.getNode(el.getNode4()-1);

            a=n2.getX()-n1.getX();b=n3.getX()-n1.getX();c=n4.getX()-n1.getX();
            d=n2.getY()-n1.getY();e=n3.getY()-n1.getY();f=n4.getY()-n1.getY();
            g=n2.getZ()-n1.getZ();h=n3.getZ()-n1.getZ();i=n4.getZ()-n1.getZ();
            //Se calcula el determinante de esta matriz utilizando
            //la Regla de Sarrus.
            J = a*e*i+d*h*c+g*b*f-g*e*c-a*h*f-d*b*i;

            return J;
        }
        
    }
}