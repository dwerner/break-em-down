using System;
using System.Collections;
using UnityEngine;


public class MainMenuButton : MonoBehaviour	{
	
	public Material defaultMaterial;
	public Material highlightMaterial;


	public void OnMouseEnter(){
		this.renderer.material = highlightMaterial;
	}
	
	public void OnMouseExit(){
		this.renderer.material = defaultMaterial;
	}
	
}
