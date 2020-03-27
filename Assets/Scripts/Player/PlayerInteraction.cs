using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class allows the player to interact with differents object in the world, like picking items or searching corpses.
/// </summary>
[RequireComponent(typeof(Inventory))]
public class PlayerInteraction : MonoBehaviour
{
    protected enum InteractionType { Pick, Interact, None};

    [SerializeField]
    protected float m_interactionDistance = 3f;

    [SerializeField]
    protected LayerMask m_pickItemMask;
    [SerializeField]
    protected LayerMask m_interactiblesMask;

    [SerializeField]
    protected string m_pickName = "Prendre";
    [SerializeField]
    protected GameObject m_interactionPanel;
    [SerializeField]
    protected Text m_interactibleObjectName;
    [SerializeField]
    protected Text m_interactionName;

    [SerializeField]
    protected Transform m_playerView;

    protected GameObject m_interactibleObject;

    protected Inventory m_inventory;
    protected GameObject m_lastInteractibleObject;

    protected InteractionType m_interactionType = InteractionType.None;

    protected void Start()
    {
        m_inventory = GetComponent<Inventory>();
    }

    protected void Update()
    {
        CheckForInteraction();

        if (Input.GetButtonDown("Interact"))
        {
            switch(m_interactionType)
            {
                case InteractionType.Pick:
                    Pick();
                    break;

                case InteractionType.Interact:
                    Debug.Log("Some Interaction");
                    break;

                default:
                    Debug.Log("No possible interaction");
                    break;
            }
            
        }
    }

    protected bool Pick()
    {
        if (m_interactibleObject == null)//If there isn't an item to pick
        {
            Debug.Log("Interactible object = null");
            return false;
        }

        ItemData itemData = m_interactibleObject.GetComponent<ItemData>();

        if(itemData == null)
        {
            Debug.Log("Unable to find Component of type ItemData");
            return false;
        }

        //try to add the item to the inventory
        if(m_inventory.AddItem(itemData) == false)//Unable to add item to inventory
        {
            Debug.Log("Cannot add item " + itemData.GetName() + " to inventory.");

            return false;
        }

        //If item was added to inventory
        Debug.Log(itemData.GetName() + " added to inventory.");

        Destroy(m_interactibleObject);

        return true;
    }

    //This methods update the current interactible object in front of the player, and at range.
    protected void CheckForInteraction()
    {
        RaycastHit hit;

        //Check for an interactible object in front of the player at range
        if(Physics.Raycast(m_playerView.position, m_playerView.forward, out hit, m_interactionDistance, m_interactiblesMask))
        {

            GameObject obj = hit.collider.gameObject;

            //We pass the code if the object is the same of the object of the last frame
            if (obj != m_lastInteractibleObject)
            {

                m_interactibleObject = obj;//set the current interactible object
                m_interactionPanel.SetActive(true);//Display the interaction panel

                if (m_pickItemMask == (m_pickItemMask | 1 << m_interactibleObject.layer))//case of a pickable item
                {
                    //Set the current type of the interactible object
                    m_interactionType = InteractionType.Pick;

                    //Set the informations on the panel
                    m_interactibleObjectName.text = m_interactibleObject.GetComponent<ItemData>().GetName();
                    m_interactionName.text = m_pickName;
                }
                else
                {
                    //Set the current type of the interactible object
                    m_interactionType = InteractionType.Interact;

                    //Set the informations on the panel
                    m_interactibleObjectName.text = "Some interactible object";
                    m_interactionName.text = "Interaction";
                }

                m_lastInteractibleObject = m_interactibleObject;
            }
        }
        else//if no interactible object was found
        {
            //Set the current type of the interactible object to None
            m_interactionType = InteractionType.None;

            //disable the current object
            m_interactibleObject = null;
            m_lastInteractibleObject = null;
            //disable the panel
            m_interactionPanel.SetActive(false);
        }
    }

}
