using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanGame.Utilities
{
	public class WordsDictionary
	{
		private Dictionary<string, string> _dict;
		private HashSet<string> _usedKeys;
		private string? _lastKey;

		private static readonly string _filePath = "dict.txt";

		public WordsDictionary()
		{
			if (!File.Exists(_filePath))
			{
				throw new FileNotFoundException("File not found", _filePath);
			}

			_dict = InitializeDictionaryFromFile();
			_usedKeys = new HashSet<string>();
		}

		public KeyValuePair<string, string> GetRandomWord()
		{
			if (_usedKeys.Count == _dict.Count)
			{
				ResetUsedWords();
			}

			var unusedWords = _dict.Where(pair => !_usedKeys.Contains(pair.Key)).ToList();

			var value = unusedWords.ElementAt(new Random().Next(_dict.Count));
			_lastKey = value.Key;

			return value;
		}

		public void ResetUsedWords()
		{
			_usedKeys.Clear();
		}

		public bool IsAllWordsUsed()
		{
			return _usedKeys.Count == _dict.Count;
		}

		public void MarkWordAsUsed()
		{
			if (_lastKey == null)
			{
				return;
			}

			_usedKeys.Add(_lastKey);
		}

		private Dictionary<string, string> InitializeDictionaryFromFile()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();

			using StreamReader reader = new StreamReader(_filePath);
			while (reader.ReadLine() is { } line)
			{
				var parts = line.Split('|');
				if (parts.Length == 2)
				{
					dictionary.Add(parts[0], parts[1]);
				}
				else
				{
					throw new FormatException("Invalid file format. Must be: \"word|hint\"");
				}
			}

			return dictionary;
		}
	}
}
