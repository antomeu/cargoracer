// 
// ConclifyApiResponsePlayerExtended.cs
//  
// Contains the class to represent a player-extended API response
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System;
using Conclify.Game;
using TinyJSON;

namespace Conclify.Responses
{
	/// <summary>
	/// Class to represent a player-extended API response
	/// </summary>
	public class ConclifyApiResponsePlayerExtended : ConclifyApiResponsePlayerStandard
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Name of the 'platform' json property</summary>
		private const string Property_Platform = "platform";

		/// <summary>Name of the 'deviceId' json property</summary>
		private const string Property_DeviceId = "deviceId";

		/// <summary>Name of the 'firstName' json property</summary>
		private const string Property_FirstName = "firstName";

		/// <summary>Name of the 'lastName' json property</summary>
		private const string Property_LastName = "lastName";

		/// <summary>Name of the 'emailAddress' json property</summary>
		private const string Property_EmailAddress = "emailAddress";

		/// <summary>Name of the 'country' json property</summary>
		private const string Property_Country = "country";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Platform that the player uses to play the game</summary>
		private string platform = null;

		/// <summary>Identifier of the player's device</summary>
		private string deviceId = null;

		/// <summary>First name of the player</summary>
		private string firstName = null;

		/// <summary>Last name of the player</summary>
		private string lastName = null;

		/// <summary>Email address of the player</summary>
		private string emailAddress = null;

		/// <summary>Country of the player</summary>
		private string country = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Platform that the player uses to play the game</summary>
		public string Platform
		{ get { return platform; } }

		/// <summary>Identifier of the player's device</summary>
		public string DeviceId
		{ get { return deviceId; } }

		/// <summary>First name of the player</summary>
		public string FirstName
		{ get { return firstName; } }

		/// <summary>Last name of the player</summary>
		public string LastName
		{ get { return lastName; } }

		/// <summary>Email address of the player</summary>
		public string EmailAddress
		{ get { return emailAddress; } }

		/// <summary>Country of the player</summary>
		public string Country
		{ get { return country; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponsePlayerExtended</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponsePlayerExtended(Variant jsonResponse, ConclifyApiGameTypes gameType)
			: base(jsonResponse, gameType)
		{
			//Retrieve Response Fields
			try { platform = jsonResponse[Property_Platform]; }
			catch(Exception) { }
			try{ deviceId = jsonResponse[Property_DeviceId]; }
			catch(Exception) { }
			try{ firstName = jsonResponse[Property_FirstName]; }
			catch(Exception) { }
			try { lastName = jsonResponse[Property_LastName]; }
			catch(Exception) { }
			try { emailAddress = jsonResponse[Property_EmailAddress]; }
			catch(Exception) { }
			try { country = jsonResponse[Property_Country]; }
			catch(Exception) { }
		}
	}
}
// ================================================================================================