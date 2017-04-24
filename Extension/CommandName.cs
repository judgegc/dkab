using System;

namespace dkab.Extension
{
    [AttributeUsage(AttributeTargets.All)]
    class CommandName: Attribute
    {
        public string Name { get; private set; }

        public CommandName(string name)
        {
            Name = name;
        }
    }
}
