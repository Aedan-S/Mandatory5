using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mid_FoxAndChicken : MonoBehaviour
{
    //public Vector3 startPosition, endPosition1, endPosition2;
    public GameObject ghostAnimal1, ghostAnimal2;
    public bool firstSeat, canPressE, hasBeenMoved;
    public Mid_Boat boat;
    public Mid_StartingArea startingArea;

    public void Start()
    {

    }

    private void Update()
    {
        if (canPressE && boat.boatIsFull == false) 
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                boat.MoveAnimalToBoat();
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
        if (hasBeenMoved)
        {
            startingArea.animalsToBeMoved.Remove(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPressE = true; 
            boat.objectToMove = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canPressE = false;
        boat.objectToMove = null;
    }
}
