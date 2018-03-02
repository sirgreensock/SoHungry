using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDragController : MonoBehaviour {

	private bool draggingItem = false;
	private GameObject draggedObject;
	private Vector2 touchOffset;
    private bool validItem = false;
    RaycastHit2D[] touches;
    public int targetSortingLayer = 6;
    private int defaultSortingLayer;

    void Start ()
    {       
        
    }

    void Update () {

        //Checks if mouse button 1 is held, if false drops item
        if (HasInput)
		{
			DragOrPickup();            
        }
		else
		{
            EyeTracker.itemSelected.itemSelectedBool = false; //inform eyes to stop tracking
            if (draggingItem)
            {
                DropItem();      

            }
        }
		
	}

	Vector2 CurrentTouchPosition
	{
		get
		{
			Vector2 inputPos;
			inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return inputPos;
		}
	}

	private void DragOrPickup()
	{
		var inputPosition = CurrentTouchPosition;
		if (draggingItem)
		{
			draggedObject.transform.position = inputPosition + touchOffset;
            EyeTracker.itemSelected.itemSelectedBool = true;
        }
		else
		{
			touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
			if (touches.Length > 0)			{

                for (int i = 0; i < touches.Length; i++)
                {

                    var hit = touches[i];

                    //checks if valid item was already selected, if true the loop ends
                    if (validItem)                    {

                        return;
                    }

                    var objectTag = hit.transform.gameObject.tag;

                    //checks if current object is valid, if not it moves on to the next one
                    if (objectTag == "GoodTarget" || objectTag == "BadTarget") {
                        validItem = true;
                    } else
                    {
                        continue;
                    }
                    
                    //if item is draggable, drags item
                    if (hit.transform != null)
                    {
                        draggingItem = true;
                        draggedObject = hit.transform.gameObject;
                        touchOffset = (Vector2)hit.transform.position - inputPosition;

                        defaultSortingLayer = draggedObject.GetComponent<SpriteRenderer>().sortingOrder;

                        var hitLayer = draggedObject.GetComponent<SpriteRenderer>();
                        hitLayer.sortingOrder = targetSortingLayer; //raise item on sorting layer
                        draggedObject.transform.parent.gameObject.GetComponent<FoodController>().GrabItem();
                    }
                }

			}
		}
	}

	private bool HasInput
	{
		get
		{
			//returns true if either the mouse button is down or touch is detected
			return Input.GetMouseButton(0);
		}
	}
    
	public void DropItem()
	{
		draggingItem = false; //an item isnt being dragged
        validItem = false; //no valid item selected
        if (draggedObject != null)
        {
            draggedObject.GetComponent<SpriteRenderer>().sortingOrder = defaultSortingLayer; //reset sorting layer of dropped item
            draggedObject.transform.parent.gameObject.GetComponent<FoodController>().DropItem();
        }
    }
	


}
