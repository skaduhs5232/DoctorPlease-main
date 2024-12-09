
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public enum EState
{
	Start,
	Gameplay,
	End
}


public class LetterManager : MonoBehaviour
{

[SerializeField] private Button PTButton;
[SerializeField] private Button ENButton;
	public LetterGenerator generator;
	public Letter heldLetter;
	public EBinType hoveredBin = EBinType.None;
	public Slider slider;
	public Image deathWheel;

	public EState state { get; private set; } = EState.Start;

	float deathCount = 0;
	int failCount = 0;

	public GameObject cross1;
	public GameObject cross2;
	public GameObject cross3;
	public GameObject cross4;
	public GameObject cross5;

	const int failTotal = 5;
	const float deathTotal = 15;

	public Text scoreText;

	public static LetterManager instance = null;

	GameObject letterObject;

	public GameObject endScreen;
	public GameObject startScreen;
	public GameObject gameplayScreen;
	public Text finalScoreText;

	float letterSpawnCountdown = 10;
	bool initialSpawnCountdown = true;

	//Jornal variables
	List<int> playererrors = new List<int>();
	GameObject Jornal;
	GameObject JornalX;
	Button JornalXButton;
	int semana = 1;

	//Instruction note variables
	Vector3 pos;
	GameObject note;
	GameObject semiFade;
	GameObject ContinueCanvas;
	Button continueButton;


	float letterValueCount = 0;
	float totalScore = 0;

	bool spawnedFakeStampNote = false;
	bool spawnedWrongNameNote = false;
	bool spawnedWrongNISNote = false;
	bool readytocontinue = false;

	public int scorePennies { get; private set; } = 0;
	public int scorePounds { get; private set; } = 0;

	// Start is called before the first frame update
	void Start()
	{
		semana = 1;
		Screen.SetResolution(1920, 1080, true);
		if (instance == null)
			instance = this;
		else
			Destroy(this);

		letterObject = (GameObject)Resources.Load("Letter");
	}

	// Update is called once per frame
	void Update()
	{
		if (state == EState.Gameplay)
		{
			letterSpawnCountdown -= Time.deltaTime;

			//guarantee that no letter spawns for first 10 seconds after a note appearing
			//spawn a letter within 2 seconds if no other letters
			if (!initialSpawnCountdown && letterValueCount < .5f && letterSpawnCountdown > 2f || !readytocontinue)
			{
				letterSpawnCountdown = Random.Range(.25f, 2f);
			}
			else if (letterSpawnCountdown < 0)
			{
				initialSpawnCountdown = false;
				letterSpawnCountdown = Mathf.Max(0.25f, Random.Range(2.5f, 5.0f) - ((float)scorePennies + scorePounds * 100) / 45f);
				AddLetter();
			}

			/*if (Input.GetKeyDown(KeyCode.E))
			{
				Letter letterComponent = AddLetter();
				generator.Generate(letterComponent);
			}*/

			if (heldLetter && hoveredBin != EBinType.None)
			{
				heldLetter.Shrink();
			}
			else if (heldLetter && hoveredBin == EBinType.None)
			{
				heldLetter.Grow();
			}

			if (letterValueCount >= 19)
			{
				GetComponent<SoundManager>().StartClock();
				deathCount += Time.deltaTime;
				deathWheel.fillAmount = deathCount / deathTotal;
			}
			else if (deathCount >= 0)
			{

				deathCount -= Time.deltaTime;
				deathWheel.fillAmount = deathCount / deathTotal;
				GetComponent<SoundManager>().StopClock();
			}

			if (deathCount >= deathTotal || failCount >= failTotal)
			{
				state = EState.End;
				gameplayScreen.SetActive(false);
				Debug.Log("end");
				endScreen.SetActive(true);
				GetComponent<SoundManager>().StopClock();
				totalScore = (scorePounds + (scorePennies / 100.0f)) * 10;

				finalScoreText.text = "Credibilidade Final: " + totalScore.ToString("F2");
			}
		}
	}

	public void SetHeldLetter(Letter letter)
	{
		heldLetter = letter;
	}

	public void ReleaseHeldLetter(Letter letter)
	{
		if (state != EState.Gameplay)
			return;

		if (letter == heldLetter)
		{
			if (hoveredBin == EBinType.None)
				heldLetter = null;
			else
			{
				if (letter.isValid)
				{
					if (letter.deliveryType == EDeliveryType.FirstClass && hoveredBin == EBinType.First)
					{
						Debug.Log("succeed");
						SetScore(2);
						RemoveLetter(letter,true);
						return;
					}
					else if (letter.deliveryType == EDeliveryType.SecondClass && hoveredBin == EBinType.Second)
					{
						Debug.Log("succeed");
						SetScore(1);
						RemoveLetter(letter,true);
						return;
					}

					Fail();
					Debug.Log("fail");
					RemoveLetter(letter, false) ;
					return;
				}
				else
				{
					if (hoveredBin != EBinType.Discard)
					{
						Fail();
						Debug.Log("fail");
						RemoveLetter(letter , false);
						return;
					}
					else
					{
						SetScore(1);
						Debug.Log("succeed");
						RemoveLetter(letter , true);
					}
				}
			}
		}
		else
			Debug.LogWarning("Carta deu pau.");
	}

