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
using Microsoft.Extensions.DependencyInjection;
using Neuroglia.AsyncApi.Models;
using System;

namespace Neuroglia.AsyncApi.Client.Services
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IChannelFactory"/> interface
    /// </summary>
    public class ChannelFactory
        : IChannelFactory
    {

        /// <summary>
        /// Initializes a new <see cref="ChannelFactory"/>
        /// </summary>
        /// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
        public ChannelFactory(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceProvider"/>
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <inheritdoc/>
        public virtual IChannel CreateChannel(string key, ChannelDefinition definition, AsyncApiDocument document)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            return ActivatorUtilities.CreateInstance<Channel>(this.ServiceProvider, key, definition, document);
        }

    }

}
