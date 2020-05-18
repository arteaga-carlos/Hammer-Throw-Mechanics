using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{

    // Find child object from the parent transform
    public static Transform findChildTransform(Transform parent, string childName) {

        foreach (Transform childTransform in parent.GetComponentsInChildren<Transform>()) {

            if (childTransform.gameObject.name == childName) {

                return childTransform;
            }
        }

        return null;
    }
}
