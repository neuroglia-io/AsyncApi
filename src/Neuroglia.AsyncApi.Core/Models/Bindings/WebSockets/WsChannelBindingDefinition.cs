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
using NJsonSchema;

namespace Neuroglia.AsyncApi.Models.Bindings.WebSockets
{
    /// <summary>
    /// Represents the object used to configure an WebSocket channel binding
    /// </summary>
    public class WsChannelBindingDefinition
        : WsBindingDefinition, IChannelBindingDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="WsChannelBindingDefinition"/>'s operation type
        /// </summary>
        [Newtonsoft.Json.JsonProperty("type")]
        [YamlDotNet.Serialization.YamlMember(Alias = "type")]
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public virtual HttpBindingOperationType Type { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for each query parameter. This schema MUST be of type object and have a properties key.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("query")]
        [YamlDotNet.Serialization.YamlMember(Alias = "query")]
        [System.Text.Json.Serialization.JsonPropertyName("query")]
        public virtual JsonSchema Query { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JsonSchema"/> containing the definitions for HTTP-specific headers. This schema MUST be of type object and have a properties key.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("headers")]
        [YamlDotNet.Serialization.YamlMember(Alias = "headers")]
        [System.Text.Json.Serialization.JsonPropertyName("headers")]
        public virtual JsonSchema Headers { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
