using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeProperties : MonoBehaviour
{
    //Indicates if player has visited nodes
    bool visited = false;

    //List of every line in poemLine
    List<string> eachLine;

    //TextAsset poemLine as one string
    string theWholeFileAsOneLongString;

    //List of neighbor nodes
    public List<GameObject> edges = new List<GameObject>();

    public int nodeNumber;

    //The textbox that the poem line is contained in
    public Text lineHolder;
    public TextAsset poemLine;

    PlayerInventory PlayerInventory;

	void Start()
    {
        PlayerInventory = FindObjectOfType<PlayerInventory>();
        theWholeFileAsOneLongString = poemLine.text;

        eachLine = new List<string>();
        eachLine.AddRange(theWholeFileAsOneLongString.Split("\n"[0]));

        int kWords = eachLine.Count;
    }

    void OnMouseOver()
    {
        //Correspond node text with line in text file. If more nodes than lines, default last line as text
        if (nodeNumber < eachLine.Count)
        {
            lineHolder.text = eachLine[nodeNumber];
        }
        else
        {
            lineHolder.text = eachLine[eachLine.Count - 1];
        }
        lineHolder.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        lineHolder.gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the values of bool visited
    /// </summary>
    /// <returns>Returns bool visited</returns>
    public bool getVisited()
    {
        return visited;
    }

    /// <summary>
    /// Sets the valus of bool visited
    /// </summary>
    /// <param name="v">Parameter value to pass.</param>
    public void setVisited(bool v)
    {
        visited = v;
    }

    /// <summary>
    /// Adds line to the players inventory based on node
    /// </summary>
    public void addToInventory()
    {
        PlayerInventory.addToList(eachLine[nodeNumber]);
    }
}
