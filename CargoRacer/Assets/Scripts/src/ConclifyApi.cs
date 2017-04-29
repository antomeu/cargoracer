// 
// ConclifyApi.cs
//  
// Contains the mono-behaviour classs to manage all API interactions
// 
// Author:		Jon Massey
// Copyright:   Conclify 2017
// Company:     Conclify
// Since:		29/04/2017
// ================================================================================================

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Conclify.Game;
using Conclify.Player;
using Conclify.Requests;
using Conclify.Responses;
using Conclify.Responses.Objects;
using UnityEngine;

namespace Conclify
{
	//Event Delegates
	//---------------------------------------------------------------------------------------------

	/// <summary>Delegate to define the method signature of a player updated event handler</summary>
	public delegate void ConclifyApiPlayerUpdatedHandler();

	/// <summary>Delegate to define the method signature of a game updated event handler</summary>
	public delegate void ConclifyApiGameUpdatedHandler();

	//ConclifyApi Class
	//---------------------------------------------------------------------------------------------

	/// <summary>
	/// Mono-behaviour classs to manage all API interactions
	/// </summary>
	public class ConclifyApi : MonoBehaviour
    {
		//Unity Properties
		//---------------------------------------------------------------------

		/// <summary>API key to use when accessing the API</summary>
	    public string ApiKey = null;

		/// <summary>Game-type to determine properties present in responses</summary>
		public ConclifyApiGameTypes GameType = ConclifyApiGameTypes.TokenBased;

		//Event Fields and Properties
		//---------------------------------------------------------------------

		/// <summary>Player updated event handlers</summary>
	    private ConclifyApiPlayerUpdatedHandler playerUpdatedHandlers = null;

		/// <summary>Game updated event handlers</summary>
		private ConclifyApiGameUpdatedHandler gameUpdatedHandlers = null;

		/// <summary>Adds or Removes a player updated event handler</summary>
		public event ConclifyApiPlayerUpdatedHandler PlayerUpdated
		{
			add { playerUpdatedHandlers += value; }
			remove { playerUpdatedHandlers -= value; }
		}

		/// <summary>Adds or Removes a game updated event handler</summary>
		public event ConclifyApiGameUpdatedHandler GameUpdated
		{
			add { gameUpdatedHandlers += value; }
			remove { gameUpdatedHandlers -= value; }
		}

		//Fields
		//---------------------------------------------------------------------

		/// <summary>Player information</summary>
		private ConclifyApiPlayer player = null;

		/// <summary>Game information</summary>
	    private ConclifyApiGame game = null;

		//Request Fields
		//---------------------------------------------------------------------

		/// <summary>Queue of pending API requests</summary>
		private Queue<ConclifyApiRequest> requestQueue = new Queue<ConclifyApiRequest>();

		/// <summary>API request currently being processed</summary>
	    private ConclifyApiRequest currentRequest = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Player information</summary>
		public ConclifyApiPlayer Player
		{ get { return player; } }

		/// <summary>Game information</summary>
		public ConclifyApiGame Game
		{ get { return game; } }

		//Start Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Performs any required initialisation of the API at game-start
		/// </summary>
		void Start()
	    {
			//Attempt to Load Player Data, Otherwise Create New Player
			player = (ConclifyApiPlayer.Load() ?? new ConclifyApiPlayer());

			//Attempt to Load Game Data, Otherwise Create New Game
			game = (ConclifyApiGame.Load() ?? new ConclifyApiGame());

			//Queue Initial Player Post
			RequestPlayerPost();

			//Start Co-Start Coroutine
		    StartCoroutine("CoStart");
	    }

		/// <summary>
		/// Starts the coroutine update loop
		/// </summary>
	    IEnumerator CoStart()
	    {
			//Loop to Yield Co-Update Coroutine
		    while(true)
			    yield return CoUpdate();
	    }

		//Update Method
		//---------------------------------------------------------------------

		/// <summary>
		/// Updates the API with each frame
		/// </summary>
	    void Update()
	    {
	    }

