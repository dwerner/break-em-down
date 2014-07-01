using UnityEngine;
using System.Collections;
public class GlobalMusic : MonoBehaviour {

  public static AudioSource instance = null;

  void Awake() {

    if (instance == null) {
      DontDestroyOnLoad(transform.gameObject);
      instance = GetComponent<AudioSource>();
      if (instance != null) {
        instance.Play();
      }
    } else {
      DestroyImmediate(transform.gameObject);
    }
  }
}
