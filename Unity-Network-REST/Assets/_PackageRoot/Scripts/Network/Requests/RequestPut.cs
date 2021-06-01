﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

public abstract class RequestPut<T1, T2> : Request<T2>
{
	public		override	string			RESTMethod	=> UnityWebRequest.kHttpVerbPUT;

	public					T1				data;

	public RequestPut(NetworkSO network, T1 data) : base(network)
	{
		this.data = data;
	}

	protected override UnityWebRequest CreateUnityWebRequest(string endpoint)
	{
		var json		= JsonConvert.SerializeObject(data);
		var byteData	= Encoding.UTF8.GetBytes(json);

#if UNITY_EDITOR
		try { DebugFormat.Log<RequestPost<T1, T2>>($"JSON {RESTMethod}:\n\n{JsonPrettify(json)}\n"); }
		catch (Exception e)		{ DebugFormat.Log<RequestPost<T1, T2>>($"JSON {RESTMethod}:\n\n{json}\n"); }
#endif

		return new UnityWebRequest(endpoint)
		{
			method			= RESTMethod,
			uploadHandler	= new UploadHandlerRaw(byteData),
			downloadHandler = new DownloadHandlerBuffer()
		};
	}
}