	public void SetHoveredBin(EBinType type)
	{
		hoveredBin = type;
	}

	public void RemoveHoveredBin(EBinType type)
	{
		if (hoveredBin == type)
			hoveredBin = EBinType.None;
		else
			Debug.LogWarning("Lixeira deu pau.");
	}

	async void SetScore(int delta)
	{
		GetComponent<SoundManager>().PlaySound(ESound.Correct);

		// Atualizando scorePounds e scorePennies
		scorePennies += delta;
		if (scorePennies >= 100)
		{
			scorePounds++;
			scorePennies -= 100;
		}

		// Criando a variável total que combina pounds e pennies, e multiplicando por 10
		totalScore = (scorePounds + (scorePennies / 100.0f)) * 10;

		// Exibindo o valor total multiplicado por 10 no scoreText
		scoreText.text = "Credibilidade: " + totalScore.ToString("F2");

		if (scorePennies >= 12 && !spawnedFakeStampNote)
		{
			Jornalgenerator(2);
		}
		if (scorePennies >= 30 && !spawnedWrongNameNote)
		{
			Jornalgenerator(3);
		}
		if (scorePennies >= 55 && !spawnedWrongNISNote)
		{
			Jornalgenerator(4);
		}
	}

	void AddLetter()
	{
		GetComponent<SoundManager>().PlaySound(ESound.New);
		GameObject instantiated = Instantiate(letterObject);
		Letter newletterr = instantiated.GetComponent<Letter>();
		generator.Generate(newletterr);
		letterValueCount += instantiated.GetComponent<Letter>().deliveryType == EDeliveryType.FirstClass ? 1.5f : 1;
		slider.value = letterValueCount;
	}

	void RemoveLetter(Letter letter , bool MissOrMake)
	{
		if(!MissOrMake)
		{
			playererrors.Add(letter.cardvalue);
		}

		letterValueCount -= letter.deliveryType == EDeliveryType.FirstClass ? 1.5f : 1;
		Destroy(letter.gameObject);
		slider.value = letterValueCount;
	}

	void Fail()
	{
		GetComponent<SoundManager>().PlaySound(ESound.Fail);

		failCount++;

		switch (failCount)
		{
			case 1:
				cross1.SetActive(true);
				break;
			case 2:
				cross2.SetActive(true);
				break;
			case 3:
				cross3.SetActive(true);
				break;
			case 4:
				cross4.SetActive(true);
				break;
			case 5:
				cross5.SetActive(true);
				break;
		}

	}

	public async void Reset()
	{
		cross1.SetActive(false);
		cross2.SetActive(false);
		cross3.SetActive(false);
		cross4.SetActive(false);
		cross5.SetActive(false);
		scorePennies = 0;
		scorePounds = 0;
		totalScore = (scorePounds + (scorePennies / 100.0f)) * 10;

		scoreText.text = "Credibilidade: " + totalScore.ToString("F2");
		spawnedFakeStampNote = false;
		spawnedWrongNameNote = false;
		spawnedWrongNISNote = false;


		letterValueCount = 0;

		deathCount = 0;
		failCount = 0;

		deathWheel.fillAmount = 0;
		slider.value = 0;

		foreach (Object draggable in FindObjectsOfType<Dragabble>())
		{
			Destroy(((Dragabble)draggable).gameObject);
		}

    
		generator.SetDifficulty(0);
		initialSpawnCountdown = true;
		letterSpawnCountdown = 10f;
		state = EState.Gameplay;
		gameplayScreen.SetActive(true);
		startScreen.SetActive(false);
		endScreen.SetActive(false);

		notegenerator(1);

	}

