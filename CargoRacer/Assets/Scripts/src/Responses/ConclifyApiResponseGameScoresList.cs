// 
// ConclifyApiResponseGameScoresList.cs
//  
// Contains the class to represent a game scores list API response
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		28/04/2017
// ================================================================================================

using System.Collections.Generic;
using Conclify.Game;
using Conclify.Responses.Objects;
using TinyJSON;

namespace Conclify.Responses
{
	/// <summary>
	/// Class to represent a game scores list API response
	/// </summary>
	public class ConclifyApiResponseGameScoresList : ConclifyApiResponse
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Name of the 'scores' json property</summary>
		private const string Property_Scores = "scores";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Collection of score objects contained in the response</summary>
		private List<ConclifyApiResponseObjectScore> scores = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Collection of score objects contained in the response</summary>
		public IEnumerable<ConclifyApiResponseObjectScore> Scores
		{ get { return scores; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponseGameScoresList</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponseGameScoresList(Variant jsonResponse, ConclifyApiGameTypes gameType)
			: base(jsonResponse, gameType)
		{
			//Create Collection
			scores = new List<ConclifyApiResponseObjectScore>();

			//Retrieve Scores Property
			ProxyArray scoresArray = (jsonResponse[Property_Scores] as ProxyArray);
			if(scoresArray == null)
				return;

			//Loop Through Scores
			foreach(Variant scoreJson in scoresArray)
			{
				//Create Score Object
				scores.Add(new ConclifyApiResponseObjectScore(scoreJson, gameType));
			}
		}
	}
}
// ================================================================================================