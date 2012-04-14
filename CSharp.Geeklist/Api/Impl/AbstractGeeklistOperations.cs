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

using System.Globalization;
using System.Text;
using System.Collections.Specialized;
using Spring.Http;

namespace CSharp.Geeklist.Api.Impl
{
    /// <summary>
    /// Base class for Geeklist operations.
    /// </summary>
    /// <author>Craig Walls</author>
    /// <author>Bruno Baia (.NET)</author>
    abstract class AbstractGeeklistOperations
    {
        private readonly bool _isAuthorized;

    	protected AbstractGeeklistOperations(bool isAuthorized) 
        {
		    _isAuthorized = isAuthorized;
	    }

        protected void EnsureIsAuthorized() 
        {
		    if (!_isAuthorized) 
            {
			    throw new GeeklistApiException(
                    "Authorization is required for the operation, but the API binding was created without authorization.", 
                    GeeklistApiError.NotAuthorized);
		    }
	    }

    	protected string BuildUrl(string path, NameValueCollection parameters)
        {
            var qsBuilder = new StringBuilder();
            bool isFirst = true;
            foreach (string key in parameters)
            {
                if (isFirst)
                {
                    qsBuilder.Append('?');
                    isFirst = false;
                }
                else
                {
                    qsBuilder.Append('&');
                }
                qsBuilder.Append(HttpUtils.UrlEncode(key));
                qsBuilder.Append('=');
                qsBuilder.Append(HttpUtils.UrlEncode(parameters[key]));
            }
            return path + qsBuilder;
	    }

    	protected static NameValueCollection BuildPagingParametersWithCount(int page, int count)
		{
			var parameters = new NameValueCollection
			{
			    {"page", page.ToString(CultureInfo.InvariantCulture)},
			    {"count", count.ToString(CultureInfo.InvariantCulture)}
			};
    		return parameters;
		}
    }
}