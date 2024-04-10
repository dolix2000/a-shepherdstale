using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Highscore
{
    /// <summary>
    /// Holds the logic methods for the HighscoreView script.
    /// Such as insantiating the layout and inserting the players as rows
    /// </summary>
    public class HighscoreController
    {
        private IHighscoreView highscoreView;

        #region UI Elements
        private RectTransform canvasRect;
        private VerticalLayoutGroup canvasLayout;

        private VerticalLayoutGroup playerTable;
        private TextMeshProUGUI title;
        #endregion

        // for saving the rows as gameobjects
        private GameObject[] playerRows;

        public HighscoreController(IHighscoreView highscoreView)
        {
            this.highscoreView = highscoreView;
            //canvas = new GameObject("Canvas").AddComponent<Canvas>();
            canvasLayout = new GameObject("CanvasLayout").AddComponent<VerticalLayoutGroup>();
            playerTable = new GameObject("PlayerTable").AddComponent<VerticalLayoutGroup>();
            title = new GameObject("Title").AddComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Method for building the highscore layout.
        /// </summary>
        /// <param name="title">of the highscorelist</param>
        /// <param name="font"> that gets used</param>
        public void BuildHighscoreLayout(Canvas canvas, string title, TMP_FontAsset font)
        {
            BuildCanvas(canvas);
            BuildCanvasLayout(canvas);
            BuildTitle(title, font);
            BuildPlayerTable();
        }

        /// <summary>
        /// Method for instantiating the players into a table 
        /// as rows (prefab)
        /// Also saves the players into an array (as gameobjects if they need to be destroyed later)
        /// </summary>
        /// <param name="players">that get instantiated per row</param> 
        public void InstantiatePlayers(List<Player> player)
        {
            playerRows = new GameObject[player.Count];

            // instantiating rows into the highscorelist scene (name, score, time)
            for (int i = 0; i < player.Count; i++)
            {
                // instantiate new playerRow (child) per player into playerTable (parent object)
                GameObject playerRow = GameObject.Instantiate<GameObject>(highscoreView.PlayerRow, playerTable.transform, false);

                PlayerRow textElements = playerRow.GetComponent<PlayerRow>();

                textElements.Name_Text.text = player[i].Player_Name;
                textElements.Score_Text.text = player[i].Score.ToString();
                textElements.Time_Text.text = player[i].Play_Time;
                textElements.Date_Text.text = player[i].Date;

                // creating reference of row for example destroying the object later if needed
                playerRows[i] = playerRow;
            }
        }

        /// <summary>
        /// Method for instantiating the headRow/headColumn
        /// </summary>
        /// <param name="headColumns">, columns of the highscoretable</param>
        public void InstantiateHeadColumns(string[] headColumns)
        {
            // initiate first row with the columns name, score, time
            GameObject firstRow = GameObject.Instantiate<GameObject>(highscoreView.PlayerRow, playerTable.transform);

            PlayerRow textElements = firstRow.GetComponent<PlayerRow>();

            for (int i = 0; i < headColumns.Length; i++)
            {
                textElements.transform.GetChild(i).GetComponent<TMP_Text>().text = headColumns[i];
            }
        }

        #region private UI methods
        /// <summary>
        /// Method for building the canvas
        /// Adding necessary components
        /// </summary>
        private void BuildCanvas(Canvas canvas)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay; // automatically resizes based on size of screen
            canvas.gameObject.AddComponent<CanvasScaler>(); // scaling affects everything under the canvas
            canvas.gameObject.AddComponent<GraphicRaycaster>(); // allows to interact with all UI objects that are children of the canvas
            canvasRect = canvas.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Method for creating a vertical layout group inside of the canvas
        /// Setting the width and height the same as the canvas size
        /// Furthermore setting expand options and adding the canvas as the parent
        /// </summary>
        private void BuildCanvasLayout(Canvas canvas)
        {
            // setting width and height the same as the canvas itself
            canvasLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRect.rect.width, canvasRect.rect.height);

            canvasLayout.childAlignment = TextAnchor.UpperCenter;

            // disable control height and force expand so children of the vertical layout group can be scaled independently
            canvasLayout.childControlHeight = false;
            canvasLayout.childControlWidth = false;
            canvasLayout.childForceExpandHeight = false;
            canvasLayout.childForceExpandWidth = false;
            canvasLayout.spacing = 80;

            canvasLayout.transform.localPosition = Vector3.zero;
            canvasLayout.transform.SetParent(canvas.transform, false);
        }

        /// <summary>
        /// Method for creating the title
        /// </summary>
        /// <param name="title">of the Highscorelist</param> 
        /// <param name="font">the Highscore title uses</param> 
        private void BuildTitle(string title, TMP_FontAsset font)
        {
            this.title.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRect.rect.width, 60);
            // adding title to vertical layoutGroup
            this.title.transform.SetParent(canvasLayout.transform, false);

            this.title.text = title;
            Debug.Log(title);
            this.title.font = font;
            this.title.fontSize = 34;
            this.title.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        }

        /// <summary>
        /// Method for building the table the player rows will be inserted in
        /// </summary>
        private void BuildPlayerTable()
        {
            playerTable.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasRect.rect.width, canvasRect.rect.height - 60);

            // disable control height and force expand so children of the vertical layout group can be scaled independently
            playerTable.childAlignment = TextAnchor.UpperCenter;
            playerTable.childControlHeight = false;

            playerTable.childForceExpandHeight = false;
            playerTable.childForceExpandWidth = false;

            playerTable.transform.SetParent(canvasLayout.transform, false);
        }
        #endregion
    }
}
