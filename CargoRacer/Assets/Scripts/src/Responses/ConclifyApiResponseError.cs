// 
// ConclifyApiResponseError.cs
//  
// Contains the class to represent an error API response
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using Conclify.Game;
using TinyJSON;

namespace Conclify.Responses
{
	/// <summary>
	/// Class to represent an error API response
	/// </summary>
	public class ConclifyApiResponseError : ConclifyApiResponse
	{
		//Constants
		//---------------------------------------------------------------------

		/// <summary>Name of the 'code' json property</summary>
		private const string Property_Code = "code";

		/// <summary>Name of the 'message' json property</summary>
		private const string Property_Message = "message";
		//Fields
		//---------------------------------------------------------------------

		/// <summary>Code of the error</summary>
		private int code = 0;

		/// <summary>Message of the error</summary>
		private string message = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Code of the error</summary>
		public int Code
		{ get { return code; } }

		/// <summary>Message of the error</summary>
		public string Message
		{ get { return message; } }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiResponseError</code> instance
		/// </summary>
		/// <param name="jsonResponse">Json response to retrieve fields from</param>
		/// <param name="gameType">Game-type to use to determine if optional properties exist or not</param>
		internal ConclifyApiResponseError(Variant jsonResponse, ConclifyApiGameTypes gameType)
			:base(jsonResponse, gameType)
		{
			//Retrieve Fields
			code = jsonResponse[Property_Code];
			message = jsonResponse[Property_Message];
		}
	}
}
// ================================================================================================