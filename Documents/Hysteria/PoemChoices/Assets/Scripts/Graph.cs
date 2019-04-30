using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Graphs{

    public class Graph<T>
    {
        #region Fields

        List<Node<T>> nodes = new List<Node<T>>();

        #endregion

        #region Constructor

        public Graph()
        {

        }
        #endregion

        #region Properties

        public int Count
        {
            get { return nodes.Count; }
        }

        public int numEdges(T value)
        {
            Node<T> node = Find(value);
            return node.Neighbors.Count;
        }

        public IList<Node<T>> Nodes
        {
            get { return nodes.AsReadOnly(); }
        }

        #endregion

        #region Methods

        public void Clear(){

            foreach (Node<T> node in nodes){
                node.RemoveAllNeighbors();
            }

            for (int i = nodes.Count - 1; i >= 0; i--){
                nodes.RemoveAt(i);
            }
        }

        public bool AddNode(T value, Vector3 pos){
            if(Find(value) != null){
                return false;
            }else{
                nodes.Add(new Node<T>(value, pos));
                return true;
            }
        }

        public bool AddEdge(T value1, T value2, int weight){
            Node<T> node1 = Find(value1);
            Node<T> node2 = Find(value2);

            if(node1 == null || node2 == null){
                return false;
            }else if(node1.Neighbors.Contains(node2)){
                return false;
            }else{
                node1.AddNeighbor(node2, weight);
                node2.AddNeighbor(node1,weight);
                return true;
            }
        }

        public bool RemoveNode(T value){
            Node<T> removeNode = Find(value);
            if(removeNode == null){
                return false;
            }else{
                nodes.Remove(removeNode);
                foreach(Node<T> node in nodes){
                    node.RemoveNeighbor(removeNode);
                }
                return true;
            }
        }

        public bool RemoveEdge(T value1, T value2){
            Node<T> node1 = Find(value1);
            Node<T> node2 = Find(value2);
            if(node1 == null || node2 == null) {
                return false;
            }else if(!node1.Neighbors.Contains(node2)){
                return false;
            }else{
                node1.RemoveNeighbor(node2);
                node2.RemoveNeighbor(node1);
                return true;
            }
        }

        public Node<T> Find(T value){
            foreach(Node<T> node in nodes){
                if(node.Value.Equals(value)){
                    return node;
                }
            }
            return null;
        }

        #endregion
    }
    
}
