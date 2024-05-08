using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{

    public GameObject dialogPanel;

    public List<string> dialogLines = new List<string>();

    public string nameOfNpc;

    public Button contButton;
    Text dialogText, nameText;
    int dialogIndex;

    private void Awake()
    {
        dialogText = dialogPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        nameText = dialogPanel.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        dialogPanel.SetActive(false);

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public static DialogSystem instance;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddDialog(string[] lines, string npcName)
    {
        dialogIndex = 0;
        dialogLines = new List<string>(lines.Length);
        dialogLines.AddRange(lines);
        this.nameOfNpc = npcName;
        CreateDialog();
    }

    public void CreateDialog()
    {
        dialogText.text = dialogLines[dialogIndex];
        nameText.text = nameOfNpc;
        dialogPanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void ContinueDialog()
    {
        if (dialogIndex < dialogLines.Count - 1)
        {
            dialogIndex++;
            dialogText.text = dialogLines[dialogIndex];
        }
        else
        {
            dialogPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
