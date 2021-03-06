﻿#region License

/*
 * Copyright 2002-2012 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System.Collections.Specialized;
using CSharp.Geeklist.Api.Interfaces;
using CSharp.Geeklist.Api.Models;
using Spring.Rest.Client;
using System.Threading.Tasks;

namespace CSharp.Geeklist.Api.Impl
{
	/// <summary>
	/// Implementation of <see cref="IConnectionsOperations"/>, providing binding to Geeklists' connection-oriented REST resources.
	/// </summary>
	/// <author>Scott Smith</author>
	class ConnectionsTemplate : AbstractGeeklistOperations, IConnectionOperations
	{
		private readonly RestTemplate _restTemplate;

		public ConnectionsTemplate(RestTemplate restTemplate, bool isAuthorized)
			: base(isAuthorized)
		{
			_restTemplate = restTemplate;
		}

		#region IConnectionsOperations Members

		public ConnectionsResponse GetUserConnections()
		{
			return GetUserConnections(1, 10);
		}

		public ConnectionsResponse GetUserConnections(int page, int count)
		{
			EnsureIsAuthorized();
			var parameters = BuildPagingParametersWithCount(page, count);
			return _restTemplate.GetForObject<ConnectionsResponse>(BuildUrl("user/connections", parameters));
		}

		public ConnectionsResponse GetUserConnections(string screenName)
		{
			return GetUserConnections(screenName, 1, 10);
		}

		public ConnectionsResponse GetUserConnections(string screenName, int page, int count)
		{
			var parameters = BuildPagingParametersWithCount(page, count);
			return _restTemplate.GetForObject<ConnectionsResponse>(BuildUrl("users/" + screenName + "/connections", parameters));
		}

		public Task<ConnectionsResponse> GetUserConnectionsAsync()
		{
			return GetUserConnectionsAsync(1, 10);
		}

		public Task<ConnectionsResponse> GetUserConnectionsAsync(int page, int count)
		{
			EnsureIsAuthorized();
			var parameters = BuildPagingParametersWithCount(page, count);
			return _restTemplate.GetForObjectAsync<ConnectionsResponse>(BuildUrl("user/connections", parameters));
		}

		public Task<ConnectionsResponse> GetUserConnectionsAsync(string screenName)
		{
			return GetUserConnectionsAsync(screenName, 1, 10);
		}

		public Task<ConnectionsResponse> GetUserConnectionsAsync(string screenName, int page, int count)
		{
			var parameters = BuildPagingParametersWithCount(page, count);
			return _restTemplate.GetForObjectAsync<ConnectionsResponse>(BuildUrl("users/" + screenName + "/connections", parameters));
		}

		#endregion

		#region Private Methods



		#endregion
	}
}