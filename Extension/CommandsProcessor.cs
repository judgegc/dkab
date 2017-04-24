using dkab.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace dkab.Extension
{
    public class CommandsProcessor
    {
        private List<Type> commands;
        public CommandsProcessor()
        {
            commands = TypeHelper.FindDerivedTypes<ICommand>();
        }
        public ICommand Parse(string input)
        {
            string[] fnc = input.Split();
            foreach (var cmdType in commands)
            {
                if((cmdType.GetCustomAttribute(typeof(CommandName)) as CommandName).Name.Equals(fnc[0]))
                {
                    var compConstructors = cmdType.GetConstructors()
                        .Where(x => x.GetParameters()
                        .Length == fnc.Length - 1);

                    foreach (var constr in compConstructors)
                    {
                        try
                        {
                            object[] args = constr.GetParameters().Select(x =>
                            {

                                    if (x.ParameterType == typeof(float))
                                    {
                                        return float.Parse(fnc[x.Position + 1], CultureInfo.InvariantCulture);
                                    }
                                    else if (x.ParameterType == typeof(int))
                                    {
                                        return int.Parse(fnc[x.Position + 1]);
                                    }
                                    else if (x.ParameterType == typeof(string))
                                    {
                                        return fnc[x.Position + 1];
                                    }
                                return (object)null;
                            }).Cast<object>().ToArray();

                            return (ICommand)Activator.CreateInstance(cmdType, args);
                        }
                        catch(FormatException)
                        {
                        }
                    }                  
                }
            }
            throw new Exception(string.Format("Unknown command type: {0}", input));
        }
    }
}
