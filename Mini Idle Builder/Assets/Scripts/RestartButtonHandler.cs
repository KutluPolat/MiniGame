using UnityEngine;

public class RestartButtonHandler : MonoBehaviour
{
    public void Restart()
    {
        EventManager.Instance.OnPressedRestart();
    }
}
