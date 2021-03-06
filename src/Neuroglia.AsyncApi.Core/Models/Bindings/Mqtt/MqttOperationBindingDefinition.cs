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

namespace Neuroglia.AsyncApi.Models.Bindings.Mqtt
{
    /// <summary>
    /// Represents the object used to configure an MQTT operation binding
    /// </summary>
    public class MqttOperationBindingDefinition
        : MqttBindingDefinition, IOperationBindingDefinition
    {

        /// <summary>
        /// Gets/sets an integer that defines the Quality of Service (QoS) levels for the message flow between client and server. Its value MUST be either 0 (At most once delivery), 1 (At least once delivery), or 2 (Exactly once delivery).
        /// </summary>
        [Newtonsoft.Json.JsonProperty("qos")]
        [YamlDotNet.Serialization.YamlMember(Alias = "qos")]
        [System.Text.Json.Serialization.JsonPropertyName("qos")]
        public virtual MqttQoSLevel QoS { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether the broker the broker should retain the message or not.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("retain")]
        [YamlDotNet.Serialization.YamlMember(Alias = "retain")]
        [System.Text.Json.Serialization.JsonPropertyName("retain")]
        public virtual bool Retain { get; set; }

        /// <summary>
        /// Gets/sets the version of this binding. Defaults to 'latest'.
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bindingVersion")]
        [YamlDotNet.Serialization.YamlMember(Alias = "bindingVersion")]
        [System.Text.Json.Serialization.JsonPropertyName("bindingVersion")]
        public virtual string BindingVersion { get; set; } = "latest";

    }

}
