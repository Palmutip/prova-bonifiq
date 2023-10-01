namespace ProvaPub.Interfaces
{
    /// <summary>
    /// Interface responsável por gerar um numero aleatório
    /// </summary>
    public interface IRandomService
    {
        /// <summary>
        /// Método responsável por retornar um numero inteiro aleatório
        /// </summary>
        /// <returns>Um numero intero aleatório cada vez que é instanciado</returns>
        int GetRandom();
    }
}