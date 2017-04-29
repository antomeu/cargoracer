// 
// ConclifyApiRequest.cs
//  
// Contains the class to represent an individual request to the API
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
	/// Class to represent an individual request to the API
	/// </summary>
	[Serializable]
	internal abstract class ConclifyApiRequest
	{
		//Fields
		//---------------------------------------------------------------------

		/// <summary>Api object to coordiante with</summary>
		[NonSerialized]
		private ConclifyApi api = null;

		/// <summary>Coroutine for the in-progress request</summary>
		[NonSerialized]
		private Coroutine coroutine = null;

		/// <summary>Indicates if the request was successful or not</summary>
		[NonSerialized]
		private bool success = false;

		/// <summary>Generic result of the request</summary>
		[NonSerialized]
		private object result = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Api object to coordiante with</summary>
		protected ConclifyApi ConclifyApi
		{ get { return api; } }

		/// <summary>Coroutine for the in-progress request</summary>
		public Coroutine Coroutine
		{ get { return (coroutine ?? (coroutine = api.StartCoroutine(PerformRequest()))); } }

		/// <summary>Indicates if the request was successful or not</summary>
		public bool IsSuccess
		{
			get { return success; }
			protected set { success = value; }
		}

		/// <summary>Generic result of the request</summary>
		public object Result
		{
			get { return result; }
			protected set { result = value; }
		}

		/// <summary>Determines if the request can be executed or not</summary>
		public abstract bool CanExecute { get; }

		//Constructor
		//---------------------------------------------------------------------

		/// <summary>
		/// Creates a new <code>ConclifyApiRequest</code> instance
		/// </summary>
		/// <param name="conclifyApi">Api object to coordinate with</param>
		protected ConclifyApiRequest(ConclifyApi conclifyApi)
		{
			//Store API
			api = conclifyApi;
		}

		//Request Methods
		//---------------------------------------------------------------------

		/// <summary>
		/// Begins performing the request
		/// </summary>
		private IEnumerator PerformRequest()
		{
			//Retrieve Specific Request
			IEnumerator request = GetRequest();

			//Handle Request Coroutine Internally
			while(request.MoveNext())
			{
				//Retrieve Request Result
				result = request.Current;

				//Yield Result
				yield return result;
			}
		}

		/// <summary>
		/// Retrieves the specific request to perform
		/// </summary>
		protected abstract IEnumerator GetRequest();
	}
}
// ================================================================================================