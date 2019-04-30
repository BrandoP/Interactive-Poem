using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphs;

public class PlayerController : MonoBehaviour
{
	public float movement_speed = 3f;
	public float rotation_speed = 10f;

    public GameObject rootNode;

    float Timer;
    static Vector3 CurrentPositionHolder;

    public CreatePath pathCreater;
    public Node<GameObject>[] points;
    public List<GameObject> rightPoints;
    public List<GameObject> leftPoints;
    public List<GameObject> upPoints;
    int currentPoint = 0;
    int closest = 0;

    List<GameObject> validMoves;

    public float distance = 2.0f;
    Vector3 direction;

    void Start()
    {
        //getClosestNode();
    }

	void Update ()
	{
		// MOVEMENT
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        // Vertical is forwards/backwards
        //this.transform.Translate(Vector2.up * vertical_input * movement_speed * Time.deltaTime);

        //this.transform.Translate(Vector2.right * horizontal_input * movement_speed * Time.deltaTime);

        //currentPoint = getClosestNode(horizontal_input, vertical_input);

        if(horizontal_input < 0){
            Debug.Log("Got horizontal input");
            foreach (Edge<GameObject> edge in pathCreater.GetGraph().Nodes[currentPoint].Edges)
            {
                Debug.Log("Getting weights " + edge.getWeight());
  
                    if (edge.getWeight() > 0)
                    {
                        Debug.Log("found edge greate than 0 edgde weight " + edge.getWeight() + "edge names are " + edge.GetNode(1).Value.name + edge.GetNode(0).Value.name);
                        this.transform.position = Vector3.MoveTowards(transform.position, edge.GetNode(0).Value.transform.position, Time.deltaTime * movement_speed * horizontal_input);
                    }

            }
        }
	}

    int getClosestNode(){
        foreach(Node<GameObject> node in pathCreater.GetGraph().Nodes)
        {
            for (int i = 0; i < node.Edges.Count; i++){
                Debug.Log(node.Edges[i].getWeight());
            }
         }

        return closest;
    }
}