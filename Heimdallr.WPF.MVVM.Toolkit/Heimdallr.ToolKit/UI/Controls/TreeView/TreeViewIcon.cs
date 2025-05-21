using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class TreeViewIcon : Control
{
  static TreeViewIcon()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeViewIcon),
      new FrameworkPropertyMetadata(typeof(TreeViewIcon)));
  }



  public string Extension
  {
    get { return (string)GetValue(ExtensionProperty); }
    set { SetValue(ExtensionProperty, value); }
  }

  public static readonly DependencyProperty ExtensionProperty =
      DependencyProperty.Register("Extension", typeof(string), typeof(TreeViewIcon),
        new PropertyMetadata(null));


  public string Type
  {
    get { return (string)GetValue(TypeProperty); }
    set { SetValue(TypeProperty, value); }
  }

  public static readonly DependencyProperty TypeProperty =
      DependencyProperty.Register("Type", typeof(string), typeof(TreeViewIcon),
        new PropertyMetadata(null));
}
