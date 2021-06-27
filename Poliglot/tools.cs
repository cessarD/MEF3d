using System;
using System.IO;




namespace Poliglot
{
    public class tools
    {
        public void CorrectConditions(int n, condition[] list, int[] indices) {
            for (int i=0;i<n; i++) {
                indices[i] = list[i].getNode1();
            }
            for (int i = 0; i < n; i++) {
                int pivot = list[i].getNode1();
                for (int j = i; j < n; j++) {
                    if (list[j].getNode1() > pivot) {
                        list[j].setNode1(list[j].getNode1() - 1);
                    }
                }
            }
        
        }
        public void ReadMeshandConditions(ref mesh m, string filename)
        {
            float k, q;
            int nnodes, neltos, ndirich, nneuman;

            if (!File.Exists(filename))
            {
                Console.WriteLine("El archivo no existe!");
                return;
            }

            FileStream data = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader reader = new StreamReader(data);

            string[] lines;
            string line;

            //reading a line;
            line = reader.ReadLine();
            lines = line.Split(' ');

            
            k = float.Parse(lines[0]);
            q = float.Parse(lines[1]);
            Console.WriteLine("k=" + k + "q=" + q);

            //salto
            line = reader.ReadLine();
            lines = line.Split(' ');
            //declarando variables
            nnodes = int.Parse(lines[0]);
            neltos = int.Parse(lines[1]);
            ndirich = int.Parse(lines[2]);
            nneuman = int.Parse(lines[3]);
            Console.WriteLine("nodos = " + nnodes + "\nElementos = " + neltos + "\nCondiciones de Dirichlet = " + ndirich + "\nCondiciones de Nneuman = " + nneuman + "\n\n");

            m.setParameters(k, q);
            m.setSizes(nnodes,neltos,ndirich, nneuman);
            m.createData();


            //salto doble
            line = reader.ReadLine();
           
            line = reader.ReadLine();
            

            
            //seting nodes
            node[] n = m.getNodes();
            for (int i = 0; i < nnodes; i++) {
                node nnode = new node();
                n[i] = nnode;
                n[i].setValue((float)0.0);
            }
            //seting values
            line = reader.ReadLine();
            Console.WriteLine(line);
            //hace split cada 12 espacios, a pesar de usar \t
            lines = line.Split(' ');
            Console.WriteLine(lines[12]);










            //CorrectConditions(ndirich,  m.getDirichlet(), m.getDirichletIndices());


        }
    }
}