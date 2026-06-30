using System.Collections.ObjectModel;

namespace PowerBICleanup.UI.Explorer;

public sealed class ExplorerItem
{
    public required string Name { get; init; }

    public required ExplorerItemType ItemType { get; init; }

    public object? Tag { get; init; }

    public ObservableCollection<ExplorerItem> Children { get; } = new();
}
