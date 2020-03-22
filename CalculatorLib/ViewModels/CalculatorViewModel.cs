using CalculatorLib.BLL;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace CalculatorLib.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, string[]> _views;
        private Commands _commands;

        public CalculatorViewModel() : this(
            new Dictionary<string, string[]>
            {
                {
                    "Simple",
                    new[] {@"ViewModels\SimpleCalculator.xml", "CalculatorLib.ViewModels.SimpleCalculator.xml"}
                },
                {"Complex", new[] {@"ViewModels\Complex.xml", "CalculatorLib.ViewModels.Complex.xml"}}
            },
            new CommandExecutor(new CommandAcceptorFactory()))
        {
        }

        public CalculatorViewModel(Dictionary<string, string[]> views, ICommandExecutor commandHandler)
        {
            _views = views;
            CommandExecutor = commandHandler;
            LoadView(Views.First());
        }

        public IList<string> Views => _views.Keys.ToList();

        public List<Command> Commands
        {
            get => _commands.CommandList;
            set => _commands.CommandList = value;
        }

        public ICommandExecutor CommandExecutor { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadView(string view)
        {
            if (!_views.TryGetValue(view, out var paths)) return;

            if (paths.Any(calculatorXmlPath => LoadCommandsFromStream(OpenStream(calculatorXmlPath)))
            )
                OnPropertyChanged(nameof(Commands));

            Stream OpenStream(string path)
            {
                return File.Exists(path)
                    ? File.OpenRead(path)
                    : typeof(CalculatorViewModel).Assembly.GetManifestResourceStream(path);
            }

            bool LoadCommandsFromStream(Stream stream)
            {
                if (stream == null) return false;
                try
                {
                    var serializer = new XmlSerializer(typeof(Commands));

                    using (var reader = new StreamReader(stream))
                    {
                        _commands = (Commands)serializer.Deserialize(reader);
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}