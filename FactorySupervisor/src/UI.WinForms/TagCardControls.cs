using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorySupervisor.src.UI.WinForms
{
    public sealed class TagCardControls
    {
        public Sunny.UI.UIPanel Panel { get; init; } = null!;
        public Sunny.UI.UILabel NameLabel { get; init; } = null!;
        public Sunny.UI.UILabel ValueLabel { get; init; } = null!;
        public Sunny.UI.UILabel QualityLabel { get; init; } = null!;
        public Sunny.UI.UILabel AddressLabel { get; init; } = null!;
    }
}
