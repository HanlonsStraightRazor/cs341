using System.Collections.Generic;
using System.Windows.Controls;

namespace NFLStats
{
    /// <summary>
    /// Interaction logic for DataGridTable.xaml
    /// </summary>
    public partial class DataGridTable : UserControl
    {
        public DataGridTable(List<object> stats)
        {
            InitializeComponent();
            grid.ItemsSource = stats;
        }
    }
}
