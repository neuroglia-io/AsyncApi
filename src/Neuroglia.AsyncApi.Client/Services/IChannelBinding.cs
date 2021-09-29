﻿/*
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
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Neuroglia.AsyncApi.Client.Services
{

    /// <summary>
    /// Defines the fundamentals of an <see cref="IChannel"/>'s binding
    /// </summary>
    public interface IChannelBinding
        : IDisposable
    {

        /// <summary>
        /// Publishes the specified <see cref="IMessage"/>
        /// </summary>
        /// <param name="message">The <see cref="IMessage"/> to publish</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        Task PublishAsync(IMessage message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Subscribes to <see cref="IMessage"/>s consumed by the <see cref="IChannelBinding"/>
        /// </summary>
        /// <param name="observer">The subscribing <see cref="IObserver{T}"/></param>
        /// <param name="cancellationToken"></param>
        /// <returns>An <see cref="IDisposable"/> object used to unsubscribe from the observable sequence</returns>
        Task<IDisposable> SubscribeAsync(IObserver<IMessage> observer, CancellationToken cancellationToken = default);

    }

}
