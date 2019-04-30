using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Graphs;

public class NodeControl : MonoBehaviour {

    Node<GameObject> startNode;
    Node<GameObject> targetNode;

    public CreatePath createdPath;

    /// <summary>
    /// Returns a path from the starting node position to
    /// the target nodes poisiton
    /// </summary>
    /// <param name="startPos">Parameter value to pass.</param>
    /// <param name="targetPos">Parameter value to pass.</param>
    /// <returns>Returns a list of nodes in the path between startPos to targetPos</returns>
    public List<GameObject> Path(GameObject startPos, GameObject targetPos){

        //Create a list of gameobjects, the path
        List<GameObject> path = new List<GameObject>();

        //If startPos and targetPos are the same, return nothing
        if (startPos == targetPos)
        {
            return path;
        }else{
            path.Add(startPos);

            //Node<GameObject> graphNode = pathCreater.GetGraph().Find(startPos);

            //if(graphNode != null){
            //    foreach(Node<GameObject> node in graphNode.Neighbors){
            //        path.Add(node.Value);
            //    }
            //}

            //Find the node equal to startPos in the graph
            Node<GameObject> start = createdPath.GetGraph().Find(startPos);

            //If start is no empty, search through neighbors to find node equal to targetPos
            if(start != null){
                foreach(Node<GameObject> n in start.Neighbors){
                    if(n.Value == targetPos){
                        path.Add(targetPos);
                    }
                }
            }
            return path; 
        }
    }

}
