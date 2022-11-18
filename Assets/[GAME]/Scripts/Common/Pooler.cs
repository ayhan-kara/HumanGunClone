using UnityEngine;
using System.Collections.Generic;

public class Pooler : MonoBehaviour {

	#region Public Editor

	public GameObject defaultGo;
	public int poolCount = 1;

	private List<GameObject> list; // do not use prewarmed list (LevelDesigner -> Editor Mode)

	#endregion

	#region Private Vars

	int lastIndex = 0;
	

	#endregion

	#region MonoBehaviour

	private void Awake()
    {
		InitPooler();
    }

    public void InitPooler() {
		if (list == null)
			list = new List<GameObject>();

		defaultGo.transform.parent = transform;

		//list.Add(defaultGo);

        while(list.Count < poolCount)
		{
			GameObject go = Instantiate(defaultGo);
			go.SetActive(false);

            go.transform.parent = transform;

			list.Add(go);
		}

		defaultGo.SetActive(false);		
	}

    #endregion

    #region Public Functions

    public GameObject GetGo()
	{
		GameObject go = null;

        if (!Application.isPlaying)
		{
            go = Instantiate(defaultGo);
            go.transform.parent = transform;
            go.SetActive(false);

			return go;
        }

		for (int i = 0; i < list.Count; i++)
		{
			lastIndex++;
			if (lastIndex > list.Count - 1)
				lastIndex = 0;
			if (list[lastIndex].gameObject.activeSelf)
				continue;
			return list[lastIndex];
		}


		go = Instantiate(defaultGo);
		go.transform.parent = transform;
		go.SetActive(false);

		list.Add(go);

		return go;
	}

	public T GetGo<T>() where T:MonoBehaviour {
		GameObject go = GetGo();

		if (go == null)
			return null;

		return go.GetComponent<T>();
	}

    #endregion

}
