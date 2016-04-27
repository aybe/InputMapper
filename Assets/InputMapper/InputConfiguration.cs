using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Assets.InputMapper.Maths;
using JetBrains.Annotations;

namespace Assets.InputMapper
{
    /// <summary>
    ///     Represents an input configuration.
    /// </summary>
    public sealed class InputConfiguration
    {
        /// <summary>
        ///     Constructor for serialization, use
        ///     <see cref="InputConfiguration(System.Collections.Generic.List{Assets.InputMapper.Command})" /> instead.
        /// </summary>
        [UsedImplicitly]
        public InputConfiguration()
        {
        }

        public InputConfiguration([NotNull] List<Command> commands)
        {
            if (commands == null) throw new ArgumentNullException("commands");
            Commands = commands;
        }

        /// <summary>
        ///     Gets or sets the commands for this configuration.
        /// </summary>
        public List<Command> Commands { get; private set; }

        /// <summary>
        ///     Serializes an instance.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="configuration"></param>
        public static void Serialize([NotNull] Stream stream, [NotNull] InputConfiguration configuration)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (configuration == null) throw new ArgumentNullException("configuration");
            var settings = new XmlWriterSettings {Indent = true};
            using (var writer = XmlWriter.Create(stream, settings))
            {
                var extraTypes = GetExtraTypes();
                var serializer = new XmlSerializer(typeof(InputConfiguration), extraTypes);
                serializer.Serialize(writer, configuration);
            }
        }

        /// <summary>
        ///     Deserializes an instance.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static InputConfiguration Deserialize([NotNull] Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            var settings = new XmlReaderSettings();
            using (var xmlReader = XmlReader.Create(stream, settings))
            {
                var extraTypes = GetExtraTypes();
                var serializer = new XmlSerializer(typeof(InputConfiguration), extraTypes);
                var o = serializer.Deserialize(xmlReader);
                var configuration = o as InputConfiguration;
                return configuration;
            }
        }

        private static Type[] GetExtraTypes()
        {
            // find types needed for serialization in current assembly
            var executingAssembly = Assembly.GetExecutingAssembly();
            var types = executingAssembly.GetTypes();
            var needed = new[] {typeof(Command), typeof(IDeviceButton), typeof(Easing)};
            var extraTypes = types.Where(s => needed.Any(t => t == s.BaseType)).ToArray();
            return extraTypes;
        }

        /// <summary>
        ///     Updates all device buttons in buttons of commands in this instance.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            foreach (var command in Commands)
            {
                foreach (var button in command.Buttons)
                {
                    foreach (var deviceButton in button)
                    {
                        deviceButton.Update(deltaTime);
                    }
                }
            }
        }
    }
}