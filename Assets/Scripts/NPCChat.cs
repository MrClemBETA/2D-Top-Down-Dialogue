using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCChat : MonoBehaviour
{
    public string characterName;
    public List<string> dialogue;

    private int linesRead = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Chat(Text dialogueText, Text nameText, string playerName)
    {
        // This runs when all lines have been read
        if (linesRead >= dialogue.Count)
        {
            linesRead = 0;
            return false;
        } else
        {
            if (dialogue[linesRead] == "*n")
            {
                nameText.text = characterName;
                linesRead++;
            } else if(dialogue[linesRead] == "*p")
            {
                nameText.text = playerName;
                linesRead++;
            }
            dialogueText.text = dialogue[linesRead++];
            return true;
        }
    }
}
