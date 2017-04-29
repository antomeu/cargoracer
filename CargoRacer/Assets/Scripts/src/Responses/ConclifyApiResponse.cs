// 
// ConclifyApiResponse.cs
//  
// Contains the class to represent a basic API response
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
	/// Class to represent a basic API response
	/// </summary>
	public class ConclifyApiResponse
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Name of the 'responseType' json property</summary>
		private const string Property_ResponseType = "responseType";

		/// <summary>Name of the 'isSuccess' json property</summary>
		private const string Property_IsSuccess = "isSuccess";

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Type of the API response</summary>
		private ConclifyApiResponseTypes responseType = ConclifyApiResponseTypes.Basic;

		/// <summary>Indicates if the response is for a successful request or not</summary>
		private bool isSuccess = false;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Type of the API response</summary>
		public ConclifyApiResponseTypes ResponseType
		{ get { return responseType; } }

		/// <summary>Indicates if the response is for a successful request or not</summary>
		public bool IsSuccess
		{ get { return isSuccess; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponse</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		protected ConclifyApiResponse(Variant jsonResponse, ConclifyApiGameTypes gameType)
		{
			//Retrieve Response Type
			int responseTypeInt = (jsonResponse[Property_ResponseType]);
			responseType = (ConclifyApiResponseTypes)responseTypeInt;

			//Retrieve Success Flag
			isSuccess = jsonResponse[Property_IsSuccess];
		}

		//JSON Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Parses a new <code>ConclifyApiResponse</code> instance from the supplied json string
		/// </summary>
		/// <param name="json">Json string to parse the response instance from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		/// <returns>A parsed <code>C</code>, or <code>null</code> if an error occurs</returns>
		internal static ConclifyApiResponse ParseJson(string json, ConclifyApiGameTypes gameType)
		{
			try
			{
				//Load JSON
				Variant jsonReponse = JSON.Load(json);

				//Retrieve Response Type
				int responseTypeInt = jsonReponse[Property_ResponseType];
				ConclifyApiResponseTypes responseType = (ConclifyApiResponseTypes)responseTypeInt;

				//Check Response Type
				switch(responseType)
				{
					case ConclifyApiResponseTypes.Error:
						return new ConclifyApiResponseError(jsonReponse, gameType);

					case ConclifyApiResponseTypes.PlayerStandard:
						return new ConclifyApiResponsePlayerStandard(jsonReponse, gameType);

					case ConclifyApiResponseTypes.PlayerExtended:
						return new ConclifyApiResponsePlayerExtended(jsonReponse, gameType);

					case ConclifyApiResponseTypes.PlayerReward:
						return new ConclifyApiResponsePlayerReward(jsonReponse, gameType);

					case ConclifyApiResponseTypes.PlayerRewardList:
						return new ConclifyApiResponsePlayerRewardList(jsonReponse, gameType);

					case ConclifyApiResponseTypes.GameRewardList:
						return new ConclifyApiResponseGameRewardList(jsonReponse, gameType);

					case ConclifyApiResponseTypes.GameRewardsCount:
						return new ConclifyApiResponseGameRewardsCount(jsonReponse, gameType);

					case ConclifyApiResponseTypes.GameScoresList:
						return new ConclifyApiResponseGameScoresList(jsonReponse, gameType);

					default:
						return new ConclifyApiResponse(jsonReponse, gameType);
				}
			}
			catch(Exception)
			{
				return null;
			}
		}
	}
}
// ================================================================================================