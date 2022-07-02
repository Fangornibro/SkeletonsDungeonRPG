using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NPC.isDialogueOpen || InteractableItem.isDialogueOpen)
        {
            transform.Find("DialogueHud").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("DialogueHud").gameObject.SetActive(false);
        }
    }
}
