using UnityEngine;
using System;



public class Character : MonoBehaviour
{
    public string characterName;
    public int cost;
	public int premiumCost;


    public Animator animator;
	public Sprite icon;

	[Header("Sound")]
	public AudioClip jumpSound;
	public AudioClip hitSound;
	public AudioClip deathSound;


}
