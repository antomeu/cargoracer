// 
// ConclifyApiRequestPlayerScorePost.cs
//  
// Contains the class to represent a player score post request to the API
// 
// Author:		Jon Massey
// Copyright:   DSA Practice 2017
// Company:     DSA Practice
// Since:		29/04/2017
// ================================================================================================

using System;
using System.Collections;
using UnityEngine;

namespace Conclify.Requests
{
	/// <summary>
	/// Class to represent a player score post request to the API
	/// </summary>
	[Serializable]
	class ConclifyApiRequestPlayerScorePost : ConclifyApiRequest
	{
		//Constants
		//---------------------------------------------------------------------
		
		/// <summary>Url of the request</summary>
		private const string Url = "https://api.conclify.com/api/v1/players/{0}/scores";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Score to post for the player</summary>
		private long score = 0L;

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
		/// <param name="playerScore">Score to post for the player</param>
		public ConclifyApiRequestPlayerScorePost(ConclifyApi conclifyApi, long playerScore)
			: base(conclifyApi)
		{
			//Set Score
			score = playerScore;
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
			wwwForm.AddField("score", score.ToString());

			//Format Url
			string url = string.Format(Url, ConclifyApi.Player.Id);
			
			//Create Request
			WWW request = new WWW(url, wwwForm);

			//Yield Request
			yield return request;

			//Update Success & Retrieve Result
			IsSuccess = string.IsNullOrEmpty(request.error);
			Result = (IsSuccess ? request.text : request.error);
		}
	}
}
// ================================================================================================