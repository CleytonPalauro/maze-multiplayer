using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* "using System.IO"
 * O namespace System.IO contém tipos que permitem ler e gravar 
 * em arquivos e em fluxos de dados, além de tipos que
 * dão suporte básico a diretórios e arquivos.
 * https://docs.microsoft.com/pt-br/dotnet/api/system.io
 */

public class FileManager : MonoBehaviour
{
    [Header("File Section")]
    public string fileName = "save.txt";

    public enum fileExtension
    {
        txt, dat
    }

    public fileExtension myFile;

    public string fileTitle = "Título do arquivo";

    public string fileNote = "Observação";

    [Header("TextAsset Section")]
    public TextAsset myTextAsset;

    [TextArea(4,20)]
    public string myTextAssetString;
    
    void Start () { }

    /// <summary>
    /// Método para inserir a extensão do arquivo.
    /// <para>Retorna uma string com o nome e extensão do arquivo de texto.</para>
    /// </summary>
    /// <param name="name">Recebe o nome do arquivo de texto.</param>
    /// <returns>Retorna uma string com o nome e extensão do arquivo de texto.</returns>

    public string FileExtension(string name)
    {
        string fullFileName;

        switch (myFile)
        {
            case fileExtension.dat:
                fullFileName = name + "dat";
                break;
            case fileExtension.txt:
                fullFileName = name + "txt";
                break;
        }

        return fullFileName;
    }

    /// <summary>
    /// Método para Criar e gravar no arquivo.
    /// </summary>
    /// <param name="name">Recebe o nome do arquivo de texto.</param>
    /// <param name="title">Recebe um titulo para ser inserido no cabeçalho.</param>
    
    public void CreateFile(string name, string title)
    {
        string fullName = FileExtension(name);

        /* "Application.dataPath"
         * Contém o caminho para a pasta de dados do jogo (somente leitura).
         * O caminho depende de qual plataforma você está executando.
         * Unity Editor: <caminho para a pasta do projeto>/Assets
         * https://docs.unity3d.com/ScriptReference/Application-dataPath.html
         */

        string pathFile = Application.dataPath + "/" + fullName;

        /* "File.Exists"
         * Determina se o arquivo especificado existe.
         * https://docs.microsoft.com/pt-br/dotnet/api/system.io.file.exists
         */

        if (!File.Exists(pathFile))
        {
            /* "File.WriteAllText"
             * Cria um novo arquivo, grava a cadeia de caracteres
             * especificada no arquivo e fecha o arquivo.
             * Se o arquivo de destino já existir, ele será substituído.
             * https://docs.microsoft.com/pt-br/dotnet/api/system.io.file.writealltext
             */

            File.WriteAllText(pathFile, title.ToUpper() + "\n");

            Debug.Log("Arquivo " + fullName + " criado!");
        }
        else
        {
            Debug.Log("Arquivo " + fullName + " já existe!");
        }
    }

    /// <summary>
    /// Método para leitura do arquivo.
    /// <para>Retorna uma string com o conteúdo do arquivo de texto.</para>
    /// </summary>
    /// <param name="name">Recebe o nome do arquivo de texto.</param>
    /// <returns>Retorna uma string com o conteúdo do arquivo de texto.</returns>

    public string ReadFile(string name)
    {
        string fullName = FileExtension(name);

        string pathFile = Application.dataPath + "/" + fullName;

        /* "File.ReadAllText"
         * Abre um arquivo de texto, lê todo o texto do arquivo
         * como uma cadeia de caracteres e o fecha.
         * https://docs.microsoft.com/pt-br/dotnet/api/system.io.file.readalltext
         */

        string readfile = File.ReadAllText(pathFile);

        return readfile;
    }

    /// <summary>
    /// Método para atualizar o arquivo.
    /// </summary>
    /// <param name="name">Recebe o nome do arquivo de texto.</param>
    /// <param name="note">Recebe uma nota para ser inserida.</param>
    
    public void UpdateFile(string name, string note)
    {
        string fullName = FileExtension(name);

        string pathFile = Application.dataPath + "/" + fullName;

        /* "System.DateTime.Now"
         * Obtém um objeto DateTime definido como a data e hora atuais
         * neste computador, expressas como a hora local.
         * https://docs.microsoft.com/pt-br/dotnet/api/system.datetime.now
         */

        string content = "Login: " + System.DateTime.Now + " " + note + "\n";

        /* "File.AppendAllText"
         * Abre um arquivo, acrescenta a cadeia de caracteres especificada
         * no arquivo e fecha o arquivo. Se o arquivo não existir,
         * esse método criará um arquivo, gravará a cadeia de caracteres
         * especificada no arquivo e fechará o arquivo.
         * https://docs.microsoft.com/pt-br/dotnet/api/system.io.file.appendalltext
         */

        File.AppendAllText(path, content);

        Debug.Log("Arquivo " + fullName + " atualizado!");
    }

    /// <summary>
    /// Método para deletar o arquivo.
    /// </summary>
    /// <param name="name">Recebe o nome do arquivo de texto.</param>

    public void DeleteFile(string name)
    {
        string fullName = FileExtension(name);

        /* "Path.Combine"
         * Este método concatena cadeias de caracteres individuais em uma 
         * única cadeia de caracteres que representa um caminho de arquivo.
         * https://docs.microsoft.com/pt-br/dotnet/api/system.io.path.combine
         */

        string path = Path.Combine(Application.dataPath, fullName);

        if (File.Exists(path))
        {
            /* "File.Delete"
             * Exclui o arquivo especificado no caminho "path".
             * https://docs.microsoft.com/pt-br/dotnet/api/system.io.file.delete
             */

            File.Delete(path);

            Debug.Log("Arquivo " + fullName + " deletado!");
        }
        else
        {
            Debug.Log("Arquivo " + fullName + " não encontrado!");
        }
    }

    /// <summary>
    /// Método para ler o conteúdo em texto do TextAsset "rawText".
    /// <para>Retorna uma string com o conteúdo do TextAsset.</para>
    /// </summary>
    /// <param name="rawText">Recebe o TextAsset que será lido.</param>
    /// <returns>Retorna uma string com o conteúdo do TextAsset.</returns> 

    public string ReadTextAsset(TextAsset rawText)
    {
        /* "rawText.text"
         * O conteúdo do texto do arquivo .txt como uma string. (Somente leitura)
         * https://docs.unity3d.com/ScriptReference/TextAsset-text.html
         */
        
        string myRawText = rawText.text;

        return myRawText;
    }
}
