using System.IO;

public class FileLog
{
    /// <summary>
    /// ���O�t�@�C���ɒǋL
    /// </summary>
    /// <param name="filename">�t�@�C����</param>
    /// <param name="text">�ǋL����e�L�X�g</param>
    public static void AppendLog(string filename, string text)
    {
        StreamWriter sw = null;
        try
        {
            completeDirectory(Path.GetDirectoryName(filename));
            sw = new StreamWriter(filename, true, System.Text.Encoding.UTF8);
            sw.Write(text);
        }
        finally
        {
            sw?.Close();
        }
    }

    /// <summary>
    /// �w��f�B���N�g�������݂��Ȃ��ꍇ�A�ォ��H���č쐬����
    /// </summary>
    /// <param name="dir">�w��f�B���N�g��</param>
    /// <returns>true..�쐬����</returns>
    static bool completeDirectory(string dir)
    {
        if (string.IsNullOrEmpty(dir) == true)
        {
            return false;
        }
        if (Directory.Exists(dir) == false)
        {
            completeDirectory(Path.GetDirectoryName(dir));
            Directory.CreateDirectory(dir);
            return true;
        }

        return false;
    }
}