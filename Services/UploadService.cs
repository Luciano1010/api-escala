using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using OfficeOpenXml;
using DocumentFormat.OpenXml.Features;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using iText.Commons.Utils;

public class UploadService : ILeitorArquivo
{
    public async Task<List<string>> LerNomesAsync(IFormFile file)
    {
        var extensao = Path.GetExtension(file.FileName).ToLower();

        return extensao switch
        {
            ".txt" => await LerTxt(file),
            ".xlsx" => await LerExcel(file),
            ".csv" => await LerCsvManual(file),
            _ => throw new Exception("Formato não suportado")
        };
    }

    private Task<List<string>> LerTxt(IFormFile file)
    {
        var nomes = new List<string>();

        using var reader = new StreamReader(file.OpenReadStream());

        while (!reader.EndOfStream)
        {
            var linha = reader.ReadLine();

            if (!string.IsNullOrWhiteSpace(linha))
                nomes.Add(NormalizarNome(linha));
        }

        return Task.FromResult(nomes);
    }

    private Task<List<string>> LerCsvManual(IFormFile file)
    {
       
        bool primeiraLinha = true;
        var nomes = new List<string>();
        using var reader = new StreamReader(file.OpenReadStream());

        while (!reader.EndOfStream)
        {
            var linha = reader.ReadLine();

            if (linha == null)
            {
                continue;
            }

            if (primeiraLinha)
            {
                primeiraLinha= false;
                continue;
            }
            
            linha = linha.Trim();

            if(string.IsNullOrWhiteSpace(linha))
            continue;

            var partes = linha.Split(",");
            var nome = partes[0].Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                nomes.Add(nome);
            }


        }
        return Task.FromResult(nomes);

    }

    private Task<List<string>> LerExcel(IFormFile file)
    {
        var nomes = new List<string>();
        using var package = new ExcelPackage(file.OpenReadStream());
        var sheet = package.Workbook.Worksheets[0];
      
    
        for( int linha = 2; linha <= sheet.Dimension.End.Row; linha++)
        {

            var celula = sheet.Cells[linha, 1].Text;

            var nome = celula.Trim();

            if(string.IsNullOrWhiteSpace(nome))
            continue;

            nomes.Add(nome);
           
        }
    
       return Task.FromResult(nomes);

    }

    private string NormalizarNome(string texto)
    {
        return texto.Trim();
    }
}