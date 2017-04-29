// 
// ConclifyApiRequestPlayerPatch.cs
//  
// Contains the class to represent a player patch request to the API
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Conclify.Requests
{
	/// <summary>
	/// Class to represent a player patch request to the API
	/// </summary>
	[Serializable]
	class ConclifyApiRequestPlayerPatch : ConclifyApiRequest
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Url of the request</summary>
		private const string Url = "https://api.conclify.com/api/v1/players/{0}";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>First name of the player</summary>
		private string firstName = null;

		/// <summary>Last name of the player</summary>
		private string lastName = null;

		/// <summary>Email address of the player</summary>
		private string emailAddress = null;

		/// <summary>Country of the player</summary>
		private string country = null;

		/// <summary>Identifier of the player's device</summary>
		private string deviceId = null;

		/// <summary>Platform that the player uses to play the game</summary>
		private string platform = null;

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>Determines if the request can be executed or not</summary>
		public override bool CanExecute
		{ get { return !string.IsNullOrEmpty(ConclifyApi.Player.Id); } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiRequestPlayerPost</code> instance
		/// </summary>
		/// <param name="conclifyApi">Api object to coordinate with</param>
		/// <param name="playerFirstName">First name to set for the player</param>
		/// <param name="playerLastName">Last name to set for the player</param>
		/// <param name="playerEmailAddress">Email address to set for the player</param>
		/// <param name="playerCountry">Country to set for the player</param>
		/// <param name="playerDeviceId">Device identifier to set for the player</param>
		/// <param name="playerPlatform">Platform to set for the player</param>
		public ConclifyApiRequestPlayerPatch(ConclifyApi conclifyApi, string playerFirstName, string playerLastName, string playerEmailAddress, string playerCountry, string playerDeviceId, string playerPlatform)
			: base(conclifyApi)
		{
			//Store Fields
			firstName = playerFirstName;
			lastName = playerLastName;
			emailAddress = playerEmailAddress;
			country = playerCountry;
			deviceId = playerDeviceId;
			platform = playerPlatform;
		}

		//Request Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Retrieves the specific request to perform
		/// </summary>
		protected override IEnumerator GetRequest()
		{
			//Create Form
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddField("apiKey", ConclifyApi.ApiKey);
			wwwForm.AddField("playerId", ConclifyApi.Player.Id);
			if(!string.IsNullOrEmpty(firstName))
				wwwForm.AddField("firstName", firstName);
			if(!string.IsNullOrEmpty(lastName))
				wwwForm.AddField("lastName", lastName);
			if(!string.IsNullOrEmpty(emailAddress))
				wwwForm.AddField("emailAddress", emailAddress);
			if(!string.IsNullOrEmpty(country))
				wwwForm.AddField("country", country);
			if(!string.IsNullOrEmpty(deviceId))
				wwwForm.AddField("deviceId", deviceId);
			if(!string.IsNullOrEmpty(platform))
				wwwForm.AddField("platform", platform);

			//Format Url
			string url = string.Format(Url, ConclifyApi.Player.Id);

			//Add Override to Http-Patch
			Dictionary<string, string> headers = wwwForm.headers;
			headers.Add("X-HTTP-Method-Override", "PATCH");

			//Create Request
			WWW request = new WWW(url, wwwForm.data, headers);

			//Yield Request
			yield return request;

			//Update Success & Retrieve Result
			IsSuccess = string.IsNullOrEmpty(request.error);
			Result = (IsSuccess ? request.text : request.error);
		}
	}
}
// ================================================================================================