		/// <summary>
		/// Updates the API with each frame, with coroutine support
		/// </summary>
	    IEnumerator CoUpdate()
		{
			//Check for Currently Processing Request
			if(currentRequest != null)
			{
				//Yield Request Coroutine
				yield return currentRequest.Coroutine;

				//Check for Request Success
				if(currentRequest.IsSuccess)
				{
					//Parse API Response & Process It
					ConclifyApiResponse response = ConclifyApiResponse.ParseJson((currentRequest.Result as string), GameType);
					if(response != null)
						ProcessResponse(currentRequest, response);
				}
				else
				{
					//Log Failure
					print(currentRequest.Result);
				}

				//Clear Current Request
				currentRequest = null;
			}

			//Check for Pending Requests
			if(requestQueue.Any())
			{
				//Pop Next Request for Processing
				currentRequest = requestQueue.FirstOrDefault(request => request.CanExecute);
			}
		}

		//Request Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Sends a player post request to the API
		/// </summary>
	    public void RequestPlayerPost()
		{
			//Check for Pending Player Post Requests
			if(requestQueue.OfType<ConclifyApiRequestPlayerPost>().Any())
				return;

			//Enqueue Player Post Request
			requestQueue.Enqueue(new ConclifyApiRequestPlayerPost(this));
		}

		/// <summary>
		/// Sends a player patch request to the API
		/// </summary>
		/// <param name="firstName">First name to set for the player</param>
		/// <param name="lastName">Last name to set for the player</param>
		/// <param name="emailAddress">Email address to set for the player</param>
		/// <param name="country">Country to set for the player</param>
		/// <param name="deviceId">Device identifier to set for the player</param>
		/// <param name="platform">Platform to set for the player</param>
	    public void RequestPlayerPatch(string firstName = null, string lastName = null, string emailAddress = null, string country = null, string deviceId = null, string platform = null)
	    {
			//Validate Fields
		    if(string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(emailAddress) && string.IsNullOrEmpty(country) && string.IsNullOrEmpty(deviceId) && string.IsNullOrEmpty(platform))
			    return;

		    //Determine Patch Fields
		    string patchFirstName = (string.IsNullOrEmpty(firstName) ? player.FirstName : firstName);
		    string patchLastName = (string.IsNullOrEmpty(lastName) ? player.LastName : lastName);
		    string patchEmailAddress = (string.IsNullOrEmpty(emailAddress) ? player.EmailAddress : emailAddress);
		    string patchCountry = (string.IsNullOrEmpty(country) ? player.Country : country);
		    string patchDeviceId = (string.IsNullOrEmpty(deviceId) ? player.DeviceId : deviceId);
		    string patchPlatform = (string.IsNullOrEmpty(platform) ? player.Platform : platform);

			//Queue Player Patch Request
			requestQueue.Enqueue(new ConclifyApiRequestPlayerPatch(this, patchFirstName, patchLastName, patchEmailAddress, patchCountry, patchDeviceId, patchPlatform));
	    }

		/// <summary>
		/// Sends a player score post request to the API
		/// </summary>
		/// <param name="score">Score to post for the player</param>
	    public void RequestPlayerScorePost(long score)
	    {
			//Check Game Type
			if(GameType != ConclifyApiGameTypes.HighScore)
				return;

			//Validate Score
			if(score <= 0L)
				return;
			
		    //Queue Player Score Post Request
			requestQueue.Enqueue(new ConclifyApiRequestPlayerScorePost(this, score));
	    }

		/// <summary>
		/// Sends a game scores get request to the API
		/// </summary>
		public void RequestGameScoresGet()
		{
			//Check Game Type
			if(GameType != ConclifyApiGameTypes.HighScore)
				return;

			//Queue Game Score Get Request
			requestQueue.Enqueue(new ConclifyApiRequestGameScoresGet(this));
		}