	public async void Jornalgenerator(int notenum)
	{	
		gameplayScreen.SetActive(false);
		if (semana == 1)
		{
		generator.SetDifficulty(semana);
		}
		else
		{
			generator.SetDifficulty(semana+1);
		}
		readytocontinue = false;
		semana += 1;
		GameObject fullfade = (GameObject)Instantiate(Resources.Load("FullFade"));
		FullFadeScript fadeScript = fullfade.GetComponent<FullFadeScript>();
		await Task.Delay(1300);
		GameObject WeekCanvas = (GameObject)Instantiate(Resources.Load("WeekCanvas"));
		Text Weektext = WeekCanvas.GetComponentInChildren<Text>();
		Text Weektextt = Weektext.GetComponent<Text>();
		Weektextt.text= "Semana " + semana.ToString();
		await Task.Delay(2500);
		Destroy(WeekCanvas);
		fadeScript.fadeOut=true;
		if(playererrors.Count==0)
		{
			//Lógica para a criação dos jornais bons
			Jornal = (GameObject)Instantiate(Resources.Load("Jornal-" + UnityEngine.Random.Range(7,11)));
		}
		else
		{
			int playererror = generator.PickRandomFromList(playererrors);
				switch (playererror)
				{
					case LetterGenerator.baseFakeValue:
						//Sem Selo
						Jornal = (GameObject)Instantiate(Resources.Load("Jornal-1"));
						Debug.Log(1);
						break;
					case LetterGenerator.baseFakeValue + 1:
						//Selo errado
						Jornal = (GameObject)Instantiate(Resources.Load("Jornal-2"));
						Debug.Log(2);
						break;
					case LetterGenerator.baseFakeValue + 2:
						//Nome errado e Sobrenome Errado
						Jornal = (GameObject)Instantiate(Resources.Load("Jornal-3"));
						Debug.Log(3);
						break;
					case LetterGenerator.baseFakeValue + 3:
						//Doença errada
						Jornal = (GameObject)Instantiate(Resources.Load("Jornal-4"));
						Debug.Log(4);
						break;
					case LetterGenerator.baseFakeValue + 4:
						//Nis errado
						Jornal = (GameObject)Instantiate(Resources.Load("Jornal-5"));
						Debug.Log(5);	
						break;
					default:
						// Apagado errado
						Jornal = (GameObject)Instantiate(Resources.Load("Jornal-6"));
						Debug.Log(6);
						break;
			}
		}
			Jornal.transform.position = new Vector3(0,0,-9);
			GameObject JornalX = (GameObject)Instantiate(Resources.Load("JornalX"));
			Button JornalXButton = JornalX.GetComponentInChildren<Button>();

			JornalXButton.onClick.AddListener(() => OnJornalXButtonClick(Jornal,JornalX,notenum));

			JornalXButton.transform.position = new Vector3(1810.7f,992.5f,0);
			playererrors = new List<int>();
	}


	public void OnJornalXButtonClick(GameObject jornal, GameObject JornalX , int notenum)
	{
		gameplayScreen.SetActive(true);
		Destroy(JornalX);
		Destroy(jornal);
		notegenerator(notenum);
	}


	public async void notegenerator(int notenum)
	{	
		if (!generator.English)
		{
			switch(notenum)
			{
				case 1:
					note = (GameObject)Instantiate(Resources.Load("NoteStart"));
					break;
				case 2:
					note = (GameObject)Instantiate(Resources.Load("NoteFakeStamp"));
					spawnedFakeStampNote=true;
					break;
				case 3:
					note = (GameObject)Instantiate(Resources.Load("NoteWrongName"));
					spawnedWrongNameNote = true;
					break;
				case 4:
					note = (GameObject)Instantiate(Resources.Load("NoteWrongNIS"));
					spawnedWrongNISNote = true;
					break;
			}
		}
		else
		{
			switch(notenum)
			{
				case 1:
					note = (GameObject)Instantiate(Resources.Load("NoteStartEN"));
					break;
				case 2:
					note = (GameObject)Instantiate(Resources.Load("NoteFakeStampEN"));
					spawnedFakeStampNote=true;
					break;
				case 3:
					note = (GameObject)Instantiate(Resources.Load("NoteWrongNameEN"));
					spawnedWrongNameNote = true;
					break;
				case 4:
					note = (GameObject)Instantiate(Resources.Load("NoteWrongNISEN"));
					spawnedWrongNISNote = true;
					break;
			}
		}
			GetComponent<SoundManager>().PlaySound(ESound.New);
			semiFade = (GameObject)Instantiate(Resources.Load("SemiFade"));
			SpriteRenderer noteRenderer = note.GetComponent<SpriteRenderer>();
			pos = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-2.5f, 2.5f), -9.0f);
			note.transform.position = pos;


			await Task.Delay(700);
			ContinueCanvas = (GameObject)Instantiate(Resources.Load("ContinueCanvas"));
			continueButton = ContinueCanvas.GetComponentInChildren<Button>();
			continueButton.onClick.AddListener(() => OnContinueButtonClick(continueButton, semiFade));

			continueButton.GetComponent<ButtonFollowLetter>().SetLetterTransform(note.transform);
	}
	
	public void OnContinueButtonClick(Button continueButton, GameObject semiFade)
	{
		readytocontinue = true;
		Destroy(continueButton.gameObject);
		Destroy(semiFade);
	}
	
	public void OnLeaderboardButtonClick()
	{
		endScreen.SetActive(false);
		startScreen.SetActive(false);
		GameObject Leaderboardobj = (GameObject)Instantiate(Resources.Load("Leaderboard"));
		Transform scoreTransform = Leaderboardobj.transform.Find("Current score num");
    	Text scoreText = scoreTransform.GetComponent<Text>();
    	scoreText.text = (totalScore*10).ToString();

	}

	public void ShowStartScreen()
    {
        startScreen.SetActive(true); // Activate the start screen
    }


public void OnPTButtonClick()
{
    generator.English = false;
    // Get the Image component of the buttons and change their color
    PTButton.GetComponent<Image>().color = new Color32(70, 70, 70, 255);  
    ENButton.GetComponent<Image>().color = new Color32(173, 173, 173, 255);  
}

public void OnENButtonClick()
{
    generator.English = true;
    // Get the Image component of the buttons and change their color
    ENButton.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
    PTButton.GetComponent<Image>().color = new Color32(173, 173, 173, 255);
}	
	
}
