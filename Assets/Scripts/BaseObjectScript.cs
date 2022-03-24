// Created By Stephen R Ouellette

using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

// Required components can be added here...
[RequireComponent(typeof(NearInteractionGrabbable))] // Adds the visual representation that an object can be manipulated when near it...
[RequireComponent(typeof(ObjectManipulator))] // Adds the actual manipulation to the object...
[RequireComponent(typeof(ConstraintManager))] // Allows constraints to be added to the object...
public class BaseObjectScript : MonoBehaviour
{

}
