public interface ILeitorArquivo
{
    Task<List<string>> LerNomesAsync(IFormFile file);
}