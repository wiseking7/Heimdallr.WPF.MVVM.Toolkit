using System.IO;

namespace Heimdallr.ToolKit.Models.TreeViewTest;

// 테스트용 폴더
public class FileCreator
{
  public string BasePath =
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

  public void Crate()
  {
    string textData = "Vicky test file content.";

    string[] tempFiles =
    {
      @"\Vicky\Microsoft\Visual Studio\solution.txt",
      @"\Vicky\Microsoft\Visual Studio\debug.mp3",
      @"\Vicky\Microsoft\Visual Studio\class.cs",
      @"\Vicky\Microsoft\Sql Management Studio\query.text",
      @"\Vicky\Apple\iPhone\store.txt",
      @"\Vicky\Apple\iPhone\calculator.mp3",
      @"\Vicky\Apple\iPhone\safari.cs",
    };

    foreach (var file in tempFiles)
    {
      string fullPath = BasePath + file;
      string? dirName = Path.GetDirectoryName(fullPath);

      if (!Directory.Exists(dirName))
      {
        Directory.CreateDirectory(dirName!);
      }

      File.WriteAllText(fullPath, textData);
    }
  }
}
