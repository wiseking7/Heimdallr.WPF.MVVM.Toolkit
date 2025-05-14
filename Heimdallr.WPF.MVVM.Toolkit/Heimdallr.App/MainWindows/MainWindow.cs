using Heimdallr.ToolKit.UI.Controls;
using System.Windows;

namespace Heimdallr.App;

public class MainWindow : DarkThemeWindow
{
  static MainWindow()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow),
      new FrameworkPropertyMetadata(typeof(MainWindow)));
  }
}
