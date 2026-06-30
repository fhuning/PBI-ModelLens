using System.Collections.ObjectModel;

namespace PowerBICleanup.UI.Explorer;

public sealed class ExplorerItem
{
    public required string Name { get; init; }

    public ObservableCollection<ExplorerItem> Children { get; } = new();
}
