using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AnimLen))]
public class AnimLenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AnimLen heyMan = (AnimLen) target;
        // AnimationClip[] animationClips = heyMan.runtimeAnimatorController.animationClips;

        // Iterate over the clips and gather their information
        // foreach (AnimationClip animClip in animationClips)
        // {
        //     Debug.Log(animClip.name + ": " + animClip.length);
        // }


        if (GUILayout.Button("Reset"))
        {
        }
    }
}