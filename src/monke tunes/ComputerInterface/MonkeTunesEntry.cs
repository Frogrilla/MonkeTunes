using System;
using System.Collections.Generic;
using System.Text;
using ComputerInterface.Interfaces;

namespace MonkeTunes.ComputerInterface
{
    internal class MonkeTunesEntry : IComputerModEntry
    {
        public string EntryName => "Monke Tunes";
        public Type EntryViewType => typeof(MonkeTunesView);
    }
}
