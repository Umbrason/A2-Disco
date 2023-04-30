using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheatCodeContoller : MonoBehaviour
{
    private ICheatCode[] cheatCodes = null;
    private int maxLength = 0;
    private string input = "";
    
    void Awake()
    {
        cheatCodes = GetComponents<ICheatCode>();
        maxLength = cheatCodes.Select(cheatCode => cheatCode.Name.Length).Max();
    }

    void OnEnable()
    {
        Keyboard.current.onTextInput += onTextInput;
    }

    void OnDisable()
    {
        Keyboard.current.onTextInput -= onTextInput;
    }

    private void onTextInput(char key)
    {
        if (input.Length == maxLength) input = input.Substring(1);
        input += char.ToLower(key);

        foreach (var cheatCode in cheatCodes)
        {
            if (input.EndsWith(cheatCode.Name.ToLower()))
            {
                cheatCode.Execute();
                break;
            }
        }
    }
}
