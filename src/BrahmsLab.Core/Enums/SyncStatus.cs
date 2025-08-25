namespace BrahmsLab.Core.Enums;

public enum SyncStatus
{
    New,      // Criado localmente, nunca sincronizado
    Synced,   // Em sincronia com a nuvem
    Modified, // Modificado localmente, precisa ser enviado
    Error,    // Ocorreu um erro durante a sincronização
    Deleted   // Marcado para exclusão na próxima sincronização
}