		//Response Processing Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Processes responses from the API
		/// </summary>
		/// <param name="request">API request that generated the response</param>
		/// <param name="response">API response to process</param>
		private void ProcessResponse(ConclifyApiRequest request, ConclifyApiResponse response)
	    {
			//Check for Response
		    if(response == null)
				return;

			//Check Response Type
		    switch(response.ResponseType)
		    {
				case ConclifyApiResponseTypes.PlayerStandard:
			    {
					//Cast Response to Specific Type
				    ConclifyApiResponsePlayerStandard playerStandardResponse = (response as ConclifyApiResponsePlayerStandard);
				    if(playerStandardResponse == null)
					    return;

					//Update Player Fields
				    player.Id = playerStandardResponse.PlayerId;
				    player.Tokens = playerStandardResponse.Tokens;
				    player.TokensToThreshold = playerStandardResponse.TokensToThreshold;

					//Log Player Update
					//print(string.Format("Player Updated: ID = '{0}', Tokens = {1}, Tokens To Threshold = {2}", player.Id, player.Tokens, player.TokensToThreshold));
						
					//Raise Event
					RaisePlayerUpdatedEvent();
					
					//Save Player
					player.Save();
				    break;
			    }
				
				case ConclifyApiResponseTypes.PlayerExtended:
			    {
					//Cast Response to Specific Type
				    ConclifyApiResponsePlayerExtended playerExtendedResponse = (response as ConclifyApiResponsePlayerExtended);
				    if(playerExtendedResponse == null)
					    return;
					
					//Update Player Fields
				    player.Id = playerExtendedResponse.PlayerId;
				    player.Tokens = playerExtendedResponse.Tokens;
				    player.TokensToThreshold = playerExtendedResponse.TokensToThreshold;
				    player.FirstName = playerExtendedResponse.FirstName;
				    player.LastName = playerExtendedResponse.LastName;
				    player.EmailAddress = playerExtendedResponse.EmailAddress;
				    player.Country = playerExtendedResponse.Country;
				    player.Platform = playerExtendedResponse.Platform;
				    player.DeviceId = playerExtendedResponse.DeviceId;
					
					//Log Player Update
					//print(string.Format("Player Updated: ID = '{0}', Tokens = {1}, Tokens To Threshold = {2}, First Name = {3}, Last Name = {4}, Email Address = {5}, Country = {6}, Platform = {7}, Device ID = {8}", player.Id, player.Tokens, player.TokensToThreshold, player.FirstName, player.LastName, player.EmailAddress, player.Country, player.Platform, player.DeviceId));
					
					//Raise Event
					RaisePlayerUpdatedEvent();
					
					//Save Player
					player.Save();
				    break;
			    }

				case ConclifyApiResponseTypes.GameScoresList:
			    {
					//Cast Response to Specific Type
				    ConclifyApiResponseGameScoresList gameScoresListResponse = (response as ConclifyApiResponseGameScoresList);
				    if(gameScoresListResponse == null)
					    return;
					
					//Update Game
					game.ClearScores();
				    foreach(ConclifyApiResponseObjectScore score in gameScoresListResponse.Scores)
						game.AddScore(score.Rank, score.Name, score.Score);

					//Raise Event
				    RaiseGameUpdatedEvent();

					//Save Game
					game.Save();
					break;
			    }
		    }
	    }

		//Event Firing Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Raises a player updated event with all registerd handlers
		/// </summary>
	    private void RaisePlayerUpdatedEvent()
	    {
			//Copy Delegate
		    ConclifyApiPlayerUpdatedHandler tempDelegate = playerUpdatedHandlers;

			//Check for Delegate
		    if(tempDelegate != null)
			    tempDelegate();
		}

		/// <summary>
		/// Raises a game updated event with all registerd handlers
		/// </summary>
		private void RaiseGameUpdatedEvent()
		{
			//Copy Delegate
			ConclifyApiGameUpdatedHandler tempDelegate = gameUpdatedHandlers;

			//Check for Delegate
			if(tempDelegate != null)
				tempDelegate();
		}
	}
}
// ================================================================================================