
using Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vuforia;

[CreateAssetMenu(fileName = "CrossManager", menuName = "Managers/CrossManager")]
class CrossManager : ManagerBase
{
	[SerializeField]
	public GameObject prefabOfCross;
	[SerializeField]
	public GameObject prefabOfArCross;

	public GameObject Cross { get; private set; }

    public void SetCrossGO(bool AR)
    {
        
        if (AR)
        {
			Cross = Instantiate(prefabOfArCross, GameObject.Find("Static").transform, false);

        }
		else
		{
			Cross = Instantiate(prefabOfCross, GameObject.Find("Static").transform, false);
		}
        Cross.transform.eulerAngles= new Vector3 (0, 0, 0);
    }
    public void setAngles()
    {
        Cross.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}

