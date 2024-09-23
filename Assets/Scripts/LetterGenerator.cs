using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterGenerator : MonoBehaviour
{
	List<string> firstNames = new List<string>{ "João", "Jéssica", "Bárbara", "Tomás", "Dafne",
											"Jaime", "Sara", "Bruno", "Davi", "Jefferson", "Ana", "Lídia",
											"Micaela", "Ana", "Lourenço", "Olavo", "Sofia", "Luiz",
											"Moisés", "Estêvão", "Henrique", "Rebeca", "Sebastião", "Simone",
											"Miguel", "Angélica", "Marcos", "Allan", "Ricardo", "Helena", "Daniel",
											"Fernanda", "Joaquim", "Lorena" };

	List<string> surnames = new List<string>{ "Pereira", "Silva", "Lima", "Moraes", "Sousa", "Rodrigues", "Oliveira",
											"Martins", "Almeida", "Barbosa", "Gomes", "Santos", "Coelho", "Carvalho",
											"Souza", "Costa", "Schmidt", "Santos", "Muniz", "Fernandes", "Nascimento", "Maciel",
											"Camargo", "Araújo", "Ribeiro", "Batista", "Barros", "Teixeira", "Müller", "Fischer", "Schneider",
											"Carvalho", "Macedo", "Ferreira", "Borges", "Jaeger", "Ziegler" };


	List<string> streetLastWords = new List<string> { "Dor de cabeça", "Febre", "Tosse", "Fadiga", "Dor muscular", "Calafrios", "Dificuldade para respirar", "Dor no peito",
													"Náusea", "Vômito", "Diarreia", "Perda de apetite", "Erupção cutânea", "Cansaço excessivo", "Suores noturnos", "Congestão nasal",
													"Dor abdominal", "Dores nas articulações", "Tontura", "Perda de olfato", "Confusão mental", "Olhos vermelhos", "Falta de ar", "Dor de garganta" };


	List<string> streetFirstWords = new List<string> { "Enxaqueca e :", "Gripe e :", "Bronquite e :", "Anemia e :", "Fibromialgia e :", "Malária e :", "Asma e :", "Angina e :",
													"Gastrite e :", "Vômito agudo e :", "Gastroenterite e :", "Hepatite e :", "Dermatite e :", "Síndrome da fadiga crônica e :", "Tuberculose e :",
													"Sinusite e :", "Apendicite e :", "Artrite e :", "Vertigem e :", "COVID-19 e :", "Demência e :", "Conjuntivite e :", "Doença pulmonar obstrutiva crônica (DPOC) e :", "Faringite" };

	List<string> cities = new List<string>{"277056876540008","287120984760002", "296345178320004","314983645870006","327109283760003","339471087520001","341985672430007","357634178900008","369172087540004","374120938760003",  "382765478340006",  "395083761420002","404398572610009",
		"416987342580004",  "429017364570003","436789234570002","445987102830006","457432198620001",  "465789234670007","479013847260008",
		"485172936480003","497603184570002","508793617450004","512073984620001","523098741620006","537461029340002","543897612340009",
		"555671294380006","564839127450003","572098134580001","587612349870004","595871034670002","602198736450006","617409283650008","629874012360004","637198572430002","648792034870005","655401238960003"
};


	List<string> fakeCities = new List<string> {
		"49760318( ͡° ͜ʖ ͡°)570002",  "834726😊192834", "293847👾2837",  "120398👻9384", "674920💡3872",  "902834🌟1234", "876234👀0987",
		"432978😂1265",  "849320🔥0921",  "283746🤖2384",  "548392👽8765", "749284🎮4387", "123984🦄9834",  "987234🤡1257", "876239🚀0982", "432908🐱4523"
};

	int difficulty = 0;
	public float z = 0;

	const int baseFakeValue = 6;

	public void SetDifficulty(int newDifficulty)
	{
		difficulty = newDifficulty;
	}

	public void Generate(Letter letterComponent)
	{
		string firstName = PickRandomFromList(firstNames) + " ";
		string surname = PickRandomFromList(surnames);
		string houseNumber = Mathf.RoundToInt(Random.Range(1, 420)).ToString() + " ";
		string streetFirstWord = PickRandomFromList(streetFirstWords) + " ";
		string streetLastWord = PickRandomFromList(streetLastWords);
		string city = PickRandomFromList(cities);

		EDeliveryType type = Random.Range(0f, 1f) >= .65f ? EDeliveryType.FirstClass : EDeliveryType.SecondClass;

		bool isLetterValid = true;

		int value = Random.Range(1, baseFakeValue + 1 + difficulty);

		//first couple letters guaranteed to be right
		if (LetterManager.instance.scorePennies < 3 && LetterManager.instance.scorePounds < 1)
			value = 0;

		//10 percent chance of an evil letter if the letter is incorrect, overall 1/100 letters are evil
		int evilLetter = Mathf.RoundToInt(Random.Range(1, 10));

		//generate incorrect letter
		if (value >= baseFakeValue)
		{
			isLetterValid = false;

			//if (evilLetter == 1)
			//{
			//letter = MakeEvilLetter();
			//return;
			//}

			switch (value)
			{
				case baseFakeValue:
					//no stamp
					type = EDeliveryType.Missing;
					break;
				case baseFakeValue + 1:
					type = Random.Range(0f, 1f) >= .5f ? EDeliveryType.FakeFirstClass : EDeliveryType.FakeSecondClass;
					//todo
					break;
				case baseFakeValue + 2:
					//wrong name
					{
						int roll = Random.Range(0, 2);
						if (roll == 0)
							firstName = "";
						else
							surname = "";
						break;
					}
				case baseFakeValue + 3:
					//wrong address
					{
						int roll = Random.Range(0, 3);
						if (roll == 0)
							houseNumber = "";
						else if (roll == 1)
							streetFirstWord = "";
						else
							streetLastWord = "";
						break;
					}
				case baseFakeValue + 4:
					//fake city
					city = PickRandomFromList(fakeCities);
					break;
			}

		}

		string address = firstName + surname + "\n" + houseNumber + streetFirstWord + streetLastWord + "\n" + city;
		letterComponent.Initialise(address, isLetterValid, type, z);
		z += .2f;
		if (z > 8.5)
			z = 0;
	}

	//Letter MakeEvilLetter()
	//{
	//	return new Letter();
	//}

	string PickRandomFromList(List<string> list)
	{
		int index = Mathf.RoundToInt(Random.Range(0, list.Count));

		return list[index];
	}
}
