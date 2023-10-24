namespace WebAPIContentService.Infra.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        // Método para confirmar as alterações.
        Task Commit();

        // Método para desfazer as alterações.
        void Rollback();
    }
}
