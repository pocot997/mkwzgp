using DVSN.GameManagment;
using UnityEngine;

public class CasterController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Managers.Spellcasting.InstantiateSpell(0, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Managers.Spellcasting.InstantiateSpell(1, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Managers.Spellcasting.InstantiateSpell(2, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Managers.Spellcasting.InstantiateSpell(3, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Managers.Spellcasting.InstantiateSpell(4, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Managers.Spellcasting.InstantiateSpell(5, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Managers.Spellcasting.InstantiateSpell(6, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Managers.Spellcasting.InstantiateSpell(7, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Managers.Spellcasting.InstantiateSpell(8, transform.gameObject.name);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Managers.Spellcasting.InstantiateSpell(9, transform.gameObject.name);
        }
    }
}