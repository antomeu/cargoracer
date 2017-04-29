// 
// ConclifyApiRequestPlayerPost.cs
//  
// Contains the class to represent a player post request to the API
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System;
using System.Collections;
using UnityEngine;

namespace Conclify.Requests
{
	/// <summary>
	/// Class to represent a player post request to the API
	/// </summary>
	[Serializable]
	internal class ConclifyApiRequestPlayerPost : ConclifyApiRequest
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Url of the request, if no player exists locally</summary>
		private const string Url_NoPlayer = "https://api.conclify.com/api/v1/players";

		/// <summary>Url of the request, if a player exists locally</summary>
		private const string Url_Player = "https://api.conclify.com/api/v1/players/{0}";

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>Determines if the request can be executed or not</summary>
		public override bool CanExecute
		{ get { return true; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiRequestPlayerPost</code> instance
		/// </summary>
		/// <param name="conclifyApi">Api object to coordinate with</param>
		public ConclifyApiRequestPlayerPost(ConclifyApi conclifyApi)
			:base(conclifyApi)
		{ /* Default Constructor */ }

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

			//Declare Url
			string url = Url_NoPlayer;

			//Check for Existing Player
			if(!string.IsNullOrEmpty(ConclifyApi.Player.Id))
			{
				//Update Url
				url = string.Format(Url_Player, ConclifyApi.Player.Id);

				//Add Player Id Form Field
				wwwForm.AddField("playerId", ConclifyApi.Player.Id);
			}

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