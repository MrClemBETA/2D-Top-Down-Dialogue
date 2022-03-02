using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    public static StoreScript instance = null;

    public List<GameObject> itemPanels;

    public int selectedItem = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedItem = selectedItem - 1 < 0 ? 0 : selectedItem - 1;
            for (int i = 0; i < itemPanels.Count; i++)
            {
                itemPanels[i].GetComponent<ItemPanel>().SelectPanel(i == selectedItem);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedItem = selectedItem + 1 >= itemPanels.Count ? itemPanels.Count - 1 : selectedItem + 1;
            for (int i = 0; i < itemPanels.Count; i++)
            {
                itemPanels[i].GetComponent<ItemPanel>().SelectPanel(i == selectedItem);
            }
        }
    }
}
