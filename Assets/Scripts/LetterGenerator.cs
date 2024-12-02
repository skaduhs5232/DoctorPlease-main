using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LetterGenerator : MonoBehaviour
{

string firstName;
string surname;
Tuple<string, List<string>> diseaseSymptom;
string disease;
string symptom;
string NISS;


public bool English = false;

List<string> firstNames = new List<string>{ "Joao", "Jessica", "Barbara", "Tomas", "Dafne",
											"Jaime", "Sara", "Bruno", "Davi", "Jefferson", "Ana", "Lidia",
											"Micaela", "Ana", "Lourenco", "Olavo", "Sofia", "Luiz",
											"Moises", "Estevao", "Henrique", "Rebeca", "Sebastiao", "Simone",
											"Miguel", "Angelica", "Marcos", "Allan", "Ricardo", "Helena", "Daniel",
											"Fernanda", "Joaquim", "Lorena" };

List<string> surnames = new List<string>{ "Pereira", "Silva", "Lima", "Moraes", "Sousa", "Rodrigues", "Oliveira",
											"Martins", "Almeida", "Barbosa", "Gomes", "Santos", "Coelho", "Carvalho",
											"Souza", "Costa", "Schmidt", "Santos", "Muniz", "Fernandes", "Nascimento", "Maciel",
											"Camargo", "Araujo", "Ribeiro", "Batista", "Barros", "Teixeira", "Muller", "Fischer", "Schneider",
											"Carvalho", "Macedo", "Ferreira", "Borges", "Jaeger", "Ziegler" };

    List<Tuple<string, List<string>>> diseaseSymptomPairs = new List<Tuple<string, List<string>>>
    {
        new Tuple<string, List<string>>("Enxaqueca", new List<string> { "Dor de cabeca", "Nausea", "Sensibilidade a luz" }),
        new Tuple<string, List<string>>("Gripe", new List<string> { "Febre", "Tosse", "Dor no corpo", "Fadiga" }), 
        new Tuple<string, List<string>>("Bronquite", new List<string> { "Tosse", "Dificuldade para respirar", "Chiado no peito" }), 
        new Tuple<string, List<string>>("Anemia", new List<string> { "Fadiga", "Palidez", "Falta de ar" }),
        new Tuple<string, List<string>>("Fibromialgia", new List<string> { "Dor muscular", "Cansaco extremo", "Disturbios do sono" }),
        new Tuple<string, List<string>>("Malaria", new List<string> { "Calafrios", "Febre alta", "Suores intensos" }),
        new Tuple<string, List<string>>("Asma", new List<string> { "Dificuldade para respirar", "Chiado no peito", "Tosse", "Falta de ar" }),
        new Tuple<string, List<string>>("Angina", new List<string> { "Dor no peito", "Falta de ar", "Suor frio" }),
        new Tuple<string, List<string>>("Gastrite", new List<string> { "Dor abdominal", "Nausea", "Azia" }),
        new Tuple<string, List<string>>("Vomito agudo", new List<string> { "Vomito", "Nausea", "Dor abdominal" }), 
        new Tuple<string, List<string>>("Gastroenterite", new List<string> { "Diarreia", "Colicas", "Nausea" }),
        new Tuple<string, List<string>>("Hepatite", new List<string> { "Perda de apetite", "Ictericia", "Dor abdominal" }),
        new Tuple<string, List<string>>("Dermatite", new List<string> { "Erupcao cutanea", "Coceira", "Vermelhidao" }), 
        new Tuple<string, List<string>>("Sindromeda fadiga cronica", new List<string> { "Fadiga", "Dor muscular" }),
        new Tuple<string, List<string>>("Tuberculose", new List<string> { "Tosse", "Suores noturnos", "Perda de peso", "Falta de apetite" }),
        new Tuple<string, List<string>>("Sinusite", new List<string> { "Congestao nasal", "Dor facial", "Dor de cabeca" }),  
        new Tuple<string, List<string>>("Apendicite", new List<string> { "Dor abdominal", "Nausea", "Febre" }),  
        new Tuple<string, List<string>>("Artrite", new List<string> { "Dores nas articulacoes", "Inchaco nas articulacoes", "Dificuldade de movimento" }), 
        new Tuple<string, List<string>>("Vertigem", new List<string> { "Tontura", "Sensacao de desequilibrio", "Nausea" }), 
        new Tuple<string, List<string>>("COVID-19", new List<string> { "Falta de ar", "Febre", "Tosse", "Fadiga", "Perda de olfato" }),
        new Tuple<string, List<string>>("Conjuntivite", new List<string> { "Olhos vermelhos", "Coceira nos olhos", "Secrecao ocular" }), 
        new Tuple<string, List<string>>("Faringite", new List<string> { "Dor de garganta", "Dificuldade para engolir", "Febre" }),
        new Tuple<string, List<string>>("Pneumonia", new List<string> { "Dificuldade para respirar", "Tosse com secrecao", "Febre alta" }),
        new Tuple<string, List<string>>("Hipotireoidismo", new List<string> { "Cansaco", "Ganho de peso", "Sensibilidade ao frio" }),
        new Tuple<string, List<string>>("Diabetes Tipo 2", new List<string> { "Sede excessiva", "Fome constante", "Cansaco", "Visao turva" }),
        new Tuple<string, List<string>>("Acidente Vascular Cerebral (AVC)", new List<string> { "Fraqueza em um lado do corpo", "Dificuldade para falar", "Perda de equilibrio" }),
        new Tuple<string, List<string>>("Hernia de disco", new List<string> { "Dor nas costas", "Formigamento nas pernas", "Dificuldade de movimento" }),
		 new Tuple<string, List<string>>("Doenca de Alzheimer", new List<string> { "Perda de memoria", "Confusao mental", "Dificuldade para falar" }),
        new Tuple<string, List<string>>("Doenca de Parkinson", new List<string> { "Tremores", "Rigidez muscular", "Dificuldade de movimento" })
    };

	List<string> firstNamesEN = new List<string>
{
    "James", "Emily", "Michael", "Sarah", "Jessica", "John", "Olivia", "David", "Sophia", "Joshua",
    "Isabella", "Matthew", "Ava", "William", "Madison", "Benjamin", "Charlotte", "Samuel", "Grace",
    "Daniel", "Zoe", "Ethan", "Chloe", "Alexander", "Lily", "Jacob", "Amelia", "Henry", "Megan",
    "Lucas", "Evelyn", "Jack", "Ella", "Ryan", "Victoria"
};

List<string> surnamesEN = new List<string>
{
    "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez",
    "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore",
    "Jackson", "Martin", "Lee", "Perez", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis",
    "Roberts", "Walker", "Young", "Allen", "King", "Scott"
};

List<Tuple<string, List<string>>> diseaseSymptomPairsEN = new List<Tuple<string, List<string>>>
{
    new Tuple<string, List<string>>("Migraine", new List<string> { "Headache", "Nausea", "Light sensitivity" }),
    new Tuple<string, List<string>>("Flu", new List<string> { "Fever", "Cough", "Body aches", "Fatigue" }),
    new Tuple<string, List<string>>("Bronchitis", new List<string> { "Cough", "Shortness of breath", "Wheezing" }),
    new Tuple<string, List<string>>("Anemia", new List<string> { "Fatigue", "Paleness", "Shortness of breath" }),
    new Tuple<string, List<string>>("Fibromyalgia", new List<string> { "Muscle pain", "Extreme fatigue", "Sleep disturbances" }),
    new Tuple<string, List<string>>("Malaria", new List<string> { "Chills", "High fever", "Severe sweating" }),
    new Tuple<string, List<string>>("Asthma", new List<string> { "Shortness of breath", "Wheezing", "Cough", "Difficulty breathing" }),
    new Tuple<string, List<string>>("Angina", new List<string> { "Chest pain", "Shortness of breath", "Cold sweat" }),
    new Tuple<string, List<string>>("Gastritis", new List<string> { "Abdominal pain", "Nausea", "Heartburn" }),
    new Tuple<string, List<string>>("Acute Vomiting", new List<string> { "Vomiting", "Nausea", "Abdominal pain" }),
    new Tuple<string, List<string>>("Gastroenteritis", new List<string> { "Diarrhea", "Cramping", "Nausea" }),
    new Tuple<string, List<string>>("Hepatitis", new List<string> { "Loss of appetite", "Jaundice", "Abdominal pain" }),
    new Tuple<string, List<string>>("Dermatitis", new List<string> { "Skin rash", "Itching", "Redness" }),
    new Tuple<string, List<string>>("Chronic Fatigue Syndrome", new List<string> { "Fatigue", "Muscle pain" }),
    new Tuple<string, List<string>>("Tuberculosis", new List<string> { "Cough", "Night sweats", "Weight loss", "Loss of appetite" }),
    new Tuple<string, List<string>>("Sinusitis", new List<string> { "Nasal congestion", "Facial pain", "Headache" }),
    new Tuple<string, List<string>>("Appendicitis", new List<string> { "Abdominal pain", "Nausea", "Fever" }),
    new Tuple<string, List<string>>("Arthritis", new List<string> { "Joint pain", "Swelling in joints", "Difficulty moving" }),
    new Tuple<string, List<string>>("Vertigo", new List<string> { "Dizziness", "Feeling of imbalance", "Nausea" }),
    new Tuple<string, List<string>>("COVID-19", new List<string> { "Shortness of breath", "Fever", "Cough", "Fatigue", "Loss of smell" }),
    new Tuple<string, List<string>>("Conjunctivitis", new List<string> { "Red eyes", "Itching in the eyes", "Eye discharge" }),
    new Tuple<string, List<string>>("Pharyngitis", new List<string> { "Sore throat", "Difficulty swallowing", "Fever" }),
    new Tuple<string, List<string>>("Pneumonia", new List<string> { "Difficulty breathing", "Cough with phlegm", "High fever" }),
    new Tuple<string, List<string>>("Hypothyroidism", new List<string> { "Fatigue", "Weight gain", "Sensitivity to cold" }),
    new Tuple<string, List<string>>("Type 2 Diabetes", new List<string> { "Excessive thirst", "Constant hunger", "Fatigue", "Blurred vision" }),
    new Tuple<string, List<string>>("Stroke", new List<string> { "Weakness on one side of the body", "Difficulty speaking", "Loss of balance" }),
    new Tuple<string, List<string>>("Herniated Disc", new List<string> { "Back pain", "Numbness in the legs", "Difficulty moving" }),
    new Tuple<string, List<string>>("Alzheimer's Disease", new List<string> { "Memory loss", "Mental confusion", "Difficulty speaking" }),
    new Tuple<string, List<string>>("Parkinson's Disease", new List<string> { "Tremors", "Muscle stiffness", "Difficulty moving" })
};

List<string> NIS = new List<string>{"277056876540008","287120984760002", "296345178320004","314983645870006","327109283760003","339471087520001","341985672430007","357634178900008","369172087540004","374120938760003",  "382765478340006",  "395083761420002","404398572610009",
		"416987342580004",  "429017364570003","436789234570002","445987102830006","457432198620001",  "465789234670007","479013847260008",
		"485172936480003","497603184570002","508793617450004","512073984620001","523098741620006","537461029340002","543897612340009",
		"555671294380006","564839127450003","572098134580001","587612349870004","595871034670002","602198736450006","617409283650008","629874012360004","637198572430002","648792034870005","655401238960003"
};


	List<string> fakeNIS = new List<string> {
    "49760318 :) :D ;) 570002",
    "834726 :P :O XD 192834",
    "293847 ;) :/ :( 2837",
    "120398 :D :) :* 9384",
    "674920 XD ;) :P 3872",
    "902834 :O :/ :) 1234",
    "876234 :/ :P :S 0987",
    "432978 :( :D :) 1265",
    "849320 :| :O :P 0921",
    "283746 :S XD ;( 2384",
    "548392 :* :) :O 8765",
    "749284 <3 :P ;) 4387",
    "123984 :X :) :D 9834",
    "987234 >:( ;) XD 1257",
    "876239 :P :O :) 0982",
    "432908 ^_^ XD :D 4523"
};


	int difficulty = 0;
	public float z = 0;

	public const int baseFakeValue = 6;
	public int value = 0;


	public Letter createdletter;

	public void SetDifficulty(int newDifficulty)
	{
		difficulty = newDifficulty;
	}

	public void Generate(Letter letterComponent)
	{
		if(!English)
		{
		firstName = PickRandomFromList(firstNames) + " ";
		surname = PickRandomFromList(surnames);
 		diseaseSymptom = PickRandomFromList(diseaseSymptomPairs);
		}
		else
		{ 
		firstName = PickRandomFromList(firstNamesEN) + " ";
		surname = PickRandomFromList(surnamesEN);
 		diseaseSymptom = PickRandomFromList(diseaseSymptomPairsEN);
		}
        disease = diseaseSymptom.Item1;
		symptom = PickRandomFromList(diseaseSymptom.Item2);
		NISS = PickRandomFromList(NIS);

		EDeliveryType type = UnityEngine.Random.Range(0f, 1f) >= .65f ? EDeliveryType.FirstClass : EDeliveryType.SecondClass;

		bool isLetterValid = true;

		value = UnityEngine.Random.Range(1, baseFakeValue + 1 + difficulty);

		//first couple letters guaranteed to be right
		if (LetterManager.instance.scorePennies < 3 && LetterManager.instance.scorePounds < 1)
			value = 0;

		//generate incorrect letter
		if (value >= baseFakeValue)
		{
			isLetterValid = false;
		
			switch (value)
			{
				case baseFakeValue:
					//no stamp
					type = EDeliveryType.Missing;
					break;
				case baseFakeValue + 1:
					//fake stamp
					type = UnityEngine.Random.Range(0f, 1f) >= .5f ? EDeliveryType.FakeFirstClass : EDeliveryType.FakeSecondClass;
					break;
				case baseFakeValue + 2:
					//wrong name
					{
						int roll = UnityEngine.Random.Range(0, 2);
						if (roll == 0)
							firstName = "";
						else
							surname = "";
						break;
					}
				case baseFakeValue + 3:
					//wrong diseases
					{
						int roll = UnityEngine.Random.Range(0, 2);
						if (roll == 0)
							disease = "";
						else
							symptom = "";
						break;
					}
				case baseFakeValue + 4:
					//fake NIS
					NISS = PickRandomFromList(fakeNIS);
					break;
			}

		}

		string wholetext = firstName + surname + "\n" + disease + "\n" + symptom + "\n" + NISS;
		letterComponent.Initialise(wholetext, isLetterValid, type, z , value);
		createdletter = letterComponent;
		z += .2f;
		if (z > 8.5)
			z = 0;
	}

	internal T PickRandomFromList<T>(List<T> list)
	{
		int index = Mathf.RoundToInt(UnityEngine.Random.Range(0, list.Count));

		return list[index];
	}
}
