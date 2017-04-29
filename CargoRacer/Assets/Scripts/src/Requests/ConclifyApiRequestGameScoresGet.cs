// 
// ConclifyApiRequestGameScoresGet.cs
//  
// Contains the class to represent a game scores get request to the API
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System;
using System.Collections;
using Conclify.Game;
using UnityEngine;

namespace Conclify.Requests
{
	/// <summary>
	/// Class to represent a game scores get request to the API
	/// </summary>
	[Serializable]
	class ConclifyApiRequestGameScoresGet : ConclifyApiRequest
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Url of the request</summary>
		private const string Url = "https://api.conclify.com/api/v1/games/scores?apiKey={0}";

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>Determines if the request can be executed or not</summary>
		public override bool CanExecute
		{ get { return (ConclifyApi.GameType == ConclifyApiGameTypes.HighScore); } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiRequestGameScoresGet</code> instance
		/// </summary>
		/// <param name="conclifyApi">Api object to coordinate with</param>
		public ConclifyApiRequestGameScoresGet(ConclifyApi conclifyApi)
			: base(conclifyApi)
		{ /* Default Constructor */ }

		//Request Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Retrieves the specific request to perform
		/// </summary>
		protected override IEnumerator GetRequest()
		{
			//Format Url
			string url = string.Format(Url, ConclifyApi.ApiKey);

			//Create Request
			WWW request = new WWW(url);

			//Yield Request
			yield return request;

			//Update Success & Retrieve Result
			IsSuccess = string.IsNullOrEmpty(request.error);
			Result = (IsSuccess ? request.text : request.error);
		}
	}
}
// ================================================================================================