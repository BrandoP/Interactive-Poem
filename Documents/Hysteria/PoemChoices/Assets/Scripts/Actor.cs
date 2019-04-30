using UnityEngine;
using System.Collections.Generic;

public class Actor : MonoBehaviour {
	
	enum State
	{
		IDLE,
		MOVING,
	}

    bool onNode = true;
	
    float oldTime = 0;
    float checkTime = 0;
    float elapsedTime = 0;

    GameObject target;
    GameObject currNode;

    int nodeIndex;

    List<GameObject> path = new List<GameObject>();

    CreatePath pathCreater;
    NodeControl control;
    State state = State.IDLE;
	
    public bool DebugMode;
	
    public GameObject firstNode;
	
	void Awake()
	{
        //Find camera object
		GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        //Get NodeControl class
		control = (NodeControl)cam.GetComponent(typeof(NodeControl));

        currNode = firstNode;
        pathCreater = FindObjectOfType<CreatePath>();
	}
	
	void Update () 
	{
        //Keep track of total time
		elapsedTime += Time.deltaTime;
		
		if (elapsedTime > oldTime)
		{
			switch (state)
			{
			case State.IDLE:
				break;
				
			case State.MOVING:
			
                    oldTime = elapsedTime + 0.01f;

                    //Sets target periodically while moving
                    if (elapsedTime > checkTime){
					    checkTime = elapsedTime + 1;
					    SetTarget();
                        Debug.Log("In elapsed time");
				    }
				
			
                    if (path != null)
                    {
                        if (onNode)
                        {
                            //if onNode, set to not on node and change currNode to the next node in path
                            onNode = false;
						    if (nodeIndex < path.Count)
							    currNode = path[nodeIndex];
                                Debug.Log("Current node is " + currNode.name);
					    } else
                        {
                            //Move if not on node
                            MoveToward();  
                        }
                    }
                    break;
            }
		}
	}
	
    /// <summary>
    /// Move toward the clicked on node
    /// </summary>
	void MoveToward()
	{
		if (DebugMode)
		{
			for (int i=0; i<path.Count-1; ++i)
			{
                Debug.DrawLine(path[i].transform.position, path[i+1].transform.position, Color.white, 0.01f);
			}
		}

        //Change this objects poition to the currNode's position
        this.transform.position = Vector3.MoveTowards(this.transform.position, currNode.transform.position, 0.25f);
		
        //If palyer position and current node position are the same
        //Increase nodeIndex and set onNode to true 
        if(this.transform.position == currNode.transform.position){
            Debug.Log("Increasing node index");
			nodeIndex++;
			onNode = true;
		}
	}
	
    /// <summary>
    /// Sets the target node path to travel to the clicked node
    /// </summary>
	void SetTarget()
	{
        path = control.Path(currNode, target);
        currNode.GetComponent<NodeProperties>().setVisited(true);
        currNode.GetComponent<NodeProperties>().addToInventory();
		nodeIndex = 0;
		onNode = true;
	}

    /// <summary>
    /// Set the state of the player to newState
    /// </summary>
    /// <param name="newState">Parameter value to pass.</param>
    void ChangeState(State newState)
    {
        state = newState;
    }

    /// <summary>
    /// Sets the target and state of the player to moving
    /// </summary>
    /// <param name="pos">Parameter value to pass.</param>
    public void MoveOrder(GameObject pos)
	{
		target = pos;
		SetTarget();
		ChangeState(State.MOVING);
	}

    /// <summary>
    /// Sets the current node
    /// </summary>
    /// <param name="curNode">Parameter value to pass.</param>
    public void setCurrentNode(GameObject curNode){
        currNode = curNode;
    }
}
