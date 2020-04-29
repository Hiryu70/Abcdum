using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;

namespace Abcdum
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string _trueWord;
		private string _wrongPhrase;
		private string _congratulationPhrase;
		private SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

		public MainWindow()
		{
			InitializeComponent();
			_synthesizer.SelectVoice("Microsoft Server Speech Text to Speech Voice (ru-RU, Elena)");
			_synthesizer.SpeakAsync("Привет, меня зовут Кукуся. Я младшая сестра Маруси,. Буду учить тебя читать. Буду загадывать попробуй угадать. Первое слово: ");
			ButtonBase_OnClick(new Button(), new RoutedEventArgs());
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			var button = (Button) sender;

			if (button.Content == _trueWord)
			{
				if (!string.IsNullOrEmpty(_trueWord))
				{
					var congratulations = new List<string> { "правильно",
						"Ух ты, хорошо",
						"правильно, похоже, что ты умнее чем кот",
						"молодец, я тобой горжусь",
						"хорошая работа",
						"неправильно, шучу, правильно. ха ха",
						"поразительный результат",
						"угадала" };
					congratulations.Remove(_congratulationPhrase);
					var randomTrue = new Random();
					var congratulationPhrase = congratulations[randomTrue.Next(congratulations.Count)];
					_congratulationPhrase = congratulationPhrase;
					_synthesizer.SpeakAsync(congratulationPhrase);
				}

				var words = new List<string> { "кот", "собака", "утка", "мама", "папа", "баба", "Мелисса" };
				var random = new Random();
				var trueNumber = random.Next(1, 4);

				for (int i = 1; i < 5; i++)
				{
					var word = words[random.Next(words.Count)];
					switch (i)
					{
						case 1: A.Content = word; break;
						case 2: B.Content = word; break;
						case 3: C.Content = word; break;
						case 4: D.Content = word; break;
					}

					if (trueNumber == i)
					{
						_trueWord = word;
					}

					words.Remove(word);
				}

				_synthesizer.SpeakAsync(_trueWord);
			}
			else
			{
				var wrong = new List<string> { "неправильно", 
					"Мелиса ты же должна знать это слово", 
					$"это что? по твоему {_trueWord}?", 
					$"не уверена, что это {_trueWord}", 
					"не угадала", 
					"хм, думаю нет",
					"не правильно, Мелиса - соси са" };
				wrong.Remove(_wrongPhrase);
				var random = new Random();
				var wrongPhrase = wrong[random.Next(wrong.Count)];
				_wrongPhrase = wrongPhrase;

				_synthesizer.SpeakAsync(wrongPhrase);
			}
		}
	}
}
