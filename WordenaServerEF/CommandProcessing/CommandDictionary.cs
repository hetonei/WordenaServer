using System;
using System.Collections.Generic;

namespace WordenaServerEF.CommandProcessing
{
    public class CommandDictionary
    {
        private ICommand _commands;
        private Dictionary<string, Func<string, string>> _dictionary;

        public CommandDictionary(ICommand commands)
        {
            _commands = commands;
            _dictionary = CreateDictionary();
        }
        public Dictionary<string, Func<string, string>> CreateDictionary()
        {
            var dictionary = new Dictionary<string, Func<string, string>>
            {
                {"LogIn", _commands.LogIn},
                {"NewUser", _commands.NewUser},
                {"NewChar", _commands.NewChar},
                {"RemoveChar", _commands.RemoveChar},
                {"StartGame", _commands.StartGame},
                {"CatchResults", _commands.CatchResults},
                {"GetResult", _commands.GetResult}
            };
            return dictionary;
        }

        public string RunCommand(string name, string value)
        {
            if (_dictionary.ContainsKey(name))
            {
                return _dictionary[name].Invoke(value);
            }
            return string.Empty;
        }
    }
}
