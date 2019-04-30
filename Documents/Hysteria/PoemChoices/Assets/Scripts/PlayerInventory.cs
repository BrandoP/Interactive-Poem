using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    List<string> wordsChosen = new List<string>();

    public ScrollRect scrollRect;
    public Text textbox;

    /// <summary>
    /// Adds a string to the wordsChosen list and displaying the added words
    /// in the scrollRect
    /// </summary>
    /// <param name="t">Parameter value to pass.</param>
	public void addToList(string t){
        //If wordsChosen does not already contain the new string, add it to the list
        if(!wordsChosen.Contains(t)){
            
            wordsChosen.Add(t);

            //Create a new textbox to display t
            Text newLine = Instantiate(textbox);
            newLine.transform.SetParent(this.transform);

            //Set new textbox text to added string t
            newLine.text = t;
        }
    }
}
