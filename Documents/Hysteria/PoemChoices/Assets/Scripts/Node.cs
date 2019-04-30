using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Graphs
{
    public class Node<T>
    {

        #region Fields

        T value;
        List<Node<T>> neighbors;
        List<Edge<T>> edges;

        public Vector3 vPosition;

        public Node<T> ParentNode;

        #endregion

        #region Constructors 

        public Node(T value, Vector3 a_vPos)
        {
            this.value = value;
            vPosition = a_vPos;
            neighbors = new List<Node<T>>();
            edges = new List<Edge<T>>();
        }

        #endregion

        #region Properties

        public T Value
        {
            get { return value; }
        }

        public List<Node<T>> Neighbors
        {
            get { return neighbors; }
        }

        public IList<Edge<T>> Edges
        {
            get { return edges.AsReadOnly(); }
        }

        #endregion

        #region Methods

        public bool AddNeighbor(Node<T> neighbor, int weight){
            if(neighbors.Contains(neighbor)){
                return false;
            }else{
                neighbors.Add(neighbor);
                Edge<T> edge = new Edge<T>(this,neighbor,weight);
                edges.Add(edge);
                return true;
            }
        }

        public bool RemoveNeighbor(Node<T> neighbor){
            return neighbors.Remove(neighbor);
        }

        public bool RemoveAllNeighbors(){
            for (int i = neighbors.Count - 1; i >= 0; i--){
                neighbors.RemoveAt(i);
            }
            return true;
        }

		#endregion

	}

    public class Edge<T>
    {
        int weight;
        Node<T> A;
        Node<T> B;

        public Edge(Node<T> a, Node<T> b, int weight)
        {
            A = a;
            B = b;
            this.weight = weight;
        }

        public int getWeight()
        {
            return weight;
        }

        public Node<T> GetNode(int nodeNum){
            if(nodeNum == 0){
                return A;
            }else{
                return B;
            }
        }
    }
}