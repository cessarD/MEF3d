

using System;

namespace Poliglot

{
    enum indicators { NOTHING };
    enum lines { NOLINE, SINGLELINE, DOUBLELINE };
    enum modes { NOMODE, INT_FLOAT, INT_FLOAT_FLOAT_FLOAT, INT_INT_INT_INT_INT };
    enum Parameters { THERMAL_CONDUCTIVITY, HEAT_SOURCE };
    enum Sizes { NODES, ELEMENTS, DIRICHLET, NEUMANN };
    public unsafe class item
    {

        protected int id;
        protected float x;
        protected float y;
        protected float z;
        protected int node1;
        protected int node2;
        protected int node3;
        protected int node4;
        protected float value;

        public void setId(int identifier)
        {
            id = identifier;
        }

        public void setX(float x_coord)
        {
            x = x_coord;
        }

        public void setY(float y_coord)
        {
            y = y_coord;
        }

        public void setZ(float z_coord)
        {
            z = z_coord;
        }

        public void setNode1(int node_1)
        {
            node1 = node_1;
        }

        public void setNode2(int node_2)
        {
            node2 = node_2;
        }

        public void setNode3(int node_3)
        {
            node3 = node_3;
        }

        public void setNode4(int node_4)
        {
            node4 = node_4;
        }

        public void setValue(float value_to_assign)
        {
            value = value_to_assign;
        }

        public int getId()
        {
            return id;
        }

        public float getX()
        {
            return x;
        }

        public float getY()
        {
            return y;
        }

        public float getZ()
        {
            return z;
        }

        public int getNode1()
        {
            return node1;
        }

        public int getNode2()
        {
            return node2;
        }

        public int getNode3()
        {
            return node3;
        }

        public int getNode4()
        {
            return node4;
        }

        public float getValue()
        {
            return value;
        }



    };

    public unsafe class node : item
    {

        public void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i)
        {
            id = a;
            x = b;
            y = c;
            z = d;
        }

    };

    public unsafe class element : item
    {

        public void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i)
        {
            id = a;
            node1 = e;
            node2 = f;
            node3 = g;
            node4 = h;
        }

    };

    public unsafe class condition : item
    {

        public void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i)
        {
            node1 = e;
            value = i;
        }

    };

    unsafe public class mesh
    {
        float[] parameters = new float[2];
        int[] sizes = new int[4];

        unsafe node[] node_list;
        unsafe element[] element_list;
        unsafe int[] indices_dirich;
        unsafe condition[] dirichlet_list;
        unsafe condition[] neumann_list;



        public void setParameters(float k, float Q)
        {
            parameters[0] = k;
            parameters[1] = Q;

        }
        public void setSizes(int nnodes, int neltos, int ndirich, int nneu)
        {
            sizes[0] = nnodes;
            sizes[1] = neltos;
            sizes[2] = ndirich;
            sizes[3] = nneu;

        }
        public int getSize(int s)
        {
            return sizes[s];
        }
        public float getParameter(int p)
        {
            return parameters[p];
        }
        public unsafe void createData()
        {
            node_list = new node[sizes[0]];
            element_list = new element[sizes[1]];
            indices_dirich = new int[2];
            dirichlet_list = new condition[sizes[2]];
            neumann_list = new condition[sizes[3]];
        }
        public unsafe node[] getNodes()
        {
            return node_list;
        }
        public unsafe element[] getElements()
        {
            return element_list;
        }
        public unsafe int[] getDirichletIndices()
        {
            return indices_dirich;
        }
        public unsafe condition[] getDirichlet()
        {
            return dirichlet_list;
        }
        public unsafe condition[] getNeumann()
        {
            return neumann_list;
        }
        public unsafe node getNode(int i)
        {
            return node_list[i];
        }
        public unsafe element getElement(int i)
        {
           
            return element_list[i];
        }
        public  void setElements(element[] i)
        {
            element_list = i;

        }
        public unsafe condition getCondition(int i, int type)
        {
            if (type == 2) return dirichlet_list[i];
            else return neumann_list[i];
        }
    };



}