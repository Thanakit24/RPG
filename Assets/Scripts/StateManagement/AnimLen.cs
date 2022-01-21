using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class AnimLen : MonoBehaviour
{
    private Dictionary<string, float> animLens = new Dictionary<string, float>();
    void Start()
    {
        foreach (var anim in GetComponent<Animator>().runtimeAnimatorController.animationClips)
        {
           animLens.Add(anim.name, anim.length); 
        }
    }
}
