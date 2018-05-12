using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsScript : MonoBehaviour 
{
	public Quaternion LookAt2D(GameObject source, GameObject target)
	{
		if (source == null || target == null)
		{
			return Quaternion.identity;
		}
		Vector2 translationVector = target.transform.position - source.transform.position; //Calculates the Translation vector between the two objects.
		float angle = Mathf.Atan2 (translationVector.y, translationVector.x) * Mathf.Rad2Deg; //Calculates the angle between the TranslationVector's Y and X axes. Then convert it from radians to degrees.
		//Debug.Log (string.Format("The angle in degrees is: {0}", angle)); //Prints in Unity's Console the angle calculated above.
		//transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward); //Applies the calculated angle in Vector3.foward (0,0,1). In Unity3D 2D mode, the object rotation is defined by the Z-axis, therefore Vector3.foward is needed to rotate the object.
		return Quaternion.AngleAxis (angle, Vector3.forward); //Returns the calculated angle.
	}
}
