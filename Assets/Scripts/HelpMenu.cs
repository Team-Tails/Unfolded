using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    [SerializeField]
    Canvas menu;
    [SerializeField]
    Canvas help;
    [SerializeField]
    GameObject page1;
    [SerializeField]
    GameObject page2;
    [SerializeField]
    GameObject keyboardLayout;
    [SerializeField]
    GameObject controllerLayout;

    public void Return()
    {
        menu.enabled = !menu.enabled;
        help.enabled = !help.enabled;
    }

    public void PageButton()
    {
        page1.SetActive(!page1.activeSelf);
        page2.SetActive(!page2.activeSelf);
    }
}
