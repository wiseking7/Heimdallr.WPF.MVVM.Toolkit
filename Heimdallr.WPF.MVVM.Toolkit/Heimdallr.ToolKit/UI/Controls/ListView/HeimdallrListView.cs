using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrListView : ListView
{
  static HeimdallrListView()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListView),
      new FrameworkPropertyMetadata(typeof(HeimdallrListView)));
  }

  protected override void OnInitialized(EventArgs e)
  {
    base.OnInitialized(e);

    AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnHeaderClick));
  }

  private void OnHeaderClick(object sender, RoutedEventArgs e)
  {
    if (e.OriginalSource is GridViewColumnHeader header && header.Column != null)
    {
      var binding = header.Column.DisplayMemberBinding as System.Windows.Data.Binding;

      string? sortBy = binding?.Path.Path ?? header.Column.Header?.ToString();

      ICollectionView dataView = CollectionViewSource.GetDefaultView(ItemsSource);

      if (dataView == null || string.IsNullOrEmpty(sortBy))
      {
        return;
      }

      ListSortDirection direction = ListSortDirection.Ascending;

      if (dataView.SortDescriptions.Count > 0 && dataView.SortDescriptions[0].PropertyName == sortBy)
      {
        direction = dataView.SortDescriptions[0].Direction == ListSortDirection.Ascending
          ? ListSortDirection.Descending
          : ListSortDirection.Ascending;

        dataView.SortDescriptions.Clear();
      }

      dataView.SortDescriptions.Add(new SortDescription(sortBy, direction));
    }
  }
}
