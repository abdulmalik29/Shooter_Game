using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScrollUV : MonoBehaviour
{
	public float parralax = 2f;

	private Material mat;

	private void Start()
    {
		MeshRenderer mr = GetComponent<MeshRenderer>();
		mat = mr.material;
	}
    void Update()
	{
		Vector2 offset = mat.mainTextureOffset;

		offset.x = transform.position.x / transform.localScale.x / parralax;
		offset.y = transform.position.y / transform.localScale.y / parralax;

		mat.mainTextureOffset = offset;

	}
}
