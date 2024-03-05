namespace Orobouros.Attributes
{
    /// <summary>
    /// Enum representing what kind of value a settings option holds.
    /// </summary>
    public enum SettingType
    {
        /// <summary>
        /// Simple true/false value.
        /// </summary>
        Boolean,

        /// <summary>
        /// A whole number value. An integer by default, but could be any derivitive type of whole
        /// number storage.
        /// </summary>
        Number,

        /// <summary>
        /// A partial number value. Could be a float, a double, or any other decimal type.
        /// </summary>
        Decimal,

        /// <summary>
        /// A text value.
        /// </summary>
        String,

        /// <summary>
        /// A value that contains a list of other values. Could be an array, list, or any other
        /// iEnumerable subclass.
        /// </summary>
        Collection,

        /// <summary>
        /// A value different from any of the other value types. Example: logging class.
        /// </summary>
        Other,

        /// <summary>
        /// Default enum value used for errors and placeholders.
        /// </summary>
        Unknown
    }

    public class ModuleSetting : Attribute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public SettingType Type { get; set; }
        public string RawTypeName { get; set; }

        public ModuleSetting(string name = "", string description = "", SettingType type = SettingType.Unknown)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}