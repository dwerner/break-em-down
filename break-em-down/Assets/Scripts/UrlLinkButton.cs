using UnityEngine;
using System.Collections;

public class UrlLinkButton : MonoBehaviour {

  public string url;

  void OnMouseUpAsButton(){
    Debug.Log("opening url:" + url);
    Application.OpenURL(url);
  }

}
