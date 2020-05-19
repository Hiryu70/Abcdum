using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace Abcdum
{
	public partial class MainWindow : MetroWindow
	{
		private string _fileName = "words.txt";
		private string _trueWord;
		private string _wrongPhrase;
		private string _congratulationPhrase;
		private readonly SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

		public MainWindow()
		{
			InitializeComponent();
			_synthesizer.SelectVoice("Microsoft Server Speech Text to Speech Voice (ru-RU, Elena)");
			_synthesizer.SpeakAsync("Привет, меня зовут Кукуся. Первое слово: ");
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
						"неправильно, шучу, правильно",
						"поразительный результат",
						"угадала" };
					congratulations.Remove(_congratulationPhrase);
					var randomTrue = new Random();
					var congratulationPhrase = congratulations[randomTrue.Next(congratulations.Count)];
					_congratulationPhrase = congratulationPhrase;
					_synthesizer.SpeakAsync(congratulationPhrase);
				}

				var rowsCount = File.ReadLines(_fileName).Count();
				var random = new Random();

				var row = random.Next(0, rowsCount);

				string line = File.ReadLines(_fileName).Skip(row).Take(1).First();

				var words = line.Split(',').ToList();
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
					"хм, думаю нет" };
				wrong.Remove(_wrongPhrase);
				var random = new Random();
				var wrongPhrase = wrong[random.Next(wrong.Count)];
				_wrongPhrase = wrongPhrase;

				_synthesizer.SpeakAsync(wrongPhrase);
			}
		}

		private void Repeat_OnClick(object sender, RoutedEventArgs e)
		{
			_synthesizer.SpeakAsync(_trueWord);
		}
	}
}
