using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public Text itemName;
    public Text itemCost;

    public string iName;
    public string cost;

    // Start is called before the first frame update
    void Start()
    {
        itemName.text = iName;
        itemCost.text = cost;
        gameObject.transform.parent = StoreScript.instance.gameObject.transform;
        StoreScript.instance.itemPanels.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPanel(bool isSelected)
    {
        if(isSelected)
        {
            float gray = 53.0f / 256;
            GetComponent<Image>().color = new Color(gray, gray, gray, .83f);
            itemName.color = new Color(1f, 1f, 1f, 1f);
            itemCost.color = new Color(1f, 1f, 1f, 1f);
        } else
        {
            GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            itemName.color = new Color(0f, 0f, 0f, 1f);
            itemCost.color = new Color(0f, 0f, 0f, 1f);
        }
    }
}
