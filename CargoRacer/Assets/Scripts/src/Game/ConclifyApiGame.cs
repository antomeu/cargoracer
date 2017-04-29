// 
// ConclifyApiGame.cs
//  
// Contains the class to represent the game
// 
// Author:		Jon Massey
// Copyright:   DSA Practice 2017
// Company:     DSA Practice
// Since:		29/04/2017
// ================================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Conclify.Game
{
	/// <summary>
	/// Class to represent the game
	/// </summary>
	[Serializable]
	public class ConclifyApiGame
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Path to the save file for the game data</summary>
		private const string FilePathFormat = "{0}/conclify.game";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Collection of scores for the game</summary>
		private List<ConclifyApiGameScore> scores = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Collection of scores for the game</summary>
		public IEnumerable<ConclifyApiGameScore> Scores
		{ get { return scores; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiGame</code> instance
		/// </summary>
		internal ConclifyApiGame()
		{
			//Create Collections
			scores = new List<ConclifyApiGameScore>();
		}

		//Score Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Clears all scores
		/// </summary>
		internal void ClearScores()
		{
			//Clear Scores Collection
			scores.Clear();
		}

		/// <summary>
		/// Adds a new score to the game
		/// </summary>
		/// <param name="rank">Rank to display for the score</param>
		/// <param name="name">Name to display for the score</param>
		/// <param name="score">Score display value</param>
		internal void AddScore(string rank, string name, string score)
		{
			//Add New Score
			scores.Add(new ConclifyApiGameScore(rank, name, score));
		}

		//Load and Save Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Loads a <code>ConclifyApiGame</code> instance from persistent storage
		/// </summary>
		/// <returns>The loaded <code>ConclifyApiGame</code>, or <code>null</code> if an error occurred</returns>
		internal static ConclifyApiGame Load()
		{
			//Declare File Path
			string filePath = string.Format(FilePathFormat, Application.persistentDataPath);

			//Check for File
			if(!File.Exists(filePath))
				return null;

			try
			{
				//Create Formatter
				BinaryFormatter binaryFormatter = new BinaryFormatter();

				//Open File
				FileStream file = File.Open(filePath, FileMode.Open);

				//Deserialise Game
				ConclifyApiGame game = (ConclifyApiGame)binaryFormatter.Deserialize(file);

				//Close File
				file.Close();

				//Return Loaded Game
				return game;
			}
			catch(Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Saves the <code>ConclifyApiGame</code> instance to persistent storage
		/// </summary>
		public void Save()
		{
			//Declare File Path
			string filePath = string.Format(FilePathFormat, Application.persistentDataPath);

			//Create Formatter
			BinaryFormatter binaryFormatter = new BinaryFormatter();

			try
			{
				//Open File
				FileStream file = File.Create(filePath);

				//Serialise Player
				binaryFormatter.Serialize(file, this);

				//Close File
				file.Close();
			}
			catch(Exception)
			{ }
		}
	}
}
// ================================================================================================