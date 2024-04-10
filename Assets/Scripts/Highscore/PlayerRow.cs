using UnityEngine;
using TMPro;

/// <summary>
/// Helperclass for HighscoreController to reference the text in the inspector.
/// </summary>
public class PlayerRow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI name_text;
    public TextMeshProUGUI Name_Text
    {
        get { return name_text; }
        set { name_text = value; }
    }

    [SerializeField]
    private TextMeshProUGUI score_text;
    public TextMeshProUGUI Score_Text
    {
        get { return score_text; }
        set { score_text = value; }
    }

    [SerializeField]
    private TextMeshProUGUI time_text;
    public TextMeshProUGUI Time_Text
    {
        get { return time_text; }
        set { time_text = value; }
    }

    [SerializeField]
    private TextMeshProUGUI date_text;
    public TextMeshProUGUI Date_Text
    {
        get { return date_text; }
        set { date_text = value; }
    }
}
