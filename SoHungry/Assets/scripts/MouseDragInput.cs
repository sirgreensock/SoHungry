using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDragInput : MonoBehaviour {

	private bool draggingItem = false;
	private GameObject draggedObject;
	private Vector2 touchOffset;

    void Start ()
    {
       // EyeTracker eyeTracker = gameObject.GetComponent<EyeTracker>();
        
    }

    // Update is called once per frame
    void Update () {
        EyeTracker eyeTracker = gameObject.GetComponent<EyeTracker>();
        if (HasInput)
		{
			DragOrPickup();            
        }
		else
		{
			if(draggingItem) DropItem();
            EyeTracker.itemSelected.itemSelectedBool = false;
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
			RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
			if (touches.Length > 0)
			{
				var hit = touches[0];
				if(hit.transform != null)
				{
				draggingItem = true;
				draggedObject = hit.transform.gameObject;
				touchOffset = (Vector2)hit.transform.position - inputPosition;
				draggedObject.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
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

	void DropItem()
	{
		draggingItem = false;
		draggedObject.transform.localScale = new Vector3(1f,1f,1f);
	}
	


}
