using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSound : MonoBehaviour
{
    private HingeJoint2D hinge;
    public AudioSource[] chainSounds;
    private int chainSoundChoice;
    private bool stopSounds = false; //So the sounds don't constantly play

    public int strength = 300; //The force at which the sound will be played. 
    //This will be different depending on the mass and amount of chains.

    //private bool letSound = true;


    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>(); //Sets the hinge variable as the connected HingeJoint2D
    }
    
    void Update()
    {
        if (hinge.reactionForce.magnitude > strength && !stopSounds) //If the force pulling on the hinge is greater than the threshold
        {
            chainSoundChoice = Random.Range(0, chainSounds.Length); //Choosing a random sound so it's not always the same
            chainSounds[chainSoundChoice].Play();
            stopSounds = true; //stops the sounds playing constantly
        }

        else if(hinge.reactionForce.magnitude <= strength && stopSounds)
        {
            stopSounds = false; //resets the stopSounds bool so the sound can be played again 
        }
    }
}
