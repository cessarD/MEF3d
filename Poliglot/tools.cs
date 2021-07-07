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
            //declarando variables
            float ei,fx,fy,fz;
            int nnodes, neltos, ndirichx,ndirichy,ndirichz, nneuman;

            if (!File.Exists(filename))
            {
                Console.WriteLine("El archivo no existe!");
                return ;
            }

            FileStream data = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader reader = new StreamReader(data);
         
            string[] lines;
            string line;

        
            line = reader.ReadLine();
            lines = line.Split(' ');

            //Guardando parametros iniciales
            ei = float.Parse(lines[0]);
            fx = float.Parse(lines[1]);
            
            fy = float.Parse(lines[2]);
            fz = float.Parse(lines[3]);
          
            Console.WriteLine("ei=" + ei + "fx=" + fx);

            line = reader.ReadLine();
            lines = line.Split(' ');
            //Guardando cantidades
            nnodes = int.Parse(lines[0]);
            neltos = int.Parse(lines[1]);
            ndirichx = int.Parse(lines[2]);
            ndirichy = int.Parse(lines[3]);
            ndirichz = int.Parse(lines[4]);
            nneuman = int.Parse(lines[5]);
            Console.WriteLine("nodos = " + nnodes + "\nElementos = " + neltos + "\nCondiciones de Dirichlet = " + ndirichx + "\nCondiciones de Nneuman = " + nneuman + "\n\n");

            m.setParameters(ei, fx,fy,fz);
            m.setSizes(nnodes,neltos,ndirichx,ndirichy,ndirichz, nneuman);
            m.createData();


          
            line = reader.ReadLine();
           
            line = reader.ReadLine();
            
         
            Console.WriteLine("Guardando Nodos");
      
            
            //Guardando nodos en la malla
            node[] n = m.getNodes();
            for (int i = 0; i < nnodes; i++) {
                line = reader.ReadLine();
               
                while (line.Split(' ').Length > 4)
                {
                    line = line.Replace("  "," ");
                   
                }
          
                lines = line.Split(' ');
    
                node nnode = new node();
                n[i] = nnode;
                n[i].setValues(int.Parse(lines[0].ToString()), float.Parse(lines[1].ToString()),float.Parse(lines[2].ToString()),float.Parse(lines[3].ToString()),0,0,0,0,0);

            }
            
            line = reader.ReadLine();
            line = reader.ReadLine();
            line = reader.ReadLine();
            Console.WriteLine("Guardando elementos");
            //Guardando Elementos en la malla
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
            Console.WriteLine("Guardando Condiciones de Dirichlet en vector X");
            //Guardar en la malla las condiciones de dirichlet
            condition[] DIR = m.getDirichletx();
            for (int i = 0; i < ndirichx; i++) {
                line = reader.ReadLine();

                while (line.Split(' ').Length > 2)
                {
                    line = line.Replace("  "," ");
                   
                }

   
                lines = line.Split(' ');
    
                condition element = new condition();
                DIR[i] = element;
        
                DIR[i].setValues(0 ,0,0,0,int.Parse(lines[0].ToString()),0,0,0,int.Parse(lines[1].ToString()));

            }
            line = reader.ReadLine();
            line = reader.ReadLine();
            line = reader.ReadLine();
            Console.WriteLine("Guardando Condiciones de Dirichlet en vector Y");
            //Guardar en la malla las condiciones de dirichlet
            condition[] DIRy = m.getDirichlety();
            for (int i = 0; i < ndirichy; i++) {
                line = reader.ReadLine();

                while (line.Split(' ').Length > 2)
                {
                    line = line.Replace("  "," ");
                   
                }

             
                lines = line.Split(' ');
    
                condition element = new condition();
                DIRy[i] = element;
        
                DIRy[i].setValues(0 ,0,0,0,int.Parse(lines[0].ToString()),0,0,0,int.Parse(lines[1].ToString()));

            }
            
            line = reader.ReadLine();
            line = reader.ReadLine();
            line = reader.ReadLine();
            Console.WriteLine("Guardando Condiciones de Dirichlet en vector Z");
            //Guardar en la malla las condiciones de dirichlet
            condition[] DIRz = m.getDirichletz();
            for (int i = 0; i < ndirichz; i++) {
                line = reader.ReadLine();

                while (line.Split(' ').Length > 2)
                {
                    line = line.Replace("  "," ");
                   
                }

             
                lines = line.Split(' ');
    
                condition element = new condition();
                DIRz[i] = element;
        
                DIRz[i].setValues(0 ,0,0,0,int.Parse(lines[0].ToString()),0,0,0,int.Parse(lines[1].ToString()));

            }
            line = reader.ReadLine();
            line = reader.ReadLine();
            line = reader.ReadLine();
            Console.WriteLine("Guardando Condiciones de Neumann");
            //Guardar en la malla las condiciones de neumann
            condition[] NEU = m.getNeumann();
            for (int i = 0; i < nneuman; i++) {
                line = reader.ReadLine();
                while (line.Split(' ').Length > 2)
                {
                    line = line.Replace("  "," ");
                   
                }

         
                lines = line.Split(' ');
    
                condition element = new condition();
                NEU[i] = element;
                NEU[i].setValues(0,0,0,0,int.Parse(lines[0].ToString()) ,0,0,0,int.Parse(lines[1].ToString()));

            }
            
      

        }
    }
}