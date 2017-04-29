// 
// ConclifyApiPlayer.cs
//  
// Contains the class to represent the player of the game
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Conclify.Player
{
	/// <summary>
	/// Class to represent the player of the game
	/// </summary>
	[Serializable]
	public class ConclifyApiPlayer
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Path to the save file for the player data</summary>
		private const string FilePathFormat = "{0}/conclify.player";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Identifier of the player</summary>
		private string id = null;

		/// <summary>First name of the player</summary>
		private string firstName = null;

		/// <summary>Last name of the player</summary>
		private string lastName = null;

		/// <summary>Email address of the player</summary>
		private string emailAddress = null;

		/// <summary>Country of the player</summary>
		private string country = null;

		/// <summary>Platform that the player uses to play the game</summary>
		private string platform = null;

		/// <summary>Identifier of the player's device</summary>
		private string deviceId = null;

		/// <summary>Tokens earned by the player</summary>
		private int tokens = 0;

		/// <summary>Number of tokens the player requires to reach the next threshold</summary>
		private int tokensToThreshold = 0;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Identifier of the player</summary>
		public string Id
		{
			get { return id; }
			internal set { id = value; }
		}

		/// <summary>First name of the player</summary>
		public string FirstName
		{
			get { return firstName; }
			internal set { firstName = value; }
		}

		/// <summary>Last name of the player</summary>
		public string LastName
		{
			get { return lastName; }
			internal set { lastName = value; }
		}

		/// <summary>Email address of the player</summary>
		public string EmailAddress
		{
			get { return emailAddress; }
			internal set { emailAddress = value; }
		}

		/// <summary>Country of the player</summary>
		public string Country
		{
			get { return country; }
			internal set { country = value; }
		}

		/// <summary>Platform that the player uses to play the game</summary>
		public string Platform
		{
			get { return platform; }
			internal set { platform = value; }
		}

		/// <summary>Identifier of the player's device</summary>
		public string DeviceId
		{
			get { return deviceId; }
			internal set { deviceId = value; }
		}

		/// <summary>Tokens earned by the player</summary>
		public int Tokens
		{
			get { return tokens; }
			internal set { tokens = value; }
		}

		/// <summary>Number of tokens the player requires to reach the next threshold</summary>
		public int TokensToThreshold
		{
			get { return tokensToThreshold; }
			internal set { tokensToThreshold = value; }
		}

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiPlayer</code> instance
		/// </summary>
		internal ConclifyApiPlayer()
		{
			/* Default Constructor */
		}

		//Load and Save Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Loads a <code>ConclifyApiPlayer</code> instance from persistent storage
		/// </summary>
		/// <returns>The loaded <code>ConclifyApiPlayer</code>, or <code>null</code> if an error occurred</returns>
		internal static ConclifyApiPlayer Load()
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

				//Deserialise Player
				ConclifyApiPlayer player = (ConclifyApiPlayer)binaryFormatter.Deserialize(file);

				//Close File
				file.Close();

				//Return Loaded Player
				return player;
			}
			catch(Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Saves the <code>ConclifyApiPlayer</code> instance to persistent storage
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