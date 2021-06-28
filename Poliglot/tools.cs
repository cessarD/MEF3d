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
        
        
        public void ReadMeshandConditions( ref mesh m, string filename)
        {
            float k, q;
            int nnodes, neltos, ndirich, nneuman;

            if (!File.Exists(filename))
            {
                Console.WriteLine("El archivo no existe!");
                return ;
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
            
         
            Console.WriteLine("PRE NODE");
      
            
            //seting nodes
            node[] n = m.getNodes();
            for (int i = 0; i < nnodes; i++) {
                line = reader.ReadLine();
                
                line = line.Replace("           ","");
                line = line.Replace("          ","");
                line = line.Replace("         ","");
                line = line.Replace("        ","");
                line = line.Replace("       "," ");
               
                lines = line.Split(' ');
    
                node nnode = new node();
                n[i] = nnode;
                n[i].setValues(int.Parse(lines[0].ToString()), float.Parse(lines[1].ToString()),float.Parse(lines[2].ToString()),float.Parse(lines[3].ToString()),0,0,0,0,0);

            }
            
            line = reader.ReadLine();
            line = reader.ReadLine();
            line = reader.ReadLine();
            Console.WriteLine("PRE Element");
            //seting elements
            element[] e = m.getElements();
            for (int i = 0; i < neltos; i++) {
                line = reader.ReadLine();
                
         
               
                lines = line.Split(' ');
                
                element element = new element();
                e[i] = element;
                e[i].setValues(int.Parse(lines[0].ToString()) ,0,0,0,int.Parse(lines[1].ToString()),int.Parse(lines[2].ToString()),int.Parse(lines[3].ToString()),int.Parse(lines[4].ToString()),0);

            }
            
        
            line = reader.ReadLine();
            line = reader.ReadLine();
            line = reader.ReadLine();
            Console.WriteLine("PRE DIR");
            //seting elements
            condition[] DIR = m.getDirichlet();
            for (int i = 0; i < ndirich; i++) {
                line = reader.ReadLine();

                line = line.Replace("          ", "");
                line = line.Replace("         ","");
                line = line.Replace("        ","");
                line = line.Replace("       "," ");

             
                lines = line.Split(' ');
    
                condition element = new condition();
                DIR[i] = element;
                DIR[i].setValues(int.Parse(lines[0].ToString()) ,0,0,0,0,0,0,0,int.Parse(lines[1].ToString()));

            }
            line = reader.ReadLine();
            line = reader.ReadLine();
            line = reader.ReadLine();
            Console.WriteLine("PRE NEU");
            //seting elements
            condition[] NEU = m.getNeumann();
            for (int i = 0; i < nneuman; i++) {
                line = reader.ReadLine();
                line = line.Replace("           ", "");
                line = line.Replace("          ", "");
                line = line.Replace("         ","");
                line = line.Replace("        ","");
                line = line.Replace("       "," ");

         
                lines = line.Split(' ');
    
                condition element = new condition();
                NEU[i] = element;
                NEU[i].setValues(int.Parse(lines[0].ToString()) ,0,0,0,0,0,0,0,int.Parse(lines[1].ToString()));

            }
            
            /*seting values
  
            line = reader.ReadLine();
            Console.WriteLine(line);
            //hace split cada 12 espacios, a pesar de usar \t
            lines = line.Split(' ');
            Console.WriteLine(lines[12]);*/










            //CorrectConditions(ndirich,  m.getDirichlet(), m.getDirichletIndices());

        }
    }
}