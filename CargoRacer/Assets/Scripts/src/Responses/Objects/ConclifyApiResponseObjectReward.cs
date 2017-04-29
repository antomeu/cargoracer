using System;
using System.Collections.Generic;
using System.Text;

namespace Conclify.Responses.Objects
{
	public class ConclifyApiResponseObjectReward
	{
		//Fields
		//---------------------------------------------------------------------

		/// <summary>Type of the reward</summary>
		private int type = 0;

		/// <summary>Identifier of the player that the reward is assigned to</summary>
		private string playerId = null;

		/// <summary>Indicates if the reward has been redeemed or not</summary>
		private bool redeemed = false;

		/// <summary>Short title of the reward's function</summary>
		private string title = null;

		/// <summary>Description of the reward</summary>
		private string description = null;

		/// <summary>Code for code-based rewards</summary>
		private string code = null;

		//Properties
		//---------------------------------------------------------------------

		/// <summary>Type of the reward</summary>
		public int Type
		{
			get { return type; }
			internal set { type = value; }
		}

		/// <summary>Identifier of the player that the reward is assigned to</summary>
		public string PlayerId
		{
			get { return playerId; }
			internal set { playerId = value; }
		}

		/// <summary>Indicates if the reward has been redeemed or not</summary>
		public bool Redeemed
		{
			get { return redeemed; }
			internal set { redeemed = value; }
		}

		/// <summary>Short title of the reward's function</summary>
		public string Title
		{
			get { return title; }
			internal set { title = value; }
		}

		/// <summary>Description of the reward</summary>
		public string Description
		{
			get { return description; }
			internal set { description = value; }
		}

		/// <summary>Code for code-based rewards</summary>
		public string Code
		{
			get { return code; }
			internal set { code = value; }
		}

		//Constructor
		//---------------------------------------------------------------------

		internal ConclifyApiResponseObjectReward()
		{

		}

		//JSON Methods
		//---------------------------------------------------------------------

		internal static ConclifyApiResponseObjectReward ParseJson(string json)
		{
			return null;
		}
	}
}
