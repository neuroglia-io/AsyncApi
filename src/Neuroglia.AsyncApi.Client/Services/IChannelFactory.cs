/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Neuroglia.AsyncApi.Models;

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Defines the fundamentals of a service used to create <see cref="IChannel"/>s
    /// </summary>
    public interface IChannelFactory
    {

        /// <summary>
        /// Creates a new <see cref="IChannel"/>
        /// </summary>
        /// <param name="key">The key of the <see cref="IChannel"/> to create</param>
        /// <param name="definition">The <see cref="ChannelDefinition"/> of the <see cref="IChannel"/> to create</param>
        /// <param name="document">The <see cref="AsyncApiDocument"/> that defines the <see cref="IChannel"/> to create</param>
        /// <returns>A new <see cref="IChannel"/></returns>
        IChannel CreateChannel(string key, ChannelDefinition definition, AsyncApiDocument document);

    }

}
