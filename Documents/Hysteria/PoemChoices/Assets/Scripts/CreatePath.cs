using System.Collections.Generic;
using UnityEngine;
using Graphs;

public class CreatePath : MonoBehaviour {

    LineRenderer line;

    Graph<GameObject> graph = new Graph<GameObject>();

    //Determines if graph is made by hand or automatically 
    public bool autoCreateGraph = true;

    public GameObject baseNode;

    public int nodeDistVertical;

    public List<GameObject> nodeList = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        //createStraightGraph();

        //Find every node in the scene and add it to the node list
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("point")){
            nodeList.Add(go);
        }

        if(autoCreateGraph){
            createInterGraph();
        }else{
            handMadeGraph();
        }
	}

	void OnDrawGizmos()
	{
        foreach(Node<GameObject> node in graph.Nodes){
            foreach(Node<GameObject> neighbor in node.Neighbors)
                Gizmos.DrawLine(node.Value.transform.position, neighbor.Value.transform.position);
        }
	}

    /// <summary>
    /// Creates a graph that is straight vertically
    /// </summary>
    void createStraightGraph(){

        //Add the very first node/ root node to the graph
        graph.AddNode(baseNode,baseNode.transform.position);

        //Add each node in nodeList to the graph
        for (int i = 0; i < nodeList.Count; i++){
            //set the poisition of each node based on order in list
            nodeList[i].transform.SetPositionAndRotation(new Vector3(0, nodeDistVertical*i, 0), new Quaternion(0,0,0,0));
            graph.AddNode(nodeList[i], nodeList[i].transform.position);
        }

        //Add the connection to the base node and next node
        graph.AddEdge(baseNode, graph.Nodes[1].Value, 1);

        //Connect each node with the previous node
        for (int i = 1; i < nodeList.Count; i++){
            graph.AddEdge(graph.Nodes[i].Value, graph.Nodes[i + 1].Value, 1);
        }

        foreach(Node<GameObject> node in graph.Nodes){
            Debug.Log("Edge name is " + node.Value + " Number of edges is " + graph.numEdges(node.Value));
        }
    }

    /// <summary>
    /// Creates a graph of interconnected nodes
    /// </summary>
    void createInterGraph(){

        //Add the very first node/ root node to the graph
        graph.AddNode(baseNode, baseNode.transform.position);

        //For each node in nodeList, change name to the number it is in NodeList
        //The add each node to the graph
        for (int i = 0; i < nodeList.Count; i++){
            nodeList[i].name = "Node" + i;
            nodeList[i].GetComponent<NodeProperties>().nodeNumber = i;
            graph.AddNode(nodeList[i],nodeList[i].transform.position);
        }

        //Connect the first three nodes to the base node
        graph.AddEdge(baseNode, graph.Nodes[1].Value, 1);
        graph.Nodes[1].ParentNode = graph.Nodes[0];
        graph.AddEdge(baseNode, graph.Nodes[2].Value, -1);
        graph.Nodes[2].ParentNode = graph.Nodes[0];
        graph.AddEdge(baseNode, graph.Nodes[3].Value, 1);
        graph.Nodes[3].ParentNode = graph.Nodes[0];


        //For each node in the graph that has less than 3 neighbors
        //add an eadge to next node that has no other connections
        foreach (Node<GameObject> node in graph.Nodes)
        {
            foreach (Node<GameObject> node2 in graph.Nodes)
            {
                if (node.Neighbors.Count < 3)
                {
                    if(node2.Neighbors.Count == 0){
                        
                        node2.ParentNode = node;
                        graph.AddEdge(node.Value,node2.Value, 1);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Creates graph based on developer selected edges
    /// </summary>
    void handMadeGraph(){

        //Add the very first node/ root node to the graph
        graph.AddNode(baseNode, baseNode.transform.position);

        //For each node in nodeList, change name to the number it is in NodeList
        //The add each node to the graph
        for (int i = 0; i < nodeList.Count; i++)
        {
            nodeList[i].name = "Node" + i;
            nodeList[i].GetComponent<NodeProperties>().nodeNumber = i;
            graph.AddNode(nodeList[i], nodeList[i].transform.position);
        }

        //For each node, get list of edges from NodeProperties and add edges to those nodes
        foreach(Node<GameObject> node in graph.Nodes){
            foreach(GameObject edgeNode in node.Value.GetComponent<NodeProperties>().edges){
                if(graph.Find(edgeNode) != null){
                    graph.AddEdge(node.Value, edgeNode, 1);
                }
            }
        }
    }

    /// <summary>
    /// Returns the created graph
    /// </summary>
    /// <returns>Returns graph </returns>
    public Graph<GameObject> GetGraph()
    {
        return graph;
    }
}
