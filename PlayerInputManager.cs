using UnityEngine;
using System.Collections.Generic;

public class PlayerInputManager : MonoBehaviour
{
    // Use this for initialization
    public enum KeyRole { MovimentationFront = 0, MovimentationBack = 1, MovimentationLeft = 2, MovimentationRight = 3, MovimentationRun = 4, MovimentationCrouch = 5, MovimentationJump = 6 };

    public List<KeyRole> WhichKeyRoles = new List<KeyRole>();
    public List<KeyCode> ForWichKeys = new List<KeyCode>();

    public List<bool> KeysPressed = new List<bool>();
    public List<float> KeysLast = new List<float>();
    public List<float> KeysTime = new List<float>();

    public void Start()
    {
        while (KeysPressed.Count < ForWichKeys.Count)
        {
            KeysPressed.Add(false);
        }
        while (KeysLast.Count < ForWichKeys.Count)
        {
            KeysLast.Add(0);
        }
        while (KeysTime.Count < ForWichKeys.Count)
        {
            KeysTime.Add(0);
        }
    }

    public void CheckKeysUpdate()
    {
        KeyDowns();
        KeyUps();
    }

    public bool IsPressed(KeyRole key)
    {
        return KeysPressed[WhichKeyRoles.IndexOf(key)];
    }

    public void Assignar(int player)
    {
        string curFile = Application.dataPath + "/player" + player + "controls.txt";
        PlayerInputManager n = new PlayerInputManager();
        /*using (StreamReader sr = File.OpenText(curFile))
        {

            string s = "";
            int teclasLidas = 0;
            while ((s = sr.ReadLine()) != null)
            {
                teclasLidas++;
                KeyCode kc = (KeyCode)System.Enum.Parse(typeof(KeyCode), s);
                switch (teclasLidas)
                {
                    case 1: n.upKey = kc; break;
                    case 2: n.downKey = kc; break;
                    case 3: n.leftKey = kc; break;
                    case 4: n.rightKey = kc; break;

                    case 5: n.attackKey = kc; break;
                    case 6: n.specialKey = kc; break;
                    case 7: n.jumpyKey = kc; break;
                    case 8: n.grabKey = kc; break;
                    case 9: n.defenseKey = kc; break;
                }
            }
        }*/

        /*upKey = n.upKey;
        downKey = n.downKey;
        leftKey = n.leftKey;
        rightKey = n.rightKey;

        attackKey = n.attackKey;
        specialKey = n.specialKey;
        grabKey = n.grabKey;
        jumpyKey = n.jumpyKey;
        defenseKey = n.defenseKey;*/
    }

    void KeyDowns()
    {
        for (int i = 0; i < ForWichKeys.Count; i++)
        {
            if (Input.GetKeyDown(ForWichKeys[i]))
            {
                if (!KeysPressed[i])
                    KeysTime[i] = 0.0f;
                KeysPressed[i] = true;
            }
        }
    }

    void KeyUps()
    {
        for (int i = 0; i < ForWichKeys.Count; i++)
        {
            if (Input.GetKeyUp(ForWichKeys[i]))
            {
                KeysLast[i] = 0.0f;
                KeysPressed[i] = false;
            }
        }
    }

    const float maxTimeAmount = 5.0f;
    public void TimeCounter()
    {
        for (int i = 0; i < ForWichKeys.Count; i++)
        {
            if (!KeysPressed[i])
            {
                if (KeysLast[i] < maxTimeAmount)
                {
                    KeysLast[i] += Time.deltaTime;
                }
            }
            else
            {
                if (KeysTime[i] < maxTimeAmount)
                {
                    KeysTime[i] += Time.deltaTime;
                }
            }
        }
    }
}
