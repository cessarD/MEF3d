using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Poliglot
{
   
    public class sel
    {

        public void crearSistemasLocales(ref mesh m,ref Matrix<float>[] localKs,ref Vector<float>[] localbs)
        {
           
             for(int i=0;i<m.getSize(1)-1;i++)
             {
               
                   localKs[i]=createLocalK(i,ref m);
                  localbs[i]=createLocalb(i,ref m);
             }
        }
        float calculateLocalD(int ind,mesh m){
            float D,a,b,c,d,e,f,g,h,i;

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
            V = (a*e*i+d*h*c+g*b*f-g*e*c-a*h*f-d*b*i)/6;
       
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

        void calculateU(int i,mesh m,int j, ref Matrix<float> U)
        {
            element e = m.getElement(i);
            node n1 = m.getNode(e.getNode1()-1);
            node n2 = m.getNode(e.getNode2()-1);
            node n3 = m.getNode(e.getNode3()-1);
            node n4 = m.getNode(e.getNode4()-1);

            
        }

        float calculatec1(element n, mesh m)
        {
            node n1 = m.getNode(n.getNode1() - 1);
            node n2 = m.getNode(n.getNode2() - 1);
            node n3 = m.getNode(n.getNode3() - 1);
            node n4 = m.getNode(n.getNode4() - 1);

            return (float)Math.Pow(n2.getX() - n1.getX(), 2); ;
        }
        float calculatec2(element n, mesh m)
        {
            node n1 = m.getNode(n.getNode1() - 1);
            node n2 = m.getNode(n.getNode2() - 1);
            node n3 = m.getNode(n.getNode3() - 1);
            node n4 = m.getNode(n.getNode4() - 1);
            //seria X8 pero no se de donde sale
            return (1 / n2.getX() - n1.getX()) * ((4 * n1.getX()) + (4 * n2.getX()) - (8 * n3.getX()));
        }
        Matrix<float> createLocalK(int element,ref mesh m){
            // K = (k*Ve/D^2)Bt*At*A*B := K_4x4
            math_tools mtools=new math_tools();
               float D,Ve,k = m.getParameter(0);
              Matrix<float> K = null,Bt = null,At = null;
             var A=Matrix<float>.Build.Dense(3,3,0);
              var B=Matrix<float>.Build.Dense(3,4,0);
            Matrix<float> u = null;

            //new matrix<matrix data>
           
             D = calculateLocalD(element,m);
              Ve = calculateLocalVolume(element,m);
             
            
              calculateLocalA(element,ref A,m);
              calculateB(ref B);
              At = A.Transpose();
              Bt = B.Transpose();
          
          
  
             mtools.productRealMatrix(k*Ve/(D*D),mtools.productMatrixMatrix(Bt,mtools.productMatrixMatrix(At,mtools.productMatrixMatrix(A,B,3,3,4),3,3,4),4,3,4),ref K);
      
        
         
            return K;
        }
        public Vector<float> createLocalb(int element,ref mesh m){
            Vector<float> b = Vector<float>.Build.Dense(4,0);;

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
        
        public void ensamblaje(ref mesh m, ref Matrix<float>[] localKs, ref Vector<float>[] localbs,ref Matrix<float> K,ref Vector<float> b, ref Matrix<float> U){
            for(int i=0;i<m.getSize(1)-1;i++){
                element e = m.getElement(i);
                assemblyK(e,  localKs[i],ref K, ref U);
                assemblyb(e, localbs[i],ref b);
            }
     
        }
        void assemblyK(element e, Matrix<float> localK,ref Matrix<float> K, ref Matrix<float> U)
        {
            int index1 = e.getNode1() - 1;
            int index2 = e.getNode2() - 1;
            int index3 = e.getNode3() - 1;
            int index4 = e.getNode4() - 1;
    
            K[index1,index1] += localK[0,0];
            K[index1,index2] += localK[0,1];
            K[index1,index3] += localK[0,2];
            K[index1,index4] += localK[0,3];
           
            K[index2,index1] += localK[1,0];
            K[index2,index2] += localK[1,1];
            K[index2,index3] += localK[1,2];
            K[index2,index4] += localK[1,3];
            
            K[index3,index1] += localK[2,0];
            K[index3,index2] += localK[2,1];
            K[index3,index3] += localK[2,2];
            K[index3,index4] += localK[2,3];
    
            K[index4,index1] += localK[3,0];
            K[index4,index2] += localK[3,1];
            K[index4,index3] += localK[3,2];
            K[index4,index4] += localK[3,3];

            //mult
            Matrix<float> mult = Matrix<float>.Build.Dense(3, 3, 0);
            Console.WriteLine("el mult");
            mult[0, 0] = U.Determinant();
            mult[1, 1] = U.Determinant();
            mult[2, 2] = U.Determinant();


            Console.WriteLine(mult);

            for (int j = 0; j < U.RowCount; j++)
            {
                for (int l = 0; l <U.ColumnCount; l++)
                {

                    Console.Write(U[j, l] + " ");




                }

                Console.WriteLine();




            }

        }

        void assemblyb(element e,Vector<float> localb,ref Vector<float> b){
            int index1 = e.getNode1() - 1;
            int index2 = e.getNode2() - 1;
            int index3 = e.getNode3() - 1;
            int index4 = e.getNode4() - 1;

            b[index1] += localb[0];
            b[index2] += localb[1];
            b[index3] += localb[2];
            b[index4] += localb[3];
 
        }
        public void applyNeumann(ref mesh m,ref Vector<float> b){
            for(int i=0;i<m.getSize(3);i++){
                condition c = m.getCondition(i,3);
              
                b[c.getNode1()-1] += c.getValue();
            }
        }
        public void applyDirichlet(ref mesh m,ref Matrix<float> K,ref Vector<float> b){
            for(int i=0;i<m.getSize(2);i++){
                condition c = m.getCondition(i,2);
                int index = c.getNode1()-1;
                K.RemoveRow(index);
              // K=removerFila(K,index);
              b=removerelemento(m,b,index);
             

                for(int row=0;row<K.RowCount;row++){
                    float cell = K[row,index];
             
                    b[row] += -1*c.getValue()*cell;
                }
                K.RemoveColumn(index);
             //   K=removerColumna(K,index);
            }
        }
        Vector<float> removerelemento(mesh m,Vector<float> b,int index){
            Vector<float> a = Vector<float>.Build.Dense(b.Count,0);

            for(int i = 0; i<b.Count; i++){

                if(i!=index){
                    
                    a.Add(b.At(i));
                }
            }

            return a;
        }

        Matrix<float> removerColumna(Matrix<float> matriz, int columna) {
            Matrix<float> nueva = null;
            if (columna < 0 || columna >= matriz.ColumnCount) {
                return matriz;
            } else {
                for (int l =0; l < nueva.RowCount; l++) {

                    int b=0;

                    for (int j = 0; j < nueva.ColumnCount; j++) {
                        if(columna==b){
                            b++;
                        }

                        nueva[l,j]=matriz[l,b];
                        b++;



                    }


                }

            }
            return nueva;
        }
        Matrix<float> removerFila(Matrix<float> matriz, int fila) {
            if (fila < 0 || fila >= matriz.RowCount) {
                return matriz;
            } else
            {
                Matrix<float> nueva = null;

                int b=0;
                for (int l =0; l < matriz.RowCount-1; l++) {

                    if((fila)==b){
                        b++;
                    }


                    for (int j = 0; j < matriz.ColumnCount; j++) {


                        nueva[l,j]=matriz[b,j];


                    }
                    b++;

                }

                return nueva;
            }
        }
        
        
        public void calculate(ref Matrix<float> K, ref Vector<float> b, ref Vector<float> T)
        {
            math_tools mtools= new math_tools();
            Console.WriteLine("Iniciando calculo de respuesta...");
           
            Matrix<float> Kinv = Matrix<float>.Build.Dense(16,16,0);
            Console.WriteLine("Calculo de inversa...");
            mtools.transpose(K,ref Kinv);
          //  Kinv = K.Inverse();
            Console.WriteLine("Calculo de respuesta...");
            Console.WriteLine("K original");
            for (int j = 0; j < K.RowCount; j++)
            {        for (int l = 0; l < K.ColumnCount; l++)
                {
                    
                    Console.Write(K[j,l] + " ");
                

              

                } 
                    
                Console.WriteLine();
                

              

            } 
            Console.WriteLine("Inversa");
            for (int j = 0; j < Kinv.RowCount; j++)
            {        for (int l = 0; l < Kinv.ColumnCount; l++)
                {
                    
                    Console.Write(Kinv[j,l] + " ");
                

              

                } 
                    
                Console.WriteLine();
                

              

            } 
            mtools.productMatrixVector(Kinv,b,ref T);
            Console.WriteLine("Respuesta");
            for (int j = 0; j < T.Count; j++) {


                Console.WriteLine(T.At(j));

            }
         
            
        }

    }
}