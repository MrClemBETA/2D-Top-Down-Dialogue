using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Text itemName;               // Holds Name
    public Text itemCost;               // Holds Cost

    public string iName;
    public string cost;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        itemName.text = iName;
        itemCost.text = cost;

        // Make sure to correctly add to the HUD (Useful when these are instantiated)
        gameObject.transform.parent = StoreScript.instance.gameObject.transform;
        // Add this panel to the list
        StoreScript.instance.itemPanels.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPanel(bool isSelected)
    {
        // Is selected has a gray background with white text
        if(isSelected)
        {
            float gray = 53.0f / 256;
            GetComponent<Image>().color = new Color(gray, gray, gray, .83f);
            itemName.color = new Color(1f, 1f, 1f, 1f);
            itemCost.color = new Color(1f, 1f, 1f, 1f);
        } else  // Not selected has a transparent background with black text
        {
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            itemName.color = new Color(0f, 0f, 0f, 1f);
            itemCost.color = new Color(0f, 0f, 0f, 1f);
        }
    }
}
