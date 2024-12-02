using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Dan.Main;

namespace LeaderboardCreatorDemo
{
    public class Leaderboard : MonoBehaviour
    {
        public bool English;
        [SerializeField] private List<Text> leaderboardRanks;
        [SerializeField] private List<Text> leaderboardNames;
        [SerializeField] private List<Text> leaderboardScores;
        [SerializeField] private InputField inputname;
        [SerializeField] private Text inputScore;
        [SerializeField] private Text Scoretext;

        [SerializeField] private Text nametitle;
        [SerializeField] private Text ranktitle;
        [SerializeField] private Text scoretitle;

        [SerializeField] private Button submitButton;
        [SerializeField] private Button backButton;

        public UnityEvent Onback;

        private void Start()
        {
            if(English)
            {
                inputname.text = "Input name";
                Scoretext.text = "Current score";
                nametitle.text = "Name";
                ranktitle.text = "Rank";
                scoretitle.text = "Score";
            }
            else
            {
                inputname.text = "Inserir nick";
                Scoretext.text = "Pontuacao atual";
                nametitle.text = "Nick";
                ranktitle.text = "Rank";
                scoretitle.text = "Score";
            }
            backButton.onClick.AddListener(() => OnbackButtonClick());
            GetLeaderboard();
            submitButton.onClick.AddListener(() => UploadEntry(inputname.text, int.Parse(inputScore.text)));

            // Subscribe to Onback with ShowStartScreen method reference (not a call)
            Onback.AddListener(() => GameObject.FindGameObjectWithTag("LetterManager").GetComponent<LetterManager>().ShowStartScreen());
        }

        private void GetLeaderboard()
        {
            Leaderboards.DoctorPleaseLeaderboard.GetEntries(msg =>
            {
                int looplength = Mathf.Min(msg.Length, leaderboardNames.Count);
                for (int i = 0; i < looplength; i++)
                {
                    leaderboardRanks[i].text = msg[i].Rank.ToString();
                    leaderboardNames[i].text = msg[i].Username;
                    leaderboardScores[i].text = msg[i].Score.ToString();
                }
            });
        }

        private void UploadEntry(string name, int score)
        {
            Leaderboards.DoctorPleaseLeaderboard.UploadNewEntry(name, score, isSuccessful =>
            {
                if (isSuccessful)
                {
                    Leaderboards.DoctorPleaseLeaderboard.ResetPlayer();
                    GetLeaderboard();
                }
            });
            inputScore.text = "0";
        }

        public void OnbackButtonClick()
        {
            Onback.Invoke(); 
            Destroy(gameObject);  
        }

        public void OnPTButtonClick()
	    {   
		    English = false;
	    }

        public void OnENButtonClick()
        {
            English = true;
        }
    }